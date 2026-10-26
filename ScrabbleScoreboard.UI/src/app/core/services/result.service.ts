import { inject, Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { ResultDto } from "../models/result-dto";
import { HttpClient } from "@angular/common/http";

@Injectable({
  providedIn: 'root'
})
export class ResultService {

  private readonly http = inject(HttpClient);

    private readonly endpoint = 'https://localhost:7210/api/results';

  getResultDtos(): Observable<ResultDto[]> {
      return this.http.get<ResultDto[]>(this.endpoint);
  }

    addResult(result: ResultDto): Observable<ResultDto> {
      return this.http.post<ResultDto>(this.endpoint, result);
  }
}