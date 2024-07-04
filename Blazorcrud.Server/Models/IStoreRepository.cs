using Blazorcrud.Shared.Data;
using Blazorcrud.Shared.Models;

namespace Blazorcrud.Server.Models;

public interface IStoreRepository
{
    PagedResult<Store> GetStores(string? name, int page);
    Task<Store?> GetById(int id);
    Task<Store?> GetByGuid(string guid);
    Task<Store> Create(Store register);
    Task<Store?> Update(Store register);
    Task<Store?> Delete(int id);
}