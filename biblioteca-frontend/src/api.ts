// src/api.ts
import axios from 'axios';

const api = axios.create({
    baseURL: 'http://localhost:5173/api', // Aseg�rate de que este sea el endpoint correcto para tu backend
});

export default api;
