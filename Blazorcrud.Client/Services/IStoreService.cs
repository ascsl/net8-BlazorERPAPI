using Blazorcrud.Shared.Data;
using Blazorcrud.Shared.Models;

namespace Blazorcrud.Client.Services;

public interface IStoreService
{
    Task<PagedResult<Store>> GetStores(string name, string page);
    Task<Store> GetById(int id);
    Task<Store> GetByGuid(string guid);
    Task Create(Store register);
    Task Update(Store register);
    Task Delete(int id);
}