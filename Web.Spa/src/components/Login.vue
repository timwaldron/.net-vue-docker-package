<template>
    <div class="flex justify-content-center flex-wrap align-items-center h-100">
        <Card style="width: 25em">
            <template #title>Login</template>
            <template #subtitle>Don't have an account? <router-link to="/create-account">Create one now!</router-link></template>

            <template #content>
                <span class="p-float-label mb-5 mt-2">
                    <InputText id="email" v-model="email" />
                    <label for="email">Email</label>
                </span>
                
                <span class="p-float-label">
                    <Password id="password" toggleMask v-model="password" :feedback="false" />
                    <label for="password">Password</label>
                </span>
            </template>

            <template #footer>
                <div class="flex justify-content-between flex-wrap mt-2">
                    <Button label="Login" @click="login" />
                    <Button label="Forgot my password" severity="secondary" style="margin-left: 0.5em" />
                </div>
            </template>
        </Card>
    </div>
</template>

<script lang="ts">
import axios from 'axios';

import Button from 'primevue/button';
import Card from 'primevue/card';
import InputText from 'primevue/inputtext';
import Password from 'primevue/password';

import { defineComponent } from 'vue';

export default defineComponent({
    name: 'Login',
    components: {
        Button,
        Card,
        InputText,
        Password,
    },
    data() {
        return {
            email: '',
            password: '',
        };
    },
    methods: {
        async login(): Promise<void> {
            // const response = (await axios.post<string>('/api/v1/dev', { email: this.email, password: this.password })).data;
            const response = (await axios.get<unknown>('/api/v1/dev')).data;

            console.log('The response: ', response);
        }
    }
});
</script>

<style scoped>
.h-100 {
    height: 100%;
}
</style>