using System.Collections.Generic;
using System.Threading.Tasks;
using MovieCommentsApi.Models;

namespace MovieCommentsApi.Services
{
    public interface ICommentsService
    {
        Task<IEnumerable<Comment>> GetCommentsForMovieAsync(int movieId);
        Task<Comment> AddCommentAsync(Comment comment);
    }
}
