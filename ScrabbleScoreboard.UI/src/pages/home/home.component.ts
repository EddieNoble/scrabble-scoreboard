import { Component } from '@angular/core';
import {InfoCardComponent} from '../../components/info-card/info-card.component';
import {NavbarComponent} from '../../components/navbar/navbar.component';
import { LeaderboardActiveComponent } from '../../components/leaderboard-active/leaderboard-active.component';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [
    InfoCardComponent,
    NavbarComponent,
    LeaderboardActiveComponent
],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent {
  displayName = 'Home';
}
