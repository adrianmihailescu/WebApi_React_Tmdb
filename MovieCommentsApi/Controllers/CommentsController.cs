using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using MovieCommentsApi.Data;
using MovieCommentsApi.Models;

[ApiController]
[Route("api/[controller]")]
public class CommentsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public CommentsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/comments/movie/5
    [HttpGet("movie/{movieId}")]
    public async Task<IActionResult> GetCommentsByMovie(int movieId)
    {
        var comments = await _context.Comments
            .Where(c => c.MovieId == movieId)
            .OrderByDescending(c => c.CreatedAt)
            .ToListAsync();

        return Ok(comments);
    }

    // POST: api/comments
    [HttpPost]
public async Task<IActionResult> PostComment([FromBody] CommentDto commentDto)
{
    try
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (string.IsNullOrWhiteSpace(commentDto.Content))
            return BadRequest("Comment content cannot be empty.");

        var comment = new Comment
        {
            MovieId = commentDto.MovieId,
            Content = commentDto.Content,
            UserName = HttpContext?.User?.Identity?.IsAuthenticated == true ? User.Identity.Name : "Anonymous",
            CreatedAt = DateTime.UtcNow,
            UserId = "PublicUser"
        };

        _context.Comments.Add(comment);
        await _context.SaveChangesAsync();

        return Ok(comment);
    }
    catch (Exception ex)
    {
        return StatusCode(500, $"Server error: {ex.Message}");
    }
}
}
