using System.Threading.Tasks;
using MovieCommentsApi.Models.Dto;

namespace MovieCommentsApi.Services
{
    public interface IMovieService
    {
        Task<List<MovieListDto?>> GetLatestMoviesAsync();
        Task<List<MovieListDto?>> SearchMoviesAsync(string query, string genre);
        Task<MovieDetailsDto> GetMovieDetailsAsync(int movieId);
    }
}
