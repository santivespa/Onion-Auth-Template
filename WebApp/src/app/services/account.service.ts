import { inject, Injectable } from '@angular/core';
import { CustomHttpService } from './custom-http.service';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

 
  controller = 'account';
  readonly _http = inject(CustomHttpService);

  getAccount = () => {
    return this._http.get(`${this.controller}/get-account-data`);
  }

  updateAccount = (data: any) => {
    return this._http.post(`${this.controller}/update-account`, data);
  }

  changePassword = (data: any) => {
    return this._http.post(`${this.controller}/change-password`, data);
  }
}
