<template>
    <Menubar :model="menuItems">
        <template #end>
            <div class="flex align-items-center gap-2">
                <Button @click="handleLoginLogout">
                    {{ loginLogoutText }} <i class="ml-2 pi pi-user"></i>
                </Button>
            </div>
        </template>
    </Menubar>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import Menubar from 'primevue/menubar';
import InputText from 'primevue/inputtext';
import Button from 'primevue/button';
import { useAccountStore } from '../stores/account';
import { mapActions, mapState } from 'pinia';
import { Role } from '../models/account';

export default defineComponent({
    name: 'Navbar',
    components: {
        Menubar,
        InputText,
        Button,
    },
    computed: {
        loginLogoutText(): string {
            return (this.account ? 'Logout' : 'Login');
        },
        menuItems() {
            const items = [{ label: 'Home', icon: 'pi pi-home', command: () => this.$router.push('/') }];

            if (this.account) {
                items.push({ label: 'Dashboard', icon: 'pi pi-desktop', command: () => this.$router.push('/dashboard') });
            }

            if (this.account?.role === Role.Admin) {
                items.push({ label: 'Administration', icon: 'pi pi-cog', command: () => this.$router.push('/administrator') });
            }

            return items;
        },
        ...mapState(useAccountStore, ['account']),
    },
    methods: {
        handleLoginLogout(): void {
            if (this.account) {
                this.logout();
            }

            this.$router.push('/login');
        },
        ...mapActions(useAccountStore, ['logout']),
    },
});
</script>