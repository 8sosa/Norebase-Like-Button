using Xunit;
using Moq;
using Microsoft.EntityFrameworkCore;
using LikeButtonApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

public class LikeControllerTests : IDisposable
{
     private readonly LikeController _controller;
    private readonly LikeDbContext _context;

    public LikeControllerTests()
    {
        var options = new DbContextOptionsBuilder<LikeDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Unique database name
            .Options;
        _context = new LikeDbContext(options);

        // Seed the in-memory database
        _context.LikeButtons.Add(new LikeButton { Id = 1, Likes = 0 });
        _context.SaveChanges();

        _controller = new LikeController(_context);
    }

    public void Dispose()
    {
        // Clean up the in-memory database after each test
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }
    [Fact]
    public async Task GetLikes_ReturnsOkResult_WithLikes()
    {
        // Act
        var result = await _controller.GetLikes(1);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var data = Assert.IsType<LikesResponse>(okResult.Value);
        Assert.Equal(0, data.Likes);
    }

    [Fact]
    public async Task GetLikes_ReturnsNotFound_WhenLikeButtonDoesNotExist()
    {
        // Act
        var result = await _controller.GetLikes(999);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task PostLike_IncrementsLikes_ReturnsOkResult()
    {
        // Act
        var result = await _controller.PostLike(1);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.NotNull(okResult.Value); // Check that Value is not null
        var data = Assert.IsType<LikesResponse>(okResult.Value);
        Assert.Equal(1, data.Likes);

        // Verify database update
        var likeButton = await _context.LikeButtons.FindAsync(1);
        Assert.NotNull(likeButton); // Check that the button exists
        Assert.Equal(1, likeButton.Likes);
    }

    [Fact]
    public async Task PostLike_ReturnsNotFound_WhenLikeButtonDoesNotExist()
    {
        // Act
        var result = await _controller.PostLike(999);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }
}
