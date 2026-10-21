import {Component, inject, OnInit } from '@angular/core';
import { LeaderboardEntry } from '../../app/core/models/leaderboard-entry';
import { LeaderboardService } from '../../app/core/services/leaderboard.service';
import { ChangeDetectorRef } from '@angular/core';

@Component({
  selector: 'app-leaderboard-table',
  standalone: true,
  templateUrl: './leaderboard-table.component.html',
  styleUrl: './leaderboard-table.component.scss'
})
export class LeaderboardTableComponent {

  displayName = 'Leaderboard';

  leaderboardEntries: LeaderboardEntry[] = [];

  constructor(
    private readonly leaderboardService: LeaderboardService,
    private readonly changeDetector: ChangeDetectorRef
  ) { }

  ngOnInit(): void {
    console.log('ngOnInit running');
    this.leaderboardService
    .getLeaderboard()
      .subscribe(entries => {

        console.log(entries);
        this.leaderboardEntries = entries;
        this.changeDetector.detectChanges();

        console.log('Assigned length:', this.leaderboardEntries.length);

        setTimeout(() => {
          console.log('One second later:', this.leaderboardEntries.length);
        });

      });
    }
}
