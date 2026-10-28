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
 private readonly leaderboardService = inject(LeaderboardService);
  displayName = 'Leaderboard';

  leaderboardEntries = this.leaderboardService.leaderboardEntries;

  ngOnInit(): void {
    console.log('ngOnInit running');
    this.leaderboardService.loadLeaderboardEntries();
  }
}

