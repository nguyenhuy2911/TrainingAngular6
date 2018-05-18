import { Injectable } from '@angular/core';
import {
    CanActivate, Router,
    ActivatedRouteSnapshot,
    RouterStateSnapshot,
    CanActivateChild,
    NavigationExtras,
    CanLoad, Route
} from '@angular/router';
import { AuthService } from './auth.service';
import { asNativeElements } from '@angular/core/src/debug/debug_node';
import { debug } from 'util';
@Injectable()
export class AuthGuard implements CanActivate {
    isLogin: boolean = false;
    constructor(private authService: AuthService, private router: Router) { }

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
        let url: string = state.url;
        return this.checkLogin(url);
    }

    checkLogin(url: string): boolean {
        if (this.authService.isAuthenticated()) { return true; }
        this.router.navigate(['/login']);
        return false;
    }
}

