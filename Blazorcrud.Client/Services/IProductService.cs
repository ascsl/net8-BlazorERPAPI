using Blazorcrud.Shared.Data;
using Blazorcrud.Shared.Models;

namespace Blazorcrud.Client.Services;

public interface IProductService
{
    Task<PagedResult<Product>> GetProducts(string name, string page);
    Task<Product> GetById(int id);
    Task<Product> GetByGuid(string guid);
    Task Create(Product register);
    Task Update(Product register);
    Task Delete(int id);
}
