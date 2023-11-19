import './style.css';

import PrimeVue from 'primevue/config';
import 'primevue/resources/themes/lara-dark-teal/theme.css';
import 'primeicons/primeicons.css';
import 'primeflex/primeflex.css';

import App from './App.vue';
import { createApp } from 'vue';

const app = createApp(App);

app.use(PrimeVue);
app.mount('#app');
