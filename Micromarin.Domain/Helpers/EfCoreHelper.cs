using Micromarin.Domain.Enums;
using Micromarin.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Text.Json;

namespace Micromarin.Domain.Helpers;

public static class EfCoreHelper
{
    public static Expression<Func<T, bool>> BuildFilter<T>(List<Filter> filters)
    {
        if (filters == null || !filters.Any()) return x => true;

        var parameter = Expression.Parameter(typeof(T), "x");
        Expression combined = null;

        foreach (var filter in filters)
        {
            // filter.Value için JsonElement dönüşüm kontrolü yapılıyor
            object value = filter.Value is JsonElement jsonElement ? ConvertJsonElement(jsonElement) : filter.Value;
            var member = Expression.Property(parameter, filter.FieldName);
            var constant = Expression.Constant(value);

            Expression comparison = filter.Operator switch
            {
                FilterOperator.Equals => Expression.Equal(member, constant),
                FilterOperator.NotEquals => Expression.NotEqual(member, constant),
                FilterOperator.GreaterThan => Expression.GreaterThan(member, constant),
                FilterOperator.GreaterThanOrEqual => Expression.GreaterThanOrEqual(member, constant),
                FilterOperator.LessThan => Expression.LessThan(member, constant),
                FilterOperator.LessThanOrEqual => Expression.LessThanOrEqual(member, constant),
                FilterOperator.Contains => Expression.Call(
                    member,
                    typeof(string).GetMethod("Contains", new[] { typeof(string) }),
                    Expression.Constant(value.ToString()) // value'yu string'e dönüştürüyoruz
                ),
                FilterOperator.In => Expression.Call(typeof(Enumerable), "Contains", new[] { member.Type }, Expression.Constant(value), member),
                FilterOperator.Between => Expression.AndAlso(
                    Expression.GreaterThanOrEqual(member, Expression.Constant(((object[])value)[0])),
                    Expression.LessThanOrEqual(member, Expression.Constant(((object[])value)[1]))
                ),
                _ => throw new ArgumentException("Invalid filter operator")
            };

            combined = combined == null ? comparison : Expression.AndAlso(combined, comparison);
        }

        return Expression.Lambda<Func<T, bool>>(combined, parameter);
    }

    public static Func<IQueryable<T>, IOrderedQueryable<T>> BuildSort<T>(string sortField, bool sortDescending)
    {
        if (string.IsNullOrWhiteSpace(sortField)) return query => query.OrderBy(e => true);

        var parameter = Expression.Parameter(typeof(T), "x");
        var member = Expression.Property(parameter, sortField);
        var keySelector = Expression.Lambda(member, parameter);

        var methodName = sortDescending ? "OrderByDescending" : "OrderBy";
        var method = typeof(Queryable).GetMethods()
            .First(m => m.Name == methodName && m.GetParameters().Length == 2)
            .MakeGenericMethod(typeof(T), member.Type);

        return query => (IOrderedQueryable<T>)method.Invoke(null, new object[] { query, keySelector });
    }

    public static async Task<ListResult<T>> GetPagedResultAsync<T>(IQueryable<T> query, ListByFilterRequest request) where T : class
    {
        var filter = BuildFilter<T>(request.Filters);
        var sort = BuildSort<T>(request.SortField, request.SortDescending);

        query = query.Where(filter).ApplySorting(sort);

        var totalCount = await query.CountAsync();
        var items = await query.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize).ToListAsync();

        return new ListResult<T>(items, totalCount);
    }

    public static IOrderedQueryable<T> ApplySorting<T>(this IQueryable<T> source, Func<IQueryable<T>, IOrderedQueryable<T>> sort)
    {
        return sort(source);
    }

    private static object ConvertJsonElement(JsonElement element)
    {
        return element.ValueKind switch
        {
            JsonValueKind.String => TryParseDateTimeOrString(element.GetString()), // DateTime veya string kontrolü
            JsonValueKind.Number => ConvertNumber(element), // Sayısal değerler için genişletilmiş destek
            JsonValueKind.True => true,
            JsonValueKind.False => false,
            JsonValueKind.Null => null, // Null değer desteği
            JsonValueKind.Array => element.EnumerateArray().Select(e => ConvertJsonElement(e)).ToArray(),
            JsonValueKind.Object => element.EnumerateObject().ToDictionary(p => p.Name, p => ConvertJsonElement(p.Value)), // Nesne desteği
            _ => throw new ArgumentException("Unsupported JsonElement type")
        };
    }

    private static object ConvertNumber(JsonElement element)
    {
        // Önce integer kontrolü
        if (element.TryGetInt32(out var intValue))
            return intValue;
        // Daha büyük sayılar için long kontrolü
        if (element.TryGetInt64(out var longValue))
            return longValue;
        // Decimal kontrolü
        if (element.TryGetDecimal(out var decimalValue))
            return decimalValue;
        // Double kontrolü
        return element.GetDouble();
    }

    private static object TryParseDateTimeOrString(string str)
    {
        // Eğer string bir DateTime ise
        if (DateTime.TryParse(str, out var dateTime))
            return dateTime;

        // Eğer string bir Guid ise
        if (Guid.TryParse(str, out var guid))
            return guid;

        // Normal bir string ise
        return str;
    }
}