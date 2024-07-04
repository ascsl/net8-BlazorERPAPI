using Blazorcrud.Client.Shared;
using Blazorcrud.Shared.Data;
using Blazorcrud.Shared.Models;

namespace Blazorcrud.Client.Services;

public class CartItemService : ICartItemService
{
    private IHttpService _httpService;

    public CartItemService(IHttpService httpService)
    {
        Console.WriteLine("Client.Services.CartItemService, CartItemService():");
        _httpService = httpService;
    }

    public async Task<PagedResult<CartItem>> GetCartItems(string name, string page)
    {
        name = "";
        page = "50";

        Console.WriteLine($"Client.Services.CartItemService, GetCartItems: {name}, {page}");
        return await _httpService.Get<PagedResult<CartItem>>("api/cartitem" + "?page=" + page + "&name=" + name);
    }

    public async Task<CartItem> GetById(int id)
    {
        Console.WriteLine($"Client.Services,CartItemService, GetById: {id}");
        return await _httpService.Get<CartItem>($"api/cartitem/{id}");
    }

    public async Task<CartItem> GetByGuid(string guid)
    {
        Console.WriteLine($"Client.Services,CartItemService, GetByGuid: {guid}");
        return await _httpService.Get<CartItem>("api/cartitem" + "?guid=" + guid);
    }

    public async Task Create(CartItem register)
    {
        Console.WriteLine($"Client.Services,CartItemService, Create: {register.Product.Title}");
        await _httpService.Post($"api/cartitem", register);
    }

    public async Task AddToCart(CartItem register)
    {
        Console.WriteLine($"Client.Services,CartItemService, AddToCart: {register.Product.Title}");
        await _httpService.Post($"api/cartitem", register);
    }
    public async Task Update(CartItem register)
    {
        Console.WriteLine($"Client.Services,CartItemService, Update: {register.Product.Title}");
        await _httpService.Put($"api/cartitem", register);
    }

    public async Task Delete(int id)
    {
        Console.WriteLine($"Client.Services,CartItemService, Delete: {id}");
        await _httpService.Delete($"api/cartitem/{id}");
    }
}
