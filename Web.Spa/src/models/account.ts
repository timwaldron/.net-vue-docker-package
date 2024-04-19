export type Account = {
    id: string;
    email: string;
    role: Role;
    verified: 'Y' | 'N';
}

export enum Role {
    User = '0',
    Admin = '100',
}