import React from 'react';
import { useLocation } from 'react-router-dom';

const Dashboard: React.FC = () => {
    const location = useLocation();
    const user = location.state.user;

    return (
        <div>
            <h1>Hello, {user.name}!</h1>
        </div>
    );
};

export default Dashboard;
