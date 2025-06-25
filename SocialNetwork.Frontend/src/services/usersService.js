import axios from 'axios';

const API_BASE = import.meta.env.VITE_ALGEBRA_SOCIAL_NETWORK_BASE_URL;

const getAuthConfig = () => ({
    headers: {
        Authorization: `Bearer ${localStorage.getItem("token")}`
    }
});

export const getAllUsers = async () => {
    try {
        return await axios.get(`${API_BASE}/users`, getAuthConfig());
    } catch {
        console.error(`Error getting all users from API: ${API_BASE}`);
        return e;
    }
};

export const updateUser = async (userId, userData) => {
    try {
        return await axios.put(`${API_BASE}/users/${userId}`, userData, getAuthConfig());
    } catch (e) {
        console.log(`Failed to update user: ${e}`);
        return e;
    }
};

export const getUserById = async (userId) => {
    try {
        return await axios.delete(`${API_BASE}/users/${userId}`, getAuthConfig());
    } catch (e) {
        console.log(`Failed to get user with id: ${userId}, ex: ${e}`);
        return e;
    }
};
