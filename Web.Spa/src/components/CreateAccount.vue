<template>
    <div class="flex justify-content-center flex-wrap align-items-center h-100">
        <Card style="width: 25em">
            <template #title>Create Account</template>
            <template #subtitle>Forgotten your password? <router-link to="/forgot-password">Reset here!</router-link></template>

            <template #content>
                <span class="p-float-label mb-5 mt-2">
                    <InputText id="email" v-model="email" />
                    <label for="email">Email</label>
                    <p v-if="invalidMessage" class="my-0 ml-2" id="username-help"><small class="red">Email address already in use</small></p>
                </span>
                
                <span class="p-float-label mb-5">
                    <Password id="password" toggleMask v-model="password" />
                    <label for="password">Password</label>
                </span>
                
                <span class="p-float-label">
                    <Password id="confirmPassword" toggleMask v-model="confirmPassword" />
                    <label for="confirmPassword">Confirm Password</label>
                </span>
            </template>

            <template #footer>
                <div class="flex justify-content-between flex-wrap mt-2">
                    <Button @click="onLoginClick" :disabled="activity">
                        <i v-if="activity" class="pi pi-spin pi-sync ml-2"></i>
                        <span v-else>Create Account</span>
                    </Button>
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
import { mapActions } from 'pinia';
import { useAccountStore } from '../stores/account';
import { ServiceResultStatus } from '../models/serviceResult';

export default defineComponent({
    name: 'CreateAccount',
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
            confirmPassword: '123',
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
        },
        ...mapActions(useAccountStore, ['login']),
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