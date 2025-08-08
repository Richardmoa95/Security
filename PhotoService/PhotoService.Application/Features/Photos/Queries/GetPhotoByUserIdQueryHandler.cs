using MediatR;
using PhotoService.Application.DTOs;
using PhotoService.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoService.Application.Features.Photos.Queries;

public class GetPhotoByUserIdQueryHandler : IRequestHandler<GetPhotoByUserIdQuery, PhotoDto?>
{
    private readonly IPhotoService _photoService;

    public GetPhotoByUserIdQueryHandler(IPhotoService photoService)
    {
        _photoService = photoService;
    }

    public async Task<PhotoDto?> Handle(GetPhotoByUserIdQuery request, CancellationToken cancellationToken)
    {
        var photo = await _photoService.GetPhotoByUserIdAsync(request.UserId);

        if (photo == null) return null;

        return new PhotoDto
        {
            UserId = photo.UserId,
            FullName = photo.FullName,
            Base64Image = photo.Base64Image
        };
    }
}
