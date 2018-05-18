import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { HttpModule } from '@angular/http';
import { JwtModule } from '@auth0/angular-jwt';

import { AppRoutingModule } from './app-routing.module';

import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { AcountComponent } from './acount/acount.component';
import { HomeComponent } from './home/home.component';
import { AuthorizeComponent } from './authorize/authorize.component';
import { UserComponent } from './user/user.component';
import { AddUserComponent } from './user/user-add.component';
import { UserDetailComponent } from './user/user-detail.component';
import { UpdateUserComponent } from './user/user-update.component';
import { PostComponent } from './post/post.component';
import { PostAddComponent } from './post/post-add.component';

import { LoginService } from './services/login.service';
import { AuthGuard } from './services/auth-guard.service';
import { AuthService } from './services/auth.service';
import { UserService } from './services/user.service';
import { PostService } from './services/post.service';



@NgModule({
    imports: [
        BrowserModule,
        FormsModule,
        ReactiveFormsModule,
        AppRoutingModule,
        HttpClientModule,
        HttpModule,
        JwtModule
    ],
    declarations: [
        AppComponent,
        LoginComponent,
        AcountComponent,
        HomeComponent,
        AuthorizeComponent,
        UserComponent,
        AddUserComponent,
        UserDetailComponent,
        UpdateUserComponent,
        PostComponent,
        PostAddComponent
    ],
    providers: [
        LoginService,
        AuthGuard,
        AuthService,
        UserService,
        PostService
    ],
    bootstrap: [AppComponent]
})
export class AppModule { }
