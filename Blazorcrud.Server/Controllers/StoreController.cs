using Blazorcrud.Server.Authorization;
using Blazorcrud.Server.Models;
using Blazorcrud.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Blazorcrud.Server.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class StoreController : ControllerBase
{
    private readonly IStoreRepository _registerRepository;

    public StoreController(IStoreRepository registerRepository)
    {
        _registerRepository = registerRepository;
    }

    [AllowAnonymous]
    [HttpGet]
    public ActionResult GetStores([FromQuery] string? name, int page)
    {
        Console.WriteLine($"Server.StoreController, GetStore: {name}, {page}");
        return Ok(_registerRepository.GetStores(name, page));
    }

    [AllowAnonymous]
    [HttpGet("{id}")]
    public async Task<ActionResult> GetById(int id)
    {
        return Ok(await _registerRepository.GetById(id));
    }

    [HttpPost]
    public async Task<ActionResult> Create(Store register)
    {
        return Ok(await _registerRepository.Create(register));
    }

    [HttpPut]
    public async Task<ActionResult> Update(Store register)
    {
        return Ok(await _registerRepository.Update(register));
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        return Ok(await _registerRepository.Delete(id));
    }
}