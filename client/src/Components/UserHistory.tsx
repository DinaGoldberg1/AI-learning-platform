
import React, { useEffect, useState } from 'react';
import axios from 'axios';
import 'bootstrap/dist/css/bootstrap.min.css';
import { useNavigate } from 'react-router-dom';

const UserHistory: React.FC<{ userId: string }> = ({ userId }) => {
    const [history, setHistory] = useState<any[]>([]);
    const [loading, setLoading] = useState<boolean>(true);
    const [error, setError] = useState<string | null>(null);
    const navigate = useNavigate();

    useEffect(() => {
        const fetchUserHistory = async () => {
            try {
                const response = await axios.get(`https://localhost:7194/api/Prompt/history?userId=${userId}`);
                setHistory(response.data);
            } catch (error) {
                setError('Error fetching user history');
            } finally {
                setLoading(false);
            }
        };

        fetchUserHistory();
    }, [userId]);

    if (loading) {
        return <div className="text-center mt-5">Loading...</div>;
    }

    if (error) {
        return <div className="text-center mt-5 text-danger">{error}</div>;
    }

    return (
        <div className="container">
            <h1 className="text-center mt-5">User History</h1>
            <ul className="list-group mt-4">
                {history.map((item, index) => (
                    <li key={index} className="list-group-item">{item}</li>
                ))}
            </ul>
            <button className="btn btn-primary mt-4" onClick={() => navigate(-1)}>
                Back
            </button>
        </div>
    );
};

export default UserHistory;