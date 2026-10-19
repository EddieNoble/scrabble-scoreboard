import { Injectable } from '@angular/core';
import { LeaderboardEntry } from '../models/leaderboard-entry';

@Injectable({
  providedIn: 'root'
})
export class LeaderboardService {

  getLeaderboard(): LeaderboardEntry[] {
    return [
      {
        position: 1,
        playerName: 'Peter Jones',
        gamesPlayed: 12,
        totalScore: 1845
      },
      {
        position: 2,
        playerName: 'Jane Smith',
        gamesPlayed: 12,
        totalScore: 1788
      },
      {
        position: 3,
        playerName: 'John Brown',
        gamesPlayed: 12,
        totalScore: 1644
      }
    ];
  }
}
