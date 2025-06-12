
import React, { useState } from 'react';
import axios from 'axios';
import { useNavigate } from 'react-router-dom';

const Login: React.FC = () => {
    const [name, setName] = useState<string>('');
    const [phone, setPhone] = useState<string>('');
    const navigate = useNavigate();

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        try {
            const response = await axios.post('https://localhost:7194/api/User/login', { userId: "", name, phone });
            const user = response.data;

            if (user) {
                navigate('/dashboard', { state: { user } });
            } else {
                navigate('/signup');
            }
        } catch (error) {
            navigate('/signup');
        }
    };

    return (
        <div className="container d-flex justify-content-center align-items-center" style={{ minHeight: '100vh' }}>
            <div className="card" style={{ width: '350px', backgroundColor: '#2c3e50', color: 'white', borderRadius: '10px' }}>
                <div className="card-body">
                    <h5 className="card-title text-center">Login</h5>
                    <form onSubmit={handleSubmit}>
                        <div className="mb-3">
                            <label className="form-label">Name:</label>
                            <input
                                type="text"
                                className="form-control"
                                placeholder="Insert name"
                                value={name}
                                onChange={(e) => setName(e.target.value)}
                                required
                            />
                        </div>
                        <div className="mb-3">
                            <label className="form-label">Phone:</label>
                            <input
                                type="text"
                                className="form-control"
                                placeholder="Insert phone number"
                                value={phone}
                                onChange={(e) => setPhone(e.target.value)}
                                required
                            />
                        </div>
                        <div className="d-flex justify-content-center">
                            <button type="submit" className="btn btn-light">Sign In</button>
                        </div>
                    </form>
                    <div className="text-center mt-3">
                        <a href="/signup" style={{ color: 'white', textDecoration: 'underline' }}>Sign Up</a>
                    </div>
                </div>
            </div>
        </div>
    );
};

export default Login;
