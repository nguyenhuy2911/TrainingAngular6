import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';
import { HttpErrorResponse } from '@angular/common/http/src/response';
import { UserService } from '../services/user.service';
import { UserInfo } from '../model/UserInfo';
import { handleHttpResponseError } from '../authorize/handleHttpResponse';
@Component({
    selector: 'app-root',
    templateUrl: 'user.component.html'
})
export class UserComponent implements OnInit {
    errMessage: string = "";
    alertclass: string = "";
    users: UserInfo[];
    constructor(private userService: UserService) { }
    ngOnInit() {
        this.getUsers();
    }
    getUsers(): void {
        this.userService.getUsers().subscribe(
            res => {
                this.users = res;
            },
            (err: HttpErrorResponse) => {
                this.errMessage = handleHttpResponseError(err.status);
                this.alertclass = "alert alert-danger";
            } 
        );
    }
}
