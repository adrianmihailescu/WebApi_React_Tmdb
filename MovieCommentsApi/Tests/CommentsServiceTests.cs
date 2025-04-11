using Xunit;
using Microsoft.EntityFrameworkCore;
using Xunit;
using MovieCommentsApi.Data;
using MovieCommentsApi.Services;
using MovieCommentsApi.Models;

public class CommentsServiceTests
{
    [Fact]
    public async Task AddComment_ReturnsCommentWithId()
    {
        // Arrange: Setup an in-memory db context
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "DummySqlDbInstance")
            .Options;
        using var dbContext = new ApplicationDbContext(options);
        var service = new CommentsService(dbContext);
        var comment = new Comment { MovieId = 1, UserId = "user1", Content = "Great movie!" };

        // Act
        var result = await service.AddCommentAsync(comment);

        // Assert
        Assert.NotEqual(0, result.Id);
    }
}
