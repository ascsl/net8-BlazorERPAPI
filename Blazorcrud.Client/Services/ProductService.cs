using Blazorcrud.Client.Shared;
using Blazorcrud.Shared.Data;
using Blazorcrud.Shared.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.Win32;
using System.Xml.Linq;

namespace Blazorcrud.Client.Services;

public class ProductService : IProductService
{
    private IHttpService _httpService;

    public ProductService(IHttpService httpService)
    {
        Console.WriteLine("Client.Services.ProductService, ProductService():");
        _httpService = httpService;
    }

    public async Task<PagedResult<Product>> GetProducts(string name, string page)
    {
        Console.WriteLine($"Client.Services.ProductService, GetProducts: {name}, {page}");
        return await _httpService.Get<PagedResult<Product>>("api/product" + "?page=" + page + "&name=" + name);
    }

    public async Task<Product> GetById(int id)
    {
        Console.WriteLine($"Client.Services,ProductService, GetById: {id}");
        return await _httpService.Get<Product>($"api/product/{id}");
    }

    public async Task<Product> GetByGuid(string guid)
    {
        Console.WriteLine($"Client.Services,ProductService, GetByGuid: {guid}");
        return await _httpService.Get<Product>("api/product" + "?guid=" + guid);
    }

    public async Task Create(Product register)
    {
        Console.WriteLine($"Client.Services,ProductService, Create: {register.Title}");
        await _httpService.Post($"api/product", register);
    }

    public async Task Update(Product register)
    {
        Console.WriteLine($"Client.Services,ProductService, Update: {register.Title}");
        await _httpService.Put($"api/product", register);
    }

    public async Task Delete(int id)
    {
        Console.WriteLine($"Client.Services,ProductService, Delete: {id}");
        await _httpService.Delete($"api/product/{id}");
    }
}