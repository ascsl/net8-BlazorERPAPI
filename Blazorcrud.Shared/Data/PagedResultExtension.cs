using System.Xml.Linq;

namespace Blazorcrud.Shared.Data;

public static class PagedResultExtensions
{
    public static PagedResult<T> GetPaged<T>(this IQueryable<T> query, int page, int pageSize) where T : class
    {
        Console.WriteLine("Shared.PagedResultExtensions, GetPaged - 1");
        Console.WriteLine($"Shared.PagedResultExtensions, GetPaged - 2: {query.ToList()}, {page}, {pageSize}");
        var result = new PagedResult<T>();
        result.CurrentPage = page;
        result.PageSize = pageSize;
        result.RowCount = query.Count();

        var pageCount = (double)result.RowCount / pageSize;
        result.PageCount = (int)Math.Ceiling(pageCount);

        var skip = (page - 1) * pageSize;
        result.Results = query.Skip(skip).Take(pageSize).ToList();
        Console.WriteLine($"Shared.PagedResultExtensions, GetPaged - 3: {result.Results.ToList()}");

        return result;
    }
}