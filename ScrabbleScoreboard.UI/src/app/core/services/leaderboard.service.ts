import { inject, Injectable } from '@angular/core';
import { Observable } from "rxjs";
import { LeaderboardEntry } from '../models/leaderboard-entry';
import { HttpClient } from "@angular/common/http";

@Injectable({
  providedIn: 'root'
})
export class LeaderboardService {

  private readonly http = inject(HttpClient);

  private readonly endpoint =
    'https://localhost:7210/api/results/leaderboard';

  getLeaderboard(): Observable<LeaderboardEntry[]> {
    return this.http.get<LeaderboardEntry[]>(this.endpoint)
  }
}
