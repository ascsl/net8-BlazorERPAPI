using Blazorcrud.Server.Authorization;
using Blazorcrud.Server.Models;
using Blazorcrud.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace Blazorcrud.Server.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class UploadController : ControllerBase
{
    private readonly IUploadRepository _uploadRepository;

    public UploadController(IUploadRepository uploadRepository)
    {
        _uploadRepository = uploadRepository;
    }

    [AllowAnonymous]
    [HttpGet]
    public ActionResult GetUploads(string? name, int page)
    {
        return Ok(_uploadRepository.GetUploads(name, page));
    }

    [AllowAnonymous]
    [HttpGet("{id}")]
    public async Task<ActionResult> GetUpload(int id)
    {
        return Ok(await _uploadRepository.GetUpload(id));
    }

    [HttpPost]
    public async Task<ActionResult> AddUpload(Upload upload)
    {
        return Ok(await _uploadRepository.AddUpload(upload));
    }
    
    [HttpPut]
    public async Task<ActionResult> UpdateUpload(Upload upload)
    {
        return Ok(await _uploadRepository.UpdateUpload(upload));
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteUpload(int id)
    {
        return Ok(await _uploadRepository.DeleteUpload(id));
    }
}