using Microsoft.AspNetCore.Components;
using Microsoft.Win32;
using System.Xml.Linq;
using Blazorcrud.Client.Shared;
using Blazorcrud.Shared.Data;
using Blazorcrud.Shared.Models;

namespace Blazorcrud.Client.Services;

public class StoreService : IStoreService
{
    private IHttpService _httpService;

    public StoreService(IHttpService httpService)
    {
        Console.WriteLine("Client.Services.StoreService, StoreService():");
        _httpService = httpService;
    }

    public async Task<PagedResult<Store>> GetStores(string name, string page)
    {
        Console.WriteLine($"Client.Services.StoreService, GetStore: {name}, {page}");
        return await _httpService.Get<PagedResult<Store>>("api/store" + "?page=" + page + "&name=" + name);
    }

    public async Task<Store> GetById(int id)
    {
        Console.WriteLine($"Client.Services,StoreService, GetById: {id}");
        return await _httpService.Get<Store>($"api/store/{id}");
    }

    public async Task<Store> GetByGuid(string guid)
    {
        Console.WriteLine($"Client.Services,StoreService, GetByGuid: {guid}");
        return await _httpService.Get<Store>("api/store" + "?guid=" + guid);
    }

    public async Task Create(Store register)
    {
        Console.WriteLine($"Client.Services,StoreService, Create: {register.Name}");
        await _httpService.Post($"api/store", register);
    }

    public async Task Update(Store register)
    {
        Console.WriteLine($"Client.Services,StoreService, Update: {register.Name}");
        await _httpService.Put($"api/store", register);
    }

    public async Task Delete(int id)
    {
        Console.WriteLine($"Client.Services,StoreService, Delete: {id}");
        await _httpService.Delete($"api/store/{id}");
    }
}
