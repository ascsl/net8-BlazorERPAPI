using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Blazorcrud.Shared.Data;
using Blazorcrud.Shared.Models;

namespace Blazorcrud.Server.Models;

public class CategoryRepository : ICategoryRepository
{
    private readonly AppDbContext _appDbContext;

    public CategoryRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<Category?> GetByGuid(string guid)
    {
        var result = await _appDbContext.CategoriesDb
            .Include(p => p.Products)
            .FirstOrDefaultAsync(p => p.CategoryGuid.Equals(guid));
        if (result != null)
        {
            return result;
        }
        else
        {
            throw new KeyNotFoundException("Register not found");
        }
    }

    public PagedResult<Category> GetCategories(string? name, int page)
    {
        int pageSize = 5;
        
        if (name != null)
        {
            Console.WriteLine($"Server.Models.CategoryRepository, GetCategories, if: {name}, {page}");
            return _appDbContext.CategoriesDb
                .Where(r => r.Name.Contains(name, StringComparison.CurrentCultureIgnoreCase))
                .OrderBy(r => r.CategoryId)
                .GetPaged(page, pageSize);
        }
        else
        {
            Console.WriteLine($"Server.Models.CategoryRepository, GetCategories, else: {name}, {page}");
            return _appDbContext.CategoriesDb
                .OrderBy(r => r.CategoryId)
                .GetPaged(page, pageSize);
        }
    }

    public async Task<Category> Create(Category register)
    {
        Console.WriteLine("Server.Models.CategoryRepository, Create:");
        var result = await _appDbContext.CategoriesDb.AddAsync(register);
        await _appDbContext.SaveChangesAsync();
        return result.Entity;
    }

    public async Task<Category?> Update(Category register)
    {
        Console.WriteLine("Server.Models.CategoryRepository, Update:");
        var result = await _appDbContext.CategoriesDb.FirstOrDefaultAsync(p => p.CategoryId == register.CategoryId);
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

    public async Task<Category?> Delete(int id)
    {
        Console.WriteLine($"Server.Models.CategoryRepository, Delete: {id}");
        var result = await _appDbContext.CategoriesDb.FirstOrDefaultAsync(p => p.CategoryId == id);
        if (result != null)
        {
            _appDbContext.CategoriesDb.Remove(result);
            await _appDbContext.SaveChangesAsync();
        }
        else
        {
            throw new KeyNotFoundException("Register not found");
        }
        return result;
    }
}