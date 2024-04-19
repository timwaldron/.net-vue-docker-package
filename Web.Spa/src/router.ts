// Vue Imports
import { createRouter, createWebHistory } from 'vue-router';

// Components
import Login from './components/Login.vue';
import Home from './components/Home.vue';
import CreateAccount from './components/CreateAccount.vue';
import VerifyAccount from './components/VerifyAccount.vue';

// Vue Router
const routes = [
    { path: '/', component: Home },
    { path: '/login', component: Login },
    { path: '/create-account', component: CreateAccount },
    { path: '/verify-account', component: VerifyAccount },
];

export const router = createRouter({ history: createWebHistory(), routes });