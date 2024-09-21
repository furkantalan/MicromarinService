
namespace Micromarin.Domain.Models;

public class ListByFilterQuery
{
    /*
     we can use like this When ListAllDataQuery
    private int _pageIndex;
    private int _pageSize;
    public int PageIndex
    {
        get => _pageIndex;
        set
        {
            if (value < 0)
            {
                throw new ArgumentException("PageIndex cannot be less than 0.");
            }
            _pageIndex = value;
        }
    }

    public int PageSize
    {
        get => _pageSize;
        set
        {
            if (value < 0)
            {
                throw new ArgumentException("PageSize cannot be less than 0.");
            }
            _pageSize = value;
        }
    }
    */

    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public List<Filter> Filters { get; set; }
    public string SortField { get; set; }
    public bool SortDescending { get; set; }

    public ListByFilterQuery()
    {
        Filters = new List<Filter>();
    }
}
