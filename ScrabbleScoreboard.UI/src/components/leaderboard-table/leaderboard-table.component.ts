import {Component, inject } from '@angular/core';
import { LeaderboardEntry } from '../../app/core/models/leaderboard-entry';
import { LeaderboardService } from '../../app/core/services/leaderboard.service';

@Component({
  selector: 'app-leaderboard-table',
  standalone: true,
  templateUrl: './leaderboard-table.component.html',
  styleUrl: './leaderboard-table.component.scss'
})
export class LeaderboardTableComponent {

  displayName = 'Leaderboard';

  private leaderboardService =
    inject(LeaderboardService);

  leaderboardEntries: LeaderboardEntry[] =
    this.leaderboardService.getLeaderboard();

}
