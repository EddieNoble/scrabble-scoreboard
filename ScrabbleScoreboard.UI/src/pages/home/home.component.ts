import { Component } from '@angular/core';
import {NavbarComponent} from '../../components/navbar/navbar.component';
import { LeaderboardActiveComponent } from '../../components/leaderboard-active/leaderboard-active.component';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [
    NavbarComponent,
    LeaderboardActiveComponent
],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent {
  displayName = 'Home';
}
