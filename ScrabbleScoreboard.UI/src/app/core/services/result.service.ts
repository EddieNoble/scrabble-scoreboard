import { inject, Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { ResultDto } from "../models/result-dto";
import { HttpClient } from "@angular/common/http";

@Injectable({
  providedIn: 'root'
})
export class ResultService {
  public getResultDtos(): ResultDto[] {
    return [
        { gameId: '1', playerName: 'Alice', score: 100 },
        { gameId: '2', playerName: 'Bob', score: 150 },
        { gameId: '3', playerName: 'Charlie', score: 200 },
        { gameId: '4', playerName: 'David', score: 250 },
        { gameId: '5', playerName: 'Eve', score: 300 }
    ];
  }
}