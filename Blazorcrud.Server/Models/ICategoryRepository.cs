using Blazorcrud.Shared.Data;
using Blazorcrud.Shared.Models;

namespace Blazorcrud.Server.Models;

public interface ICategoryRepository
{
    PagedResult<Category> GetCategories(string? name, int page);
    Task<Category?> GetByGuid(string guid);
    Task<Category> Create(Category register);
    Task<Category?> Update(Category register);
    Task<Category?> Delete(int id);
}