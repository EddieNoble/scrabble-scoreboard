import { Component } from '@angular/core';
import {InfoCardComponent} from '../../components/info-card/info-card.component';
import {LeaderboardComponent} from '../../components/leaderboard/leaderboard.component';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [
    InfoCardComponent,
    LeaderboardComponent
  ],
  templateUrl: './home.component.html',
  styleUrl: './home.component.sscss'
})
export class HomeComponent {
  displayName = 'Home';
}
