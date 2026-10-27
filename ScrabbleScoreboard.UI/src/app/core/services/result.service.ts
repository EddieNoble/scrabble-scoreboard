import { inject, Injectable, signal } from "@angular/core";
import { Observable } from "rxjs";
import { ResultDto } from "../models/result-dto";
import { Result } from "../models/result";
import { HttpClient } from "@angular/common/http";

@Injectable({
  providedIn: 'root'
})
export class ResultService {

  private readonly http = inject(HttpClient);

    private readonly endpoint = 'https://localhost:7210/api/results';

  results = signal<Result[]>([]);

loadResults(): void {
    this.http.get<Result[]>(this.endpoint)
        .subscribe(results => {
            this.results.set(results);
        });
      }

  getResultDtos(): Observable<ResultDto[]> {
      return this.http.get<ResultDto[]>(this.endpoint);
  }

  addResult(result: Result): void {

    this.http.post<Result>(this.endpoint, result)
        .subscribe(savedResult => {

    this.results.set([...this.results(), savedResult]);
    });
  }
}