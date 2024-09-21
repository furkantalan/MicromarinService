
namespace Micromarin.Domain.Models;

public class ListByFilterRequest
{
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public List<Filter> Filters { get; set; }
    public string SortField { get; set; }
    public bool SortDescending { get; set; }

    public ListByFilterRequest()
    {
        Filters = new List<Filter>();
    }

    public static ListByFilterRequest FromQuery(ListByFilterQuery query)
    {
        return new ListByFilterRequest
        {
            PageIndex = query.PageIndex >= 0 ? query.PageIndex : 0,
            PageSize = query.PageSize > 0 ? query.PageSize : 10,
            SortField = query.SortField,
            SortDescending = query.SortDescending,
            Filters = query.Filters
        };
    }

}