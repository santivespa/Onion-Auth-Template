import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class CustomHttpService {


  constructor(private http: HttpClient) { }

  get( query: string ){
    return this.http.get(environment.base_url +query);
  }

  post( query: string, body?:any ){
    return this.http.post(environment.base_url+query,body);
  }

  put( query: string, body?:any ){
    return this.http.post(environment.base_url+query,body);
  }

  delete( query:string ){
    return this.http.get(environment.base_url+query );
  }
}
