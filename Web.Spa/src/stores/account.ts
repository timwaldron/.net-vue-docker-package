import { defineStore } from 'pinia';
import axios, { AxiosError } from 'axios';
import { jwtDecode } from 'jwt-decode';

import { Account } from '../models/account';
import { ServiceResult } from '../models/serviceResult';
import { AuthToken } from '../models/auth';

export type AccountStore = {
    account: Account | null;
    token: string | null;
};

export const useAccountStore = defineStore('account', {
    state: (): AccountStore => ({
        account: null,
        token: null,
    }),
    actions: {
        async login(email: string, password: string): Promise<ServiceResult<AuthToken>> {
            try {
                const response = (await axios.post<ServiceResult<AuthToken>>('/api/v1/auth/login', { email, password })).data;

                const decoded = jwtDecode<Account>(response.result.token);
                this.account = decoded;
                this.token = response.result.token;

                console.log('Ummm, login action?', response);
                return response;
            } catch (error: unknown) {
                return (error as AxiosError<ServiceResult<AuthToken>>).response?.data!;
            }
        },
    },
})