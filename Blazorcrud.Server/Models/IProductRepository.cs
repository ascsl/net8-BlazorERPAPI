using Blazorcrud.Shared.Data;
using Blazorcrud.Shared.Models;

namespace Blazorcrud.Server.Models;

public interface IProductRepository
{
    PagedResult<Product> GetProducts(string? name, int page);
    Task<Product?> GetById(int id);
    Task<Product?> GetByGuid(string guid);
    Task<Product> Create(Product register);
    Task<Product?> Update(Product register);
    Task<Product?> Delete(int id);
}