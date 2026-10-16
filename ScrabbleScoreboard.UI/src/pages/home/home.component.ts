import { Component } from '@angular/core';
import {InfoCardComponent} from '../../components/info-card/info-card.component';
import {LeaderboardTableComponent} from '../../components/leaderboard-table/leaderboard-table.component';
import {NavbarComponent} from '../../components/navbar/navbar.component';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [
    InfoCardComponent,
    LeaderboardTableComponent,
    NavbarComponent
  ],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent {
  displayName = 'Home';
}
