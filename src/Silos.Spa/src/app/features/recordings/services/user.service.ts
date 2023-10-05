import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { RestService } from 'src/app/core/services/http/rest.service';
import { environment } from 'src/environments/environment';
import { RegisterUserRequest } from '../models/requests/RegisterUserRequest';
import { UpdateUserRequest } from '../models/requests/UpdateUserRequest';
import { Observable } from 'rxjs';
import { ServiceResponse } from './ServiceResponse';

@Injectable({
  providedIn: 'root',
})
export class UserService extends RestService {
  controllerName = 'users';

  constructor(http: HttpClient) {
    super(http, environment.apiUrl);
  }

  public loadUserDetails(): Observable<ServiceResponse> {
    console.log('loaduserdetails');
    return this.get(this.controllerName);
  }

  public getUserStoredEvents(
    aggregateId: string
  ): Observable<ServiceResponse> {
    return this.get(this.controllerName + '/' + aggregateId + '/history');
  }

  public registerUser(
    request: RegisterUserRequest
  ): Observable<ServiceResponse> {
    return this.post(this.controllerName, request);
  }

  public updateUser(
    userId: string,
    request: UpdateUserRequest
  ): Observable<ServiceResponse> {
    return this.put(this.controllerName + '/' + userId, request);
  }
}
