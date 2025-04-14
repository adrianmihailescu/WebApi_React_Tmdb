import React, { useState } from 'react';
import { login } from '../api/authApi';
import '../styles/login.css';

export {};

type Props = {
  onLoginSuccess: (token: string) => void;
};

const Login: React.FC<Props> = ({ onLoginSuccess }) => {
  const [username, setUsername] = useState('');
  const [password, setPassword] = useState('');
  const [error, setError] = useState('');

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault(); // Prevent the form from reloading the page
  
    try {
      const token = await login(username, password);
      localStorage.setItem('token', token);
      setError('');
      onLoginSuccess(token);
    } catch (err) {
      setError(err as string);
    }
  };
  
  const handleContinueAsGuest = async (e: React.FormEvent) => {
    e.preventDefault(); // Prevent the form from reloading the page
  
    try {
      const token = "guest-token"; // Simulate a guest token
      localStorage.setItem('token', "guest-token"); // Set a guest token in local storage");
      setError('');
      onLoginSuccess(token); // Call the function to continue as guest
    } catch (err) {
      setError(err as string);
    }
  };

  return (
    <div className="login-container">
      <form className="login-form">
        <h1>Login</h1>
        <input
          type="text"
          className="login-input"
          placeholder="Username"
          value={username}
          onChange={(e) => setUsername(e.target.value)}
        />
        <input
          type="password"
          className="login-input"
          placeholder="Password"
          value={password}
          onChange={(e) => setPassword(e.target.value)}
        />
        <button className="login-button" onClick={handleSubmit} type="submit">Login</button>
        <br />
        or <br />        
        <button className="login-button" onClick={handleContinueAsGuest} type="submit">Continue as guest</button>
        {error && <p className="error-message">{error}</p>}
      </form>
    </div>
  );
};

export default Login;
