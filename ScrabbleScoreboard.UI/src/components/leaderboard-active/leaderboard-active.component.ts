import { AfterViewInit, Component, effect, inject, OnInit, ViewChild } from '@angular/core';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSort, MatSortModule } from '@angular/material/sort';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { LeaderboardEntry } from '../../app/core/models/leaderboard-entry';
import { LeaderboardService } from '../../app/core/services/leaderboard.service';

@Component({
  selector: 'app-leaderboard-active',
  standalone: true,
  imports: [
    MatFormFieldModule,
    MatInputModule,
    MatSortModule,
    MatTableModule
  ],
  templateUrl: './leaderboard-active.component.html',
  styleUrl: './leaderboard-active.component.scss'
})
export class LeaderboardActiveComponent implements AfterViewInit, OnInit {
  private readonly leaderboardService = inject(LeaderboardService);
  private readonly leaderboardEntries = this.leaderboardService.leaderboardEntries;

  @ViewChild(MatSort) private readonly sort!: MatSort;

  readonly displayedColumns = ['position', 'playerName', 'gamesPlayed', 'totalScore'];
  readonly dataSource = new MatTableDataSource<LeaderboardEntry>([]);

  constructor() {
    this.dataSource.filterPredicate = (entry, filter) =>
      entry.playerName.toLowerCase().includes(filter);

    effect(() => {
      this.dataSource.data = this.leaderboardEntries();
    });
  }

  ngOnInit(): void {
    this.leaderboardService.loadLeaderboardEntries();
  }

  ngAfterViewInit(): void {
    this.dataSource.sort = this.sort;
  }

  applyFilter(event: Event): void {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

  serializePlayer(entry: LeaderboardEntry, index: number): string {
    return JSON.stringify({
      name: entry.playerName,
      score: entry.totalScore,
      index
    });
  }

  podiumImage(position: number): string | null {
    const images: Record<number, string> = {
      1: '/images/trophy-gold.svg',
      2: '/images/trophy-silver.svg',
      3: '/images/trophy-bronze.svg'
    };

    return images[position] ?? null;
  }

  placeLabel(position: number): string {
    const placeNames = ['first', 'second', 'third'];
    return `${placeNames[position - 1]} place`;
  }
}
