// src/Components/BookList.tsx
import React, { useState, useEffect } from 'react';
import api from '../api';

interface Book {
    id: number;
    title: string;
    author: string;
}

const BookList: React.FC = () => {
    const [books, setBooks] = useState<Book[]>([]);
    const [error, setError] = useState<string | null>(null);

    useEffect(() => {
        const fetchBooks = async () => {
            try {
                const response = await api.get('/books');

                // Verifica que la respuesta sea un array
                if (Array.isArray(response.data)) {
                    setBooks(response.data);
                } else {
                    throw new Error('La respuesta de la API no es un array.');
                }
            } catch (error) {
                console.error('Error fetching books:', error);
                setError('Error fetching books. Please try again later.');
            }
        };
        fetchBooks();
    }, []);

    if (error) {
        return <div>{error}</div>;
    }

    return (
        <div>
            <h2>List of Books</h2>
            <ul>
                {books.map((book) => (
                    <li key={book.id}>
                        {book.title} by {book.author}
                    </li>
                ))}
            </ul>
        </div>
    );
};

export default BookList;

