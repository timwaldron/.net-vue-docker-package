import { defineStore } from 'pinia';
import axios, { AxiosError } from 'axios';
import { JwtPayload, jwtDecode } from 'jwt-decode';

import { Account } from '../models/account';
import { ServiceResult } from '../models/serviceResult';
import { AuthToken } from '../models/auth';
import { OperationOutcome, OperationResult } from '../models/operationResult';

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

                const decoded = jwtDecode<Account & JwtPayload>(response.result.token);
                this.token = response.result.token;

                this.account = {
                    id: decoded.id,
                    email: decoded.email,
                    role: decoded.role,
                    verified: decoded.verified,
                };

                localStorage.setItem('token', this.token);
                localStorage.setItem('account', JSON.stringify(this.account));

                return response;
            } catch (error: unknown) {
                return (error as AxiosError<ServiceResult<AuthToken>>).response?.data!;
            }
        },
        async verify(code: string): Promise<void> {
            const response = (await axios.get<OperationResult>('/api/v1/account/verify', { params: { code, email: this.account?.email } })).data;

            if (response.outcome === OperationOutcome.Success && this.account !== null) {
                this.account.verified = 'Y';
            }
        },
        logout(): void {
            this.account = null;
            this.token = null;
            localStorage.removeItem('token');
            localStorage.removeItem('account');
        },
        loadFromLocalStorage(): void {
            const token = localStorage.getItem('token');

            if (token) {
                const decoded = jwtDecode<Account & JwtPayload>(token);

                // TODO: I don't like this, but exp is 3 numbers smaller than Date.now()
                // Investigate this later
                const unixTime = Date.now().toString().slice(0, -3);

                if (decoded.exp! > parseInt(unixTime)) {
                    this.token = token;
                    this.account = {
                        id: decoded.id,
                        email: decoded.email,
                        role: decoded.role,
                        verified: decoded.verified,
                    };

                    localStorage.setItem('account', JSON.stringify(this.account));
                    return;
                }

                this.logout();
            }
        }
    },
});