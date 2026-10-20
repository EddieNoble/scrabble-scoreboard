import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { PipelineHealthMessage } from '../models/pipeline-health-message';

@Injectable({
  providedIn: 'root'
})
export class MessagingService {

  private readonly http =
    inject(HttpClient);

  private readonly endpoint =
    'https://localhost:7210/api/messaging';

  public runHealthCheck(): Observable<any> {

    return this.http.post(
      `${this.endpoint}/health`,
      {});

  }

  public getHealthMessages(): Observable<PipelineHealthMessage[]> {

  return this.http.get<PipelineHealthMessage[]>(
    `${this.endpoint}/health/messages`
  );
}
}