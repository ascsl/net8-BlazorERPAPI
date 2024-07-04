using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Blazorcrud.Shared.Data;
using Blazorcrud.Shared.Models;

namespace Blazorcrud.Server.Models;

public class CartItemRepository : ICartItemRepository
{
    private readonly AppDbContext _appDbContext;

    public CartItemRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<CartItem?> GetById(int id)
    {
        var result = await _appDbContext.CartItemsDb
            .FirstOrDefaultAsync(p => p.ProductId == id);
        if (result != null)
        {
            return result;
        }
        else
        {
            throw new KeyNotFoundException("Register not found");
        }
    }

    public async Task<CartItem?> GetByGuid(string guid)
    {
        var result = await _appDbContext.CartItemsDb
            .FirstOrDefaultAsync(p => p.CartItemGuid.Equals(guid));
        if (result != null)
        {
            return result;
        }
        else
        {
            throw new KeyNotFoundException("Register not found");
        }
    }

    public PagedResult<CartItem> GetCartItems(string? name, int page)
    {
        int pageSize = 5;
        
        if (name != null)
        {
            Console.WriteLine($"Server.Models.CartItemRepository, GetCategories, if: {name}, {page}");
            return _appDbContext.CartItemsDb
                .OrderBy(r => r.CartItemId)
                .GetPaged(page, pageSize);
        }
        else
        {
            Console.WriteLine($"Server.Models.CartItemRepository, GetCategories, else: {name}, {page}");
            return _appDbContext.CartItemsDb
                .OrderBy(r => r.CartItemId)
                .GetPaged(page, pageSize);
        }
    }

    public async Task<CartItem> Create(CartItem register)
    {
        Console.WriteLine("Server.Models.CartItemRepository, Create:");
        var result = await _appDbContext.CartItemsDb.AddAsync(register);
        await _appDbContext.SaveChangesAsync();
        return result.Entity;
    }

    public async Task<CartItem> AddToCart(CartItem register)
    {
        Console.WriteLine("Server.Models.ProductRepository, AddToCart:");
        var result = await _appDbContext.CartItemsDb.AddAsync(register);
        await _appDbContext.SaveChangesAsync();
        return result.Entity;
    }

    public async Task<CartItem?> Update(CartItem register)
    {
        Console.WriteLine("Server.Models.CartItemRepository, Update:");
        var result = await _appDbContext.CartItemsDb.FirstOrDefaultAsync(p => p.ProductId == register.ProductId);
        if (result != null)
        {
            _appDbContext.Entry(result).CurrentValues.SetValues(register);
        }
        else
        {
            throw new KeyNotFoundException("Register not found");
        }
        return result;
    }

    public async Task<CartItem?> Delete(int id)
    {
        Console.WriteLine($"Server.Models.CartItemRepository, Delete: {id}");
        var result = await _appDbContext.CartItemsDb.FirstOrDefaultAsync(p => p.CartItemId == id);
        if (result != null)
        {
            _appDbContext.CartItemsDb.Remove(result);
            await _appDbContext.SaveChangesAsync();
        }
        else
        {
            throw new KeyNotFoundException("Register not found");
        }
        return result;
    }
}