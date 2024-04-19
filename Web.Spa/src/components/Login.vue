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
                    <p v-if="invalidMessage" class="my-0 ml-2"><small class="red">{{ invalidMessage }}</small></p>
                </span>
            </template>

            <template #footer>
                <div class="flex justify-content-between flex-wrap mt-2">
                    <Button type="button" label="Login" @click="onLoginClick" :disabled="activity">
                        <i v-if="activity" class="pi pi-spin pi-sync"></i>
                        <span v-else>Login</span>
                    </Button>

                    <Button type="button" label="Forgot my password" severity="secondary" style="margin-left: 0.5em" @click="$router.push('/forgot-password')" />
                </div>
            </template>
        </Card>
    </div>
</template>

<script lang="ts">
import Button from 'primevue/button';
import Card from 'primevue/card';
import InputText from 'primevue/inputtext';
import Password from 'primevue/password';

import { defineComponent } from 'vue';
import { mapActions, mapState } from 'pinia';
import { useAccountStore } from '../stores/account';
import { ServiceResultStatus } from '../models/serviceResult';

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
            email: 'test@test.com',
            password: '123',
            invalidMessage: '',
            activity: false,
        };
    },
    methods: {
        async onLoginClick(): Promise<void> {
            this.invalidMessage = '';
            this.activity = true;

            const response = await this.login(this.email, this.password);
            this.activity = false;

            if (response.status === ServiceResultStatus.Failure) {
                this.invalidMessage = response.messages[0].message;
            }

            if (this.account()?.verified === 'N') {
                this.$router.push('/verify-account');
            }

            if (this.account()?.verified === 'Y') {
                this.$router.push('/home');
            }
        },
        ...mapActions(useAccountStore, ['login']),
        ...mapState(useAccountStore, {
            account: 'account',
        }),
    }
});
</script>

<style scoped>
.h-100 {
    height: 100%;
}

.red {
    color: crimson;
}
</style>