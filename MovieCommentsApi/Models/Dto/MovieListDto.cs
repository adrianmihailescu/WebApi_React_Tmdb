namespace MovieCommentsApi.Models.Dto
{
    public class MovieListDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Overview { get; set; } = string.Empty;
        public string PosterPath { get; set; } = string.Empty;
        public string ReleaseDate { get; set; } = string.Empty;
        public double VoteAverage { get; set; }
        public List<string>? Genres { get; set; }
    }
}
