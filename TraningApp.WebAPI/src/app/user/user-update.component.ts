import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';
import { UserService } from '../services/user.service';
import { UserInfo, RegisterUserModel } from '../model/UserInfo';
import { compareEqualWith } from '../user/comparepassword.directive.component';
@Component({
    selector: 'app-root',
    templateUrl: 'user-update.component.html'
})
export class UpdateUserComponent implements OnInit {
    errorMess: string;
    alertclass: string;
    form: FormGroup;

    user: UserInfo = new UserInfo();
    registerModel: RegisterUserModel = null;
    constructor(private userService: UserService, private activeRouter: ActivatedRoute, private router: Router) { }
    ngOnInit() {
        this.form = new FormGroup({
            userName: new FormControl(this.user.userName, Validators.required),
            displayName: new FormControl('', Validators.required),
            passWord: new FormControl('', Validators.required),
            rePassword: new FormControl('', [Validators.required, compareEqualWith("passWord")])
        });
        this.getUser();
    }

    getUser() {
        let userName = this.activeRouter.snapshot.paramMap.get('id');
        this.userService.getUser(userName).subscribe(res => {
            this.user = res;
        });
    }
    onSubmit() {
        this.registerModel = this.prepareSaveUser();
        this.userService.updateUser(this.registerModel).subscribe(res => {
            if (res != null && res.isSuccess) {
                this.router.navigate(['/user/' + this.registerModel.UserName])
            }
            else {
                this.errorMess = res.message
                this.alertclass = "alert alert-danger";
            }
        });
    }


    prepareSaveUser(): RegisterUserModel {
        const formModel = this.form.value;
        const saveUser: RegisterUserModel = {
            UserName: formModel.userName as string,
            FullName: formModel.displayName as string,
            Password: formModel.passWord as string,
            ConfirmPassword: formModel.rePassword as string,
        };
        return saveUser;
    }
}
