
import React, { useEffect, useState } from 'react';
import axios from 'axios';
import 'bootstrap/dist/css/bootstrap.min.css';
import { useNavigate, useParams } from 'react-router-dom';

const UserHistory: React.FC = () => {
    const { userId } = useParams<{ userId: string }>();
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
                    <li key={index} className="list-group-item">
                        <div className="d-flex justify-content-between">
                            <span><strong>Question:</strong> {item.promptText}</span>
                            <span className="text-muted">{new Date(item.createdAt).toLocaleString()}</span>
                        </div>
                        <div className="mt-2">
                            <strong>AI Response:</strong> {item.response}
                        </div>
                    </li>
                ))}
            </ul>
            <div className="d-flex justify-content-between mt-4">
                <button className="btn btn-primary" style={{ backgroundColor: '#17a2b8', color: 'white' }} onClick={() => navigate(-1)}>
                    Back to Dashboard
                </button>
            </div>
        </div>
    );
};

export default UserHistory;
