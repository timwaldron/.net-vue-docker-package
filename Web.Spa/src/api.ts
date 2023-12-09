import axios from 'axios';
import { useAccountStore } from './stores/account';

axios.interceptors.request.use((config) => {
    const accountStore = useAccountStore();

    if (accountStore.token !== null) {
        config.headers.Authorization = accountStore.token;
    }

    return config;
});