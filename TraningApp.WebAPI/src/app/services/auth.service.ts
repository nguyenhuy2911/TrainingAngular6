import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { debug } from 'util';
@Injectable()
export class AuthService {
    constructor() { }
    public isAuthenticated(): boolean {
        const jwtHelper = new JwtHelperService();
        const token = localStorage.getItem('token');
        const isExpired = jwtHelper.isTokenExpired(token);
        return isExpired ? false : true;
    }
}

