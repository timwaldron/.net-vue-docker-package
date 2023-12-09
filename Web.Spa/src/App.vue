<template>
    <Navbar />

    <router-view />
    
    <!-- TODO: Footer -->
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import Login from './components/Login.vue';
import Navbar from './components/Navbar.vue';

export default defineComponent({
    name: 'App',
    components: {
        Login,
        Navbar,
    },
});
</script>