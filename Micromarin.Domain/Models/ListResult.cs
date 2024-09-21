namespace Micromarin.Domain.Models;

public class ListResult<T>
{
    public IEnumerable<T> Items { get; set; }
    public long TotalCount { get; set; }

    public ListResult(IEnumerable<T> items, long totalCount)
    {
        Items = items;
        TotalCount = totalCount;
    }
}
