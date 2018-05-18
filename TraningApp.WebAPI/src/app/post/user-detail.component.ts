import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Route } from '@angular/router/src/config';

import { UserService } from '../services/user.service';
import { UserInfo, RegisterUserModel } from '../model/UserInfo';
import { debug } from 'util';
import { HttpErrorResponse } from '@angular/common/http/src/response';

@Component({
    selector: 'app-root',
    templateUrl: 'user-detail.component.html'
})
export class UserDetailComponent implements OnInit {
    user: UserInfo = new UserInfo();
    constructor(private userService: UserService, private route: ActivatedRoute) { }
    ngOnInit() {
        this.getUser();
    }
    getUser() {
        let userName = this.route.snapshot.paramMap.get('id');
        this.userService.getUser(userName).subscribe(
            res => {
                this.user = res;
            },
            (err: HttpErrorResponse) => {
                console.log(err.status);
            }
        );
    }
}
