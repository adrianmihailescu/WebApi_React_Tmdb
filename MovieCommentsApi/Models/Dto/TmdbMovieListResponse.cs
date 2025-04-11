namespace MovieCommentsApi.Models.Dto
{
    public class TmdbMovieListResponse
    {
        public List<MovieListDto?> Results { get; set; } = new();
    }
}
