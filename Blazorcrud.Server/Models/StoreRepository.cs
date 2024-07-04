using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Blazorcrud.Shared.Data;
using Blazorcrud.Shared.Models;

namespace Blazorcrud.Server.Models;

public class StoreRepository : IStoreRepository
{
    private readonly AppDbContext _appDbContext;

    public StoreRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<Store?> GetById(int id)
    {
        var result = await _appDbContext.StoresDb
            .FirstOrDefaultAsync(p => p.StoreId == id);
        if (result != null)
        {
            return result;
        }
        else
        {
            throw new KeyNotFoundException("Register not found");
        }
    }

    public async Task<Store?> GetByGuid(string guid)
    {
        var result = await _appDbContext.StoresDb
            .FirstOrDefaultAsync(p => p.StoreGuid.Equals(guid));
        if (result != null)
        {
            return result;
        }
        else
        {
            throw new KeyNotFoundException("Register not found");
        }
    }

    public PagedResult<Store> GetStores(string? name, int page)
    {
        int pageSize = 5;
        
        if (name != null)
        {
            Console.WriteLine($"Server.StoreRepository, GetStore, if: {name}, {page}");
            return _appDbContext.StoresDb
                .Where(p => p.Name.Contains(name, StringComparison.CurrentCultureIgnoreCase))
                .OrderBy(p => p.StoreId)
                .GetPaged(page, pageSize);
        }
        else
        {
            Console.WriteLine($"Server.StoreRepository, GetStore, else: {name}, {page}");
            return _appDbContext.StoresDb
                .OrderBy(p => p.StoreId)
                .GetPaged(page, pageSize);
        }
    }

    public async Task<Store> Create(Store register)
    {
        Console.WriteLine("Server.Models.StoreRepository, Create:");
        var result = await _appDbContext.StoresDb.AddAsync(register);
        await _appDbContext.SaveChangesAsync();
        return result.Entity;
    }

    public async Task<Store?> Update(Store register)
    {
        Console.WriteLine("Server.Models.StoreRepository, Update:");
        var result = await _appDbContext.StoresDb.FirstOrDefaultAsync(p => p.StoreId == register.StoreId);
        if (result != null)
        {
            // Update existing person
            _appDbContext.Entry(result).CurrentValues.SetValues(register);
        }
        else
        {
            throw new KeyNotFoundException("Register not found");
        }
        return result;
    }

    public async Task<Store?> Delete(int id)
    {
        Console.WriteLine($"Server.Models.StoreRepository, Delete: {id}");
        var result = await _appDbContext.StoresDb.FirstOrDefaultAsync(p => p.StoreId == id);
        if (result != null)
        {
            _appDbContext.StoresDb.Remove(result);
            await _appDbContext.SaveChangesAsync();
        }
        else
        {
            throw new KeyNotFoundException("Register not found");
        }
        return result;
    }
}