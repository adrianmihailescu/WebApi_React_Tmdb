using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using MovieCommentsApi.Models.Dto;

namespace MovieCommentsApi.Services
{
    public class MovieService : IMovieService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey = ""; 

        public MovieService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        public async Task<List<MovieListDto?>> GetLatestMoviesAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<TmdbMovieListResponse>(
        $"https://api.themoviedb.org/3/movie/now_playing?api_key={_apiKey}");

    return response?.Results ?? new List<MovieListDto>();
        }

        public async Task<List<MovieListDto?>> SearchMoviesAsync(string query, string genre)
        {
            var url = $"https://api.themoviedb.org/3/search/movie?query={query}&api_key={_apiKey}";
    var response = await _httpClient.GetFromJsonAsync<TmdbMovieListResponse>(url);

    return response?.Results ?? new List<MovieListDto>();
        }

        public async Task<MovieDetailsDto> GetMovieDetailsAsync(int movieId)
        {
            var url = $"https://api.themoviedb.org/3/movie/{movieId}?api_key={_apiKey}&append_to_response=images,credits";
            return await _httpClient.GetFromJsonAsync<MovieDetailsDto>(url);
        }
    }
}
