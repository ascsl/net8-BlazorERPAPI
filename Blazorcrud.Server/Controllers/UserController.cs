using Blazorcrud.Server.Authorization;
using Blazorcrud.Server.Models;
using Blazorcrud.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Blazorcrud.Server.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserRepository _userRepository;
    private readonly AppSettings _appSettings;

    public UserController(IUserRepository userRepository, IOptions<AppSettings> appSettings)
    {
        _userRepository = userRepository;
        _appSettings = appSettings.Value;
    }

    [AllowAnonymous]
    [HttpPost("authenticate")]
    public ActionResult Authenticate(AuthenticateRequest request)
    {
        return Ok(_userRepository.Authenticate(request));
    }

    [AllowAnonymous]
    [HttpGet]
    public ActionResult GetUsers([FromQuery] string? name, int page)
    {
        return Ok(_userRepository.GetUsers(name, page));
    }

    [AllowAnonymous]
    [HttpGet("{id}")]
    public async Task<ActionResult> GetUser(int id)
    {
        return Ok(await _userRepository.GetUser(id));
    }

    [HttpPost]
    public async Task<ActionResult> AddUser(User user)
    {
        return Ok(await _userRepository.AddUser(user));
    }
    
    [HttpPut]
    public async Task<ActionResult> UpdateUser(User user)
    {
        return Ok(await _userRepository.UpdateUser(user));
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteUser(int id)
    {
        return Ok(await _userRepository.DeleteUser(id));
    }
}