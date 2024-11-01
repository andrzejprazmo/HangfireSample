import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DashboardService {

  constructor() { }

  http = inject(HttpClient);

  startJob(): Observable<any> {
    return this.http.get("api/job");
  }

  getStatus(): Observable<number> {
    return this.http.get<number>("api/status");
  }
}
