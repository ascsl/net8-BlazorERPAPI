using Blazorcrud.Server.Authorization;
using Blazorcrud.Server.Models;
using Blazorcrud.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Blazorcrud.Server.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryRepository _registerRepository;

    public CategoryController(ICategoryRepository registerRepository)
    {
        _registerRepository = registerRepository;
    }

    [AllowAnonymous]
    [HttpGet]
    public ActionResult GetCategories([FromQuery] string? name, int page)
    {
        Console.WriteLine($"Server.StoreController, GetStore: {name}, {page}");
        return Ok(_registerRepository.GetCategories(name, page));
    }


    [AllowAnonymous]
    [HttpGet("{guid}")]
    public async Task<ActionResult> GetByGuid(string guid)
    {
        return Ok(await _registerRepository.GetByGuid(guid));
    }

    [HttpPost]
    public async Task<ActionResult> Create(Category register)
    {
        return Ok(await _registerRepository.Create(register));
    }

    [HttpPut]
    public async Task<ActionResult> Update(Category register)
    {
        return Ok(await _registerRepository.Update(register));
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        return Ok(await _registerRepository.Delete(id));
    }
}