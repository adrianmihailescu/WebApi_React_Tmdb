import { useState, useEffect  } from 'react';
import {searchMovies} from '../api/movieApi';
import { postComment } from '../api/commentApi';
import { Movie } from '../types/types';
import '../styles/login.css';

const MovieSearch = () => {
  const [genre, setGenre] = useState('');
  const [query, setQuery] = useState('');
  const [movies, setMovies] = useState<Movie[]>([]);
  const [error, setError] = useState('');
  const [comment, setComment] = useState('');
  const [selectedMovieId, setSelectedMovieId] = useState<number | null>(null);
  const [token, setToken] = useState<string | null>(null);

  useEffect(() => {
    const storedToken = localStorage.getItem('token');
    if (storedToken) setToken(storedToken);
  }, []);

  const handleSearch = async () => {
    if (!genre || !query) {
      setError('Both genre and query are required!');
      return;
    }

    try {
      const result = await searchMovies(genre, query);
      setMovies(result);  // Set the list of movies
      setError('');
    } catch (error) {
      setError(`[${new Date().toISOString().slice(0, 19).replace("T", " ")}] An error occurred while fetching movies.`);
    }
  };

  const handleAddComment = async (movieId: number) => {
    if (!comment || !token) {
      setError('You must be logged in and write a comment.');
      return;
    }

    try {
      console.log(`Posting comment: ${comment} for movieId: ${movieId}`);
      await postComment(movieId, comment, token);
      setComment('');
      alert('Comment posted successfully!');
    } catch (err) {
      setError(`Failed to post comment. MovieId: ${movieId}: ${err}`);
    }
  };

  return (
    <div>
      <h1>Movie Search</h1>
      <input
        type="text"
        placeholder="Enter genre (e.g., Action)"
        value={genre}
        onChange={(e) => setGenre(e.target.value)}
      />
      <input
        type="text"
        placeholder="Enter search query (e.g., Batman)"
        value={query}
        onChange={(e) => setQuery(e.target.value)}
      />
      <button onClick={handleSearch}>Search</button>

      {error && <p>{error}</p>}

      <div>
        {movies.length > 0 && (
          <ol>
            {movies.map((movie) => (
              <li key={movie.id}>
                <h2>{movie.title}</h2>
                <p>{movie.overview}</p>
                {token && token !== "guest-token" && (
                  <>
                    <textarea
                      className="login-input"
                      placeholder="Add a comment..."
                      value={selectedMovieId === movie.id ? comment : ''}
                      onChange={(e) => {
                        setSelectedMovieId(movie.id);
                        setComment(e.target.value);
                      }}
                    />
                    <button onClick={() => handleAddComment(movie.id)}>Post Comment</button>
                  </>
                )}
              </li>
            ))}
          </ol>
        )}
        {movies.length === 0 && !error && <p>No movies found.</p>}
      </div>
    </div>
  );
};

export default MovieSearch;
