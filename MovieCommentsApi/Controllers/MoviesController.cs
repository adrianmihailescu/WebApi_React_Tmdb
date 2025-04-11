using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using MovieCommentsApi.Services;
using MovieCommentsApi.Models;
using Microsoft.AspNetCore.Authorization;

namespace MovieCommentsApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;
        private readonly ICommentsService _commentsService;

        public MoviesController(IMovieService movieService, ICommentsService commentsService)
        {
            _movieService = movieService;
            _commentsService = commentsService;
        }

        [HttpGet("latest")]
        public async Task<IActionResult> GetLatestMovies()
        {
            var movies = await _movieService.GetLatestMoviesAsync();
            return Ok(movies);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchMovies([FromQuery] string query, [FromQuery] string genre)
        {
            var movies = await _movieService.SearchMoviesAsync(query, genre);
            return Ok(movies);
        }

        [HttpGet("{movieId}")]
        public async Task<IActionResult> GetMovieDetails(int movieId)
        {
            var details = await _movieService.GetMovieDetailsAsync(movieId);
            // Optionally add local comments to response
            var comments = await _commentsService.GetCommentsForMovieAsync(movieId);
            return Ok(new { details, comments });
        }

[Authorize]
        [HttpPost("{movieId}/comments")]
        public async Task<IActionResult> AddComment(int movieId, [FromBody] Comment comment)
        {
            if (movieId != comment.MovieId)
            {
                return BadRequest("MovieId mismatch.");
            }
            // In real-world, validate user authentication/authorization
            var addedComment = await _commentsService.AddCommentAsync(comment);
            return CreatedAtAction(nameof(GetMovieDetails), new { movieId }, addedComment);
        }
    }
}
