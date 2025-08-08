using PhotoService.Core.Entities;
using PhotoService.Core.Interfaces;
using PhotoService.Infrastructure.External;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoService.Infrastructure.Services;

public class PhotoService : IPhotoService
{
    private readonly ReqResApiClient _apiClient;

    public PhotoService(ReqResApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    public async Task<List<Photo>> GetAllPhotosAsync()
    {
        var users = await _apiClient.GetUsersAsync();
        var photos = new List<Photo>();

        foreach (var user in users)
        {
            var base64Image = await DownloadImageAsBase64Async(user.Avatar);
            photos.Add(new Photo
            {
                UserId = user.Id.ToString(),
                FullName = $"{user.First_Name} {user.Last_Name}",
                Base64Image = base64Image
            });
        }

        return photos;
    }

    public async Task<Photo?> GetPhotoByUserIdAsync(string userId)
    {
        var users = await _apiClient.GetUsersAsync();
        var user = users.FirstOrDefault(u => u.Id.ToString() == userId);

        if (user == null) return null;

        var base64 = await DownloadImageAsBase64Async(user.Avatar);

        return new Photo
        {
            UserId = user.Id.ToString(),
            FullName = $"{user.First_Name} {user.Last_Name}",
            Base64Image = base64
        };
    }

    private async Task<string> DownloadImageAsBase64Async(string imageUrl)
    {
        using var http = new HttpClient();
        var bytes = await http.GetByteArrayAsync(imageUrl);
        return Convert.ToBase64String(bytes);
    }
}
