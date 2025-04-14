import axios from 'axios';

const BASE_URL = 'http://localhost:5146/api/comments';

export const postComment = async (movieId: number, content: string, token: string) => {
  const response = await axios.post(
    BASE_URL,
    { movieId: Number(movieId), content },
    {
      headers: {
        Authorization: `Bearer ${token}`,
        'Content-Type': 'application/json',
      },
    }
  );
  return response.data;
};
