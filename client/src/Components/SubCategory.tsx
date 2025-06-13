
import React, { useEffect, useState } from 'react';
import axios from 'axios';
import 'bootstrap/dist/css/bootstrap.min.css';
import { useParams, useLocation } from 'react-router-dom';

const SubCategory: React.FC = () => {
    const { categoryId } = useParams<{ categoryId: string }>();
    const location = useLocation();
    const user = location.state.user;
    const [subCategories, setSubCategories] = useState<any[]>([]);
    const [selectedSubCategory, setSelectedSubCategory] = useState<number | null>(null);
    const [userInput, setUserInput] = useState<string>('');
    const [response, setResponse] = useState<string>('');

    useEffect(() => {
        const fetchSubCategories = async () => {
            try {
                const response = await axios.get(`https://localhost:7194/api/Category/${categoryId}/subcategories`);
                setSubCategories(response.data);
            } catch (error) {
                console.error('Error fetching subcategories:', error);
            }
        };

        fetchSubCategories();
    }, [categoryId]);

    const handlePromptSubmit = async () => {
        if (selectedSubCategory !== null) {
            try {
                const response = await axios.post(`https://localhost:7194/api/Prompt/process prompt`, { prompt: userInput }, { params: { userId: user.id } });
                setResponse(response.data);
            } catch (error) {
                console.error('Error processing prompt:', error);
            }
        }
    };

    return (
        <div className="container">
            <h1 className="text-center mt-5">Subcategories</h1>
            <div className="row mt-4">
                {subCategories.map(subCategory => (
                    <div className="col-md-4 mb-3" key={subCategory.id}>
                        <button
                            className="btn"
                            style={{ backgroundColor: '#A3C1DA', width: '100%', height: '100px' }} // צבע כחול פסטל
                            onClick={() => setSelectedSubCategory(subCategory.id)}
                        >
                            {subCategory.name}
                        </button>
                    </div>
                ))}
            </div>
            {selectedSubCategory !== null && (
                <div className="mt-4">
                    <h3>Ask a question about this subcategory:</h3>
                    <input
                        type="text"
                        className="form-control"
                        placeholder="Enter your question"
                        value={userInput}
                        onChange={(e) => setUserInput(e.target.value)}
                    />
                    <button className="btn btn-primary mt-2" onClick={handlePromptSubmit}>Submit</button> {/* צבע כחול */}
                </div>
            )}
            {response && (
                <div className="mt-4">
                    <h4>Response:</h4>
                    <p>{response}</p>
                </div>
            )}
            <div className="mt-4">
                <a href="/history" className="btn btn-info">View History</a>
            </div>
        </div>
    );
};

export default SubCategory;