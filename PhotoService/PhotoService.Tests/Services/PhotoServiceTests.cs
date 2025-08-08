using FluentAssertions;
using Moq;
using PhotoService.Application.DTOs;
using PhotoService.Infrastructure.External;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoService.Tests.Services;

public class PhotoServiceTests
{
    private readonly Mock<IReqResApiClient> _apiClientMock;
    private readonly Infrastructure.Services.PhotoService _photoService;

    public PhotoServiceTests()
    {
        _apiClientMock = new Mock<IReqResApiClient>();
        _photoService = new Infrastructure.Services.PhotoService(_apiClientMock.Object);
    }

    [Fact]
    public async Task GetAllPhotosAsync_Returns_Photos()
    {
        var expectedPhotos = new List<PhotoDto>
        {
            new PhotoDto
            {
                UserId = "1",
                FullName = "Photo User",
                Base64Image = "data:image/png;base64,XXX"
            }
        };

        _apiClientMock.Setup(x => x.GetUsersAsync()).ReturnsAsync(expectedPhotos);

        var result = await _photoService.GetAllPhotosAsync();

        result.Should().NotBeNull();
        result.Should().HaveCount(1);
    }

    [Fact]
    public async Task GetAllPhotosAsync_Returns_Empty_When_NoUsers()
    {
        _apiClientMock.Setup(x => x.GetUsersAsync()).ReturnsAsync(new List<PhotoDto>());

        var result = await _photoService.GetAllPhotosAsync();

        result.Should().BeEmpty();
    }
}
