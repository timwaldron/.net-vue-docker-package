// Styles
import './style.css';

// PrimeVue
import PrimeVue from 'primevue/config';
import 'primevue/resources/themes/lara-dark-teal/theme.css';
import 'primeicons/primeicons.css';
import 'primeflex/primeflex.css';

// Components
import App from './App.vue';
import Login from './components/Login.vue';
import Home from './components/Home.vue';
import CreateAccount from './components/CreateAccount.vue';

// Vue Imports
import { createApp } from 'vue';
import { createRouter, createWebHistory } from 'vue-router';

// Vue Router
const routes = [
    { path: '/', component: Home },
    { path: '/login', component: Login },
    { path: '/create-account', component: CreateAccount },
];

const router = createRouter({ history: createWebHistory(), routes });
  
// Configuring app
createApp(App)
    .use(PrimeVue)
    .use(router)
    .mount('#app');
