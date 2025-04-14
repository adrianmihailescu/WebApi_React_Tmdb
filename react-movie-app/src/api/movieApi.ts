import axios from 'axios';

// API base URL
const BASE_URL = 'http://localhost:5146/api/movies';

// Function to search movies
export const searchMovies = async (genre: string, query: string) => {
  try {
    const response = await axios.get(`${BASE_URL}/search/`, {
      params: {
        genre,
        query,
      },
    });
    return response.data; // Return the response data
  } catch (error) {
    console.error('Error searching for movies:', error);
    throw error;
  }
};

// Function to get movie details
export const getMovieDetails = async (movieId: string) => {
  try {
    const response = await axios.get(`${BASE_URL}/${movieId}`);
    return response.data;  // Return the movie details
  } catch (error) {
    console.error('Error fetching movie details:', error);
    throw error;
  }
};
