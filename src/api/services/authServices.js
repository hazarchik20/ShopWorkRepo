import api from '@api/axios.config.js';

export const authService = {
    register: async (data) => {
        return await api.post('/auth/register', {
            name: data.name,
            email: data.email,
            password: data.password,
            confirmPassword: data.confirmPassword
        });
    },

    login: async (data) => {
        return await api.post('/auth/login', {
            email: data.email,
            password: data.password
        });
    },

    googleLogin: async (idToken) => {
        return await api.post('/auth/google', {
            idToken: idToken
        });
    },

    logout: async () => {
        return await api.post('/auth/logout');
    },

    getMe: async () => {
        return await api.get('/auth/me');
    },

    refreshToken: async () => {
        return await api.post('/auth/refresh');
    }
};