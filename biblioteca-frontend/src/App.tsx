import React from 'react';
import './App.css';
import BookList from './Components/BookList';

const App: React.FC = () => {
    return (
        <div className="App">
            <header className="App-header">
                <h1>Biblioteca</h1>
                <BookList />
            </header>
        </div>
    );
};

export default App;
