using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhotoService.Application.DTOs;
using PhotoService.Application.Features.Photos.Queries;

namespace PhotoService.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PhotosController : ControllerBase
{
    private readonly IMediator _mediator;

    public PhotosController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<List<PhotoDto>>> GetAll()
    {
        var photos = await _mediator.Send(new GetAllPhotosQuery());
        return Ok(photos);
    }

    [Authorize]
    [HttpGet("{userId}")]
    public async Task<ActionResult<PhotoDto>> GetByUserId(string userId)
    {
        var photo = await _mediator.Send(new GetPhotoByUserIdQuery(userId));

        if (photo == null)
        {
            return NotFound();
        }

        return Ok(photo);
    }
}
