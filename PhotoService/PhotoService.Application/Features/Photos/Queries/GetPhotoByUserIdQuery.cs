using MediatR;
using PhotoService.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoService.Application.Features.Photos.Queries;

public class GetPhotoByUserIdQuery : IRequest<PhotoDto?>
{
    public string UserId { get; set; } = string.Empty;

    public GetPhotoByUserIdQuery(string userId)
    {
        UserId = userId;
    }
}
