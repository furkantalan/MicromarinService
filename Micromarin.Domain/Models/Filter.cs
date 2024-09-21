using Micromarin.Domain.Enums;

namespace Micromarin.Domain.Models;

public class Filter
{
    public string FieldName { get; set; }
    public FilterOperator Operator { get; set; }
    public object Value { get; set; }
}