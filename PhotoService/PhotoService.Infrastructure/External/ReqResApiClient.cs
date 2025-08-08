using PhotoService.Infrastructure.Models;
using System.Net.Http.Json;

namespace PhotoService.Infrastructure.External;

public class ReqResApiClient
{
    private readonly HttpClient _httpClient;

    public ReqResApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<ReqResUserData>> GetUsersAsync()
    {
        var response = await _httpClient.GetFromJsonAsync<ReqResUserResponse>("https://reqres.in/api/users");
        return response?.Data ?? new List<ReqResUserData>();
    }
}
