using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Blazorcrud.Shared.Data;
using Blazorcrud.Shared.Models;

namespace Blazorcrud.Server.Models;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _appDbContext;

    public ProductRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<Product?> GetById(int id)
    {
        var result = await _appDbContext.ProductsDb
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

    public async Task<Product?> GetByGuid(string guid)
    {
        var result = await _appDbContext.ProductsDb
            .FirstOrDefaultAsync(p => p.ProductGuid.Equals(guid));
        if (result != null)
        {
            return result;
        }
        else
        {
            throw new KeyNotFoundException("Register not found");
        }
    }

    public PagedResult<Product> GetProducts(string? name, int page)
    {
        int pageSize = 5;
        
        if (name != null)
        {
            Console.WriteLine($"Server.Models.ProductRepository, GetProducts, if: {name}, {page}");
            return _appDbContext.ProductsDb
                .Where(r => r.Title.Contains(name, StringComparison.CurrentCultureIgnoreCase))
                .OrderBy(r => r.ProductId)
                .GetPaged(page, pageSize);
        }
        else
        {
            Console.WriteLine($"Server.Models.ProductRepository, GetProducts, else: {name}, {page}");
            return _appDbContext.ProductsDb
                .OrderBy(r => r.ProductId)
                .GetPaged(page, pageSize);
        }
    }

    public async Task<Product> Create(Product register)
    {
        Console.WriteLine("Server.Models.ProductRepository, Create:");
        var result = await _appDbContext.ProductsDb.AddAsync(register);
        await _appDbContext.SaveChangesAsync();
        return result.Entity;
    }

    public async Task<Product?> Update(Product register)
    {
        Console.WriteLine("Server.Models.ProductRepository, Update:");
        var result = await _appDbContext.ProductsDb.FirstOrDefaultAsync(p => p.ProductId == register.ProductId);
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

    public async Task<Product?> Delete(int id)
    {
        Console.WriteLine($"Server.Models.ProductRepository, Delete: {id}");
        var result = await _appDbContext.ProductsDb.FirstOrDefaultAsync(p => p.ProductId == id);
        if (result != null)
        {
            _appDbContext.ProductsDb.Remove(result);
            await _appDbContext.SaveChangesAsync();
        }
        else
        {
            throw new KeyNotFoundException("Register not found");
        }
        return result;
    }
}