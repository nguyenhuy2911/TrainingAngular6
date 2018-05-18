import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { LoginService } from '../services/login.service';
import { debug } from 'util';
import { ResultBase } from '../model/ResultBase';
@Component({
    selector: 'app-root',
    templateUrl: 'login.component.html'
})
export class LoginComponent implements OnInit {
    errorMess: string;
    constructor(private loginService: LoginService, private router: Router) { }
    ngOnInit() { }
    login(username: string, password: string): void {
        this.loginService.login(username, password).subscribe(res => {
            if (res != null && res.isSuccess ) {
                localStorage.setItem('token', res.result.token);
                localStorage.setItem('curr_acc', username);
                this.router.navigate(['/'])
            }
            else {
                this.errorMess = res.message
            }
        });
    }
}
