
import Login from './components/Login.vue';
import Home from './components/Home.vue';
import CreateAccount from './components/CreateAccount.vue';

// Vue Imports
import { createRouter, createWebHistory } from 'vue-router';

// Vue Router
const routes = [
    { path: '/', component: Home },
    { path: '/login', component: Login },
    { path: '/create-account', component: CreateAccount },
];

export const router = createRouter({ history: createWebHistory(), routes });