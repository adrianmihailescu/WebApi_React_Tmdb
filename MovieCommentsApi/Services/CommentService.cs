using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MovieCommentsApi.Data;
using MovieCommentsApi.Models;

namespace MovieCommentsApi.Services
{
    public class CommentsService : ICommentsService
    {
        private readonly ApplicationDbContext _dbContext;

        public CommentsService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Comment>> GetCommentsForMovieAsync(int movieId)
        {
            return await _dbContext.Comments
                .AsNoTracking()
                .Where(c => c.MovieId == movieId)
                .ToListAsync();
        }

        public async Task<Comment> AddCommentAsync(Comment comment)
        {
            _dbContext.Comments.Add(comment);
            await _dbContext.SaveChangesAsync();
            return comment;
        }
    }
}
