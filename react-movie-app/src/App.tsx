import React, { useState, useEffect } from 'react';
import MovieSearch from './components/MovieSearch';
import Login from './components/Login';

function App() {
  const [token, setToken] = useState<string | null>(null);

  useEffect(() => {
    const stored = localStorage.getItem('token');
    if (stored) {
      setToken(stored);
    }
  }, []);

  const handleLoginSuccess = (receivedToken: string) => {
    setToken(receivedToken);
  };

  const handleLogout = () => {
    localStorage.removeItem('token');
    setToken(null);
  };

  return (
    <div>
      {token ? (
        <>
          <button onClick={handleLogout}>Change user</button>
          <MovieSearch />
          Logged on as: {token}. 
        </>
      ) : (
        <Login onLoginSuccess={handleLoginSuccess} />
      )}
    </div>
  );
}

export default App;
