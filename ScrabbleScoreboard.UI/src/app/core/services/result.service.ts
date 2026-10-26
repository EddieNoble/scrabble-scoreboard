import { inject, Injectable, signal } from "@angular/core";
import { Observable } from "rxjs";
import { ResultDto } from "../models/result-dto";
import { HttpClient } from "@angular/common/http";

@Injectable({
  providedIn: 'root'
})
export class ResultService {

  private readonly http = inject(HttpClient);

    private readonly endpoint = 'https://localhost:7210/api/results';

  results = signal<ResultDto[]>([]);

loadResults(): void {
    this.http.get<ResultDto[]>(this.endpoint)
        .subscribe(results => {
            this.results.set(results);
        });
      }

  getResultDtos(): Observable<ResultDto[]> {
      return this.http.get<ResultDto[]>(this.endpoint);
  }

  //   addResult(result: ResultDto): Observable<ResultDto> {
  //     return this.http.post<ResultDto>(this.endpoint, result);
  // }

  addResult(result: ResultDto): void {

    this.http.post<ResultDto>(this.endpoint, result)
        .subscribe(savedResult => {

            this.results.update(existing => [
                ...existing,
                savedResult
            ]);

        });
      }
}