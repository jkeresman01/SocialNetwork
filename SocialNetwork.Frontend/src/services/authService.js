import axios from 'axios';

const API_BASE = import.meta.env.VITE_ALGEBRA_SOCIAL_NETWORK_BASE_URL;

export const login = async (credentials) => {
    try {
        return await axios.post(`${API_BASE}/auth/login`, credentials);
    } catch (e) {
        console.log('login error', e);
        return e;
    }
};

export const register = async (data) => {
    try {
        return await axios.post(`${API_BASE}/auth/register`, data);
    } catch (e) {
        console.log('register error', e);
        return e;
    }
};