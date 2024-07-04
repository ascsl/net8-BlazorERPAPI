using Blazorcrud.Client.Shared;
using Blazorcrud.Shared.Data;
using Blazorcrud.Shared.Models;

namespace Blazorcrud.Client.Services;

public class CategoryService : ICategoryService
{
    private IHttpService _httpService;

    public CategoryService(IHttpService httpService)
    {
        Console.WriteLine("Client.Services.CategoryService, CategoryService():");
        _httpService = httpService;
    }

    public async Task<PagedResult<Category>> GetCategories(string name, string page)
    {
        Console.WriteLine($"Client.Services.CategoryService, GetCategories: {name}, {page}");
        return await _httpService.Get<PagedResult<Category>>("api/category" + "?page=" + page + "&name=" + name);
    }

    public async Task<Category> GetByGuid(string guid)
    {
        Console.WriteLine($"Client.Services,CategoryService, GetByGuid: {guid}");
        return await _httpService.Get<Category>("api/category" + "?guid=" + guid);
    }

    public async Task Create(Category register)
    {
        Console.WriteLine($"Client.Services,CategoryService, Create: {register.Name}");
        await _httpService.Post($"api/category", register);
    }

    public async Task Update(Category register)
    {
        Console.WriteLine($"Client.Services,CategoryService, Update: {register.Name}");
        await _httpService.Put($"api/category", register);
    }

    public async Task Delete(int id)
    {
        Console.WriteLine($"Client.Services,CategoryService, Delete: {id}");
        await _httpService.Delete($"api/category/{id}");
    }
}
