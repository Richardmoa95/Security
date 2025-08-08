using MediatR;
using PhotoService.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoService.Application.Features.Photos.Queries;

public class GetAllPhotosQuery : IRequest<List<PhotoDto>> 
{ 
}
