import React, { useState } from 'react';
import { useDispatch } from 'react-redux';
import { useNavigate } from 'react-router-dom';
// import { setUser } from '../redux/userSlice';

interface LoginResponse {
  id: string;
}

const Login: React.FC = () => {
  const dispatch = useDispatch();
  const navigate = useNavigate();
  const [name, setName] = useState<string>('');
  const [phoneNumber, setPhoneNumber] = useState<string>('');
  const [error, setError] = useState<string>('');

  const handleLogin = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    setError('');

    try {
      const response = await fetch('http://localhost:7194/api/User/login', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ name, phone: phoneNumber }),
      });

      if (!response.ok) {
        const errorData = await response.json().catch(() => null);
        const errorMessage = errorData?.message || 'שגיאה באימות המשתמש';

        if (response.status === 404) {
          navigate('/signup', { state: { name, phoneNumber } });
          return;
        }

        setError(errorMessage);
        return;
      }

      const data: LoginResponse = await response.json();

      if (data && data.id) {
        dispatch(setUser({ id: data.id, name, phoneNumber }));
        navigate('/dashboard');
      } else {
        setError('שגיאה באימות המשתמש');
      }
    } catch (err) {
      setError('שגיאה בשרת, נסה שוב מאוחר יותר');
      console.error(err);
    }
  };

  return (
    <form onSubmit={handleLogin}>
      <input
        placeholder="שם מלא"
        value={name}
        onChange={(e) => setName(e.target.value)}
      />
      <input
        placeholder="מספר טלפון"
        value={phoneNumber}
        onChange={(e) => setPhoneNumber(e.target.value)}
      />
      {error && <p style={{ color: 'red' }}>{error}</p>}
      <button type="submit">התחבר</button>
    </form>
  );
};

export default Login;
