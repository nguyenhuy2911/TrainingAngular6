import { Component, OnInit } from '@angular/core';
import { LoginService } from '../services/login.service';
import { UserInfo } from '../model/UserInfo';
import { debug } from 'util';
@Component({
    selector: 'app-root',
    templateUrl: 'home.component.html'
})
export class HomeComponent implements OnInit {
    user: UserInfo = new UserInfo();
    constructor(private loginService: LoginService) { }
    ngOnInit() {
        this.getLoginAcount();
    }
    getLoginAcount(): void {
        this.loginService.getLoginUser(localStorage.getItem("curr_acc")).subscribe(res => {           
            this.user = res;
        });
    }
}
