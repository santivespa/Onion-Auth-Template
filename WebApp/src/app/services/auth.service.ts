import { Injectable } from '@angular/core';
import { CustomHttpService } from './custom-http.service';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private _http: CustomHttpService) { }

  controller = 'auth';

  login(model: any){
    return this._http.post(`${this.controller}/login`,model);
  }

  signup(model: any){
    return this._http.post(`${this.controller}/sign-up`,model);
  }

  recoverPassowrd(model: any){
    return this._http.post(`${this.controller}/recover-password`, model);
  }

  resetPassword(model: any){
    return this._http.post(`${this.controller}/set-password`, model);
  }

  renewToken() {
    return this._http.get(`${this.controller}/renew-token`);
  }


  roleMatch(allowedRoles:any){
    let isMatch = false;
    allowedRoles.forEach(element => {
      if(this.roleEqual(element)){
        isMatch = true;
      }
    });
    return isMatch;
  }

  get isSigned(): boolean {
    return localStorage.getItem('token') != null;
  }

  roleEqual(role: string){
    var payLoad = JSON.parse(window.atob(localStorage.getItem('token').split('.')[1]));
    var userRoles = payLoad.role;
    
    if(Array.isArray(userRoles)){
      return userRoles.some(x=>x === role);
    }else{
      return userRoles == role;
    }
  }

}
