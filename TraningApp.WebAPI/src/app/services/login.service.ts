import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';

import { Observable, of } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';
import { HttpRequest } from 'selenium-webdriver/http';
import { ResultBase } from '../model/ResultBase';
import { UserInfo } from '../model/UserInfo';
import { debug } from 'util';
const httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};
@Injectable()
export class LoginService {
    private loginUrl = 'api/token';
    private acountUrl = "api/acount";
    constructor(private http: HttpClient) { }
    login(username: string, password: string): Observable<any> {
        let params = new HttpParams();
        params = params.append('userName', username);
        params = params.append('passWord', password);
        return this.http.post(this.loginUrl, null, { headers: new HttpHeaders({ 'Content-Type': 'application/json' }), params: params }).pipe();
    }

    getLoginUser(userName: string): Observable<any> {        
        const url = `${this.acountUrl}/${userName}`;
        let token = "Bearer " + localStorage.getItem("token");
        let requestHeaders = new HttpHeaders({
            'Content-Type': 'application/json',
            'Authorization': token
        });       
        let data = this.http.get<ResultBase<UserInfo>>(url, { headers: requestHeaders }).pipe(catchError(this.handleError<any>("Login fail")));
        return data;
    }
    private handleError<T>(operation = 'operation', result?: T) {
        return (error: any): Observable<T> => {
            return of(result as T);
        };
    }
}
