// import React, { useEffect, useState } from 'react';
// import axios from 'axios';
// import 'bootstrap/dist/css/bootstrap.min.css';

// const AdminDashboard: React.FC = () => {
//     const [users, setUsers] = useState<any[]>([]);
//     const [loading, setLoading] = useState<boolean>(true);
//     const [error, setError] = useState<string | null>(null);

//     useEffect(() => {
//         const fetchUsers = async () => {
//             try {
//                 const response = await axios.get('https://localhost:7194/api/User');
//                 setUsers(response.data);
//             } catch (error) {
//                 setError('Error fetching users');
//             } finally {
//                 setLoading(false);
//             }
//         };

//         fetchUsers();
//     }, []);

//     const fetchUserHistory = async (userId: string) => {
//         try {
//             const response = await axios.get(`https://localhost:7194/api/Prompt/history?userId=${userId}`);
//             return response.data;
//         } catch (error) {
//             console.error('Error fetching user history:', error);
//             return [];
//         }
//     };

//     if (loading) {
//         return <div className="text-center mt-5">Loading...</div>;
//     }

//     if (error) {
//         return <div className="text-center mt-5 text-danger">{error}</div>;
//     }

//     return (
//         <div className="container">
//             <h1 className="text-center mt-5">Admin Dashboard</h1>
//             <h2 className="text-center mt-3">User List</h2>
//             <table className="table table-striped mt-4">
//                 <thead className="thead-dark">
//                     <tr>
//                         <th>Name</th>
//                         <th>Phone</th>
//                         <th>Prompt History</th>
//                     </tr>
//                 </thead>
//                 <tbody>
//                     {users.map(user => (
//                         <tr key={user.userId}>
//                             <td>{user.name}</td>
//                             <td>{user.phone}</td>
//                             <td>
//                                 <button className="btn btn-info" onClick={async () => {
//                                     const history = await fetchUserHistory(user.userId);
//                                     alert(`Prompt History for ${user.name}: ${history.join(', ')}`);
//                                 }}>
//                                     View History
//                                 </button>
//                             </td>
//                         </tr>
//                     ))}
//                 </tbody>
//             </table>
//         </div>
//     );
// };

// export default AdminDashboard;
import React, { useEffect, useState } from 'react';
import axios from 'axios';
import 'bootstrap/dist/css/bootstrap.min.css';
import { useNavigate } from 'react-router-dom';

const AdminDashboard: React.FC = () => {
    const [users, setUsers] = useState<any[]>([]);
    const [loading, setLoading] = useState<boolean>(true);
    const [error, setError] = useState<string | null>(null);
    const navigate = useNavigate();

    useEffect(() => {
        const fetchUsers = async () => {
            try {
                const response = await axios.get('https://localhost:7194/api/User');
                setUsers(response.data);
            } catch (error) {
                setError('Error fetching users');
            } finally {
                setLoading(false);
            }
        };

        fetchUsers();
    }, []);

    if (loading) {
        return <div className="text-center mt-5">Loading...</div>;
    }

    if (error) {
        return <div className="text-center mt-5 text-danger">{error}</div>;
    }

    return (
        <div className="container">
            <h1 className="text-center mt-5">Admin Dashboard</h1>
            <h2 className="text-center mt-3">User List</h2>
            <table className="table table-striped mt-4">
                <thead className="thead-dark">
                    <tr>
                        <th>Name</th>
                        <th>Phone</th>
                        <th>Prompt History</th>
                    </tr>
                </thead>
                <tbody>
                    {users.map(user => (
                        <tr key={user.userId}>
                            <td>{user.name}</td>
                            <td>{user.phone}</td>
                            <td>
                                <button className="btn btn-primary" onClick={() => navigate(`/user-history/${user.userId}`)}>
                                    View History
                                </button>
                            </td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    );
};

export default AdminDashboard;
