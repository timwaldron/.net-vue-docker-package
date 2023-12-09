// Styles
import './style.css';

// PrimeVue
import PrimeVue from 'primevue/config';
import 'primevue/resources/themes/lara-dark-teal/theme.css';
import 'primeicons/primeicons.css';
import 'primeflex/primeflex.css';

// Components
import App from './App.vue';

// Vue Imports
import { createApp } from 'vue';
import { router } from './router';
import { store } from './stores';
import './api';

// Configuring app
createApp(App)
    .use(PrimeVue)  // PrimeVue component library
    .use(router)    // Vue-Router containing routes
    .use(store)     // State management system
    .mount('#app');