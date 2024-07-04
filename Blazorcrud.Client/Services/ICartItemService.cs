using Blazorcrud.Shared.Data;
using Blazorcrud.Shared.Models;

namespace Blazorcrud.Client.Services;

public interface ICartItemService
{
    Task<PagedResult<CartItem>> GetCartItems(string name, string page);
    Task<CartItem> GetById(int id);
    Task<CartItem> GetByGuid(string guid);
    Task Create(CartItem register);
    Task AddToCart(CartItem register);
    Task Update(CartItem register);
    Task Delete(int id);
}