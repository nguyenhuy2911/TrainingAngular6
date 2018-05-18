import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';

import { Observable, of } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';
import { ResultBase } from '../model/ResultBase';
import { UserInfo, RegisterUserModel } from '../model/UserInfo';
import { debug } from 'util';
@Injectable()
export class UserService {
    private acountUrl = "api/acount";
    private _requestHeaders = new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': "Bearer " + localStorage.getItem("token")
    });
    constructor(private http: HttpClient) { }

    createUser(model: RegisterUserModel): Observable<any> {
        //let params = new HttpParams();
        //params = params.append('model', model);
        let data = this.http.post(this.acountUrl, JSON.stringify(model), { headers: this._requestHeaders }).pipe();
        return data;
    }
    updateUser(model: RegisterUserModel): Observable<any> {
        //let params = new HttpParams();
        //params = params.append('model', model);
        let data = this.http.put(this.acountUrl, JSON.stringify(model), { headers: this._requestHeaders }).pipe();
        return data;
    }
    getUsers(): Observable<any> {       
        let data = this.http.get(this.acountUrl, { headers: this._requestHeaders }).pipe();
        return data;
    }
    getUser(userName: string): Observable<any> {
        const url = `${this.acountUrl}/${userName}`;        
        let data = this.http.get(url, { headers: this._requestHeaders }).pipe();
        return data;
    }
    private handleError<T>(operation = 'operation', result?: T) {
        return (error: any): Observable<T> => {
            return of(result as T);
        };
    }
}
