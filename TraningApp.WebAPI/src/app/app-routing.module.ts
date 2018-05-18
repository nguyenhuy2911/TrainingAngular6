import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { LoginComponent } from './login/login.component';
import { AcountComponent } from './acount/acount.component';
import { HomeComponent } from './home/home.component';
import { UserComponent } from './user/user.component';
import { AuthorizeComponent } from './authorize/authorize.component';
import { AddUserComponent } from './user/user-add.component';
import { UserDetailComponent } from './user/user-detail.component';
import { UpdateUserComponent } from './user/user-update.component';
import { PostComponent } from './post/post.component';
import { PostAddComponent } from './post/post-add.component';

import { AuthGuard } from './services/auth-guard.service';


const routes: Routes = [
    { path: 'login', component: LoginComponent },
    {
        path: '', component: AuthorizeComponent,
        canActivate: [AuthGuard],
        children: [
            { path: '', component: HomeComponent },
            { path: 'acount', component: AcountComponent },
            { path: 'user', component: UserComponent },
            { path: 'user/add', component: AddUserComponent },
            { path: 'user/update/:id', component: UpdateUserComponent },
            { path: 'user/:id', component: UserDetailComponent },
            { path: 'post', component: PostComponent },
            { path: 'post/add', component: PostAddComponent },
        ]
    },
    { path: '**', redirectTo: '/' }
];
@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule { }
