import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { RestService } from 'src/app/core/services/http/rest.service';
import { environment } from 'src/environments/environment';
import { GetSilosRequest } from '../models/requests/GetSilosRequest';
import { Observable } from 'rxjs';
import { ServiceResponse } from './ServiceResponse';

@Injectable({
  providedIn: 'root',
})
export class SiloService extends RestService {
  controllerName = 'silos';

  constructor(http: HttpClient) {
    super(http, environment.apiUrl);
  }

  public getSilos(request: GetSilosRequest): Observable<ServiceResponse> {
    return this.post(this.controllerName, request);
  }
}
