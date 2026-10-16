import { Component } from '@angular/core';
import { LeaderboardTableComponent }
  from '../../components/leaderboard-table/leaderboard-table.component';
import {RouterLink} from '@angular/router';

@Component({
  selector: 'app-leaderboard',
  standalone: true,
  imports: [LeaderboardTableComponent, RouterLink],
  templateUrl: './leaderboard.component.html',
  styleUrl: './leaderboard.component.scss'
})
export class LeaderboardComponent {

  displayName = 'Leaderboard';
}
