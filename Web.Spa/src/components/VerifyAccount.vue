<template>
    <div class="flex justify-content-center flex-wrap align-items-center h-100">
        <Card style="width: 25em">
            <template #title>Verify account</template>
            <template #subtitle>Make sure to check your emails spam box if you haven't recieved an email yet.</template>

            <template #content>
                <span class=" p-float-label mt-2">
                    <InputText id="code" v-model="code" />
                    <label for="code">Verification Code</label>
                    <p v-if="outcome" class="my-0 ml-2"><small :style="`color: ${infoColor}`">{{ outcome.message }}</small></p>
                </span>
            </template>

            <template #footer>
                <div class="text-center">
                    <Button type="button" label="Login" @click="onVerifyAccount" :disabled="activity">
                        <i v-if="activity" class="pi pi-spin pi-sync"></i>
                        <span v-else>Verify Account</span>
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
import { mapActions, mapState } from 'pinia';
import { useAccountStore } from '../stores/account';
import { OperationResult } from '../models/operationResult';
import { AxiosError } from 'axios';

export default defineComponent({
    name: 'VerifyAccount',
    components: {
        Button,
        Card,
        InputText,
        Password,
    },
    data() {
        return {
            code: '',
            outcome: null as null | OperationResult,
            activity: false,
            infoColor: 'crimson',
        };
    },
    computed: {
        ...mapState(useAccountStore, ['account']),
    },
    methods: {
        async onVerifyAccount(): Promise<void> {
            try {
                this.outcome = null;
                this.activity = true;

                await this.verify(this.code);

                if (this.account?.verified === 'Y') {
                    this.$router.push('/home');
                }

            } catch (error: unknown) {
                this.outcome = (error as AxiosError<OperationResult>).response?.data!;
            } finally {
                this.activity = false;
            }
        },
        ...mapActions(useAccountStore, ['verify']),
    },
    beforeMount(): void {
        if (this.account === null) {
            this.$router.push('/login');
        }
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