import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class BaseService {

  protected baseUrl = environment.baseUrl;

  constructor(protected HttpClient: HttpClient) { }
}
