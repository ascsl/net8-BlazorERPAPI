using Blazorcrud.Shared.Data;
using Blazorcrud.Shared.Models;

namespace Blazorcrud.Client.Services;

public interface ICategoryService
{
    Task<PagedResult<Category>> GetCategories(string name, string page);
    Task<Category> GetByGuid(string guid);
    Task Create(Category register);
    Task Update(Category register);
    Task Delete(int id);
}