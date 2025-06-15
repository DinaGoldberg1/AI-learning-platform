
import React, { useEffect, useState } from 'react';
import axios from 'axios';
import 'bootstrap/dist/css/bootstrap.min.css';
import { useLocation } from 'react-router-dom';

const Dashboard: React.FC = () => {
    const location = useLocation();
    const user = location.state.user;
    const [categories, setCategories] = useState<any[]>([]);
    const [subCategories, setSubCategories] = useState<any[]>([]);
    const [selectedSubCategory, setSelectedSubCategory] = useState<number | null>(null);
    const [selectedCategory, setSelectedCategory] = useState<number | null>(null);
    const [userInput, setUserInput] = useState<string>('');
    const [response, setResponse] = useState<string>('');

    useEffect(() => {
        const fetchCategories = async () => {
            try {
                const response = await axios.get('https://localhost:7194/api/Category');
                setCategories(response.data);
            } catch (error) {
                console.error('Error fetching categories:', error);
            }
        };

        fetchCategories();
    }, []);

    const handleSubCategoryClick = async (categoryId: number) => {
        setSelectedCategory(categoryId);
        
        try {
            const response = await axios.get(`https://localhost:7194/api/Category/${categoryId}/subcategories`);
            setSubCategories(response.data);
        } catch (error) {
            console.error('Error fetching subcategories:', error);
        }
    };

    // const handlePromptSubmit = async () => {
    //     if (selectedSubCategory !== null && userInput.trim() !== '') {
    //         try {
    //             const promptDto = {
    //                 UserId: user.id,
    //                 CategoryId: selectedCategory,
    //                 SubCategoryId: selectedSubCategory,
    //                 PromptText: userInput,
    //                 Response: "",
    //                 CreatedAt: new Date().toISOString(),
    //             };

    //             const response = await axios.post(
    //                 `https://localhost:7194/api/Prompt/process-prompt`,
    //                 promptDto,
    //                 {
    //                     headers: {
    //                         'Content-Type': 'application/json',
    //                     }
    //                 }
    //             );

    //             setResponse(response.data);
    //         } catch (error) {
    //             console.error('Error processing prompt:', error);
    //             setResponse('Error processing prompt. Please check your connection or try again later.');
    //         }
    //     } else {
    //         setResponse('Please select a subcategory and enter a question before submitting.');
    //     }
    // };
const handlePromptSubmit = async () => {
    if (selectedSubCategory !== null && userInput.trim() !== '') {
        
        try {
            const promptDTO = {
                userId: user.id, 
                categoryId: selectedCategory,
                subCategoryId: selectedSubCategory,
                promptText: userInput,
                response: "", 
                createdAt: new Date().toISOString(),
            };

            const response = await axios.post(
                `https://localhost:7194/api/Prompt/process-prompt`, 
                promptDTO,
                {
                    headers: {
                        'Content-Type': 'application/json',
                    }
                }
            );

            setResponse(response.data);
        } catch (error) {
            console.error('Error processing prompt:', error);
            setResponse('Error processing prompt. Please check your connection or try again later.');
        }
    } else {
        setResponse('Please select a subcategory and enter a question before submitting.');
    }
};
    return (
        <div className="container">
            <h1 className="text-center mt-5">Hello, {user.name}!</h1>
            <h2 className="text-center mt-3">WHAT WOULD YOU LIKE TO LEARN TODAY?</h2>
            <div className="row mt-4">
                {categories.map((category, index) => (
                    <div className="col-md-3 mb-3" key={category.id}>
                        <button
                            className="btn"
                            style={{
                                width: '100%',
                                backgroundColor: `hsl(${210 + index * 10}, 70%, 70%)`, 
                                height: '100px',
                                fontSize: '18px'
                            }}
                            onClick={() => handleSubCategoryClick(category.id)}
                        >
                            {category.name}
                        </button>
                    </div>
                ))}
            </div>
            {subCategories.length > 0 && (
                <div className="mt-4">
                    <h3>Select a Subcategory:</h3>
                    <div className="row">
                        {subCategories.map(subCategory => (
                            <div className="col-md-2 mb-3" key={subCategory.id}>
                                <button className="btn" style={{ backgroundColor: `hsl(210, 70%, 80%)`, width: '100%' }} onClick={() => setSelectedSubCategory(subCategory.id)}>
                                    {subCategory.name}
                                </button>
                            </div>
                        ))}
                    </div>
                </div>
            )}
            {selectedSubCategory !== null && (
                <div className="mt-4">
                    <h3>Ask AI about this sub category:</h3>
                    <input
                        type="text"
                        className="form-control"
                        placeholder="Enter your question"
                        value={userInput}
                        onChange={(e) => setUserInput(e.target.value)}
                    />
                    <button className="btn btn-primary mt-2" onClick={handlePromptSubmit}>Send</button>
                </div>
            )}
            {response && (
                <div className="mt-4">
                    <h4>Response:</h4>
                    <p>{response}</p>
                </div>
            )}
            <div className="mt-4">
                <a href={`/user-history/${user.id}`} className="btn btn-info">View History</a>
            </div>
        </div>
    );
};

export default Dashboard;
