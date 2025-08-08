using PhotoService.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoService.Core.Interfaces;

public interface IPhotoService
{
    Task<List<Photo>> GetAllPhotosAsync();
    Task<Photo?> GetPhotoByUserIdAsync(string userId);
}
