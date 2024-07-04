using Blazorcrud.Shared.Data;
using Blazorcrud.Shared.Models;

namespace Blazorcrud.Server.Models;

public interface ICartItemRepository
{
    PagedResult<CartItem> GetCartItems(string? name, int page);
    Task<CartItem?> GetById(int id);
    Task<CartItem?> GetByGuid(string guid);
    Task<CartItem> Create(CartItem register);
    Task<CartItem> AddToCart(CartItem register);
    Task<CartItem?> Update(CartItem register);
    Task<CartItem?> Delete(int id);
}