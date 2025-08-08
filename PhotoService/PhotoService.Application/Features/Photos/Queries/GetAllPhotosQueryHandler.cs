using MediatR;
using PhotoService.Application.DTOs;
using PhotoService.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoService.Application.Features.Photos.Queries;

public class GetAllPhotosQueryHandler : IRequestHandler<GetAllPhotosQuery, List<PhotoDto>>
{
    private readonly IPhotoService _photoService;

    public GetAllPhotosQueryHandler(IPhotoService photoService)
    {
        _photoService = photoService;
    }

    public async Task<List<PhotoDto>> Handle(GetAllPhotosQuery request, CancellationToken cancellationToken)
    {
        var photos = await _photoService.GetAllPhotosAsync();

        return photos.Select(p => new PhotoDto
        {
            UserId = p.UserId,
            FullName = p.FullName,
            Base64Image = p.Base64Image
        }).ToList();
    }
}
