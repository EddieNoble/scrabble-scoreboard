import { Routes } from '@angular/router';
import { HomeComponent } from '../pages/home/home.component';
import {LeaderboardComponent} from '../pages/leaderboard/leaderboard.component';
import {PlayersComponent} from '../pages/players/players.component';
import {MatchesComponent} from '../pages/matches/matches.component';

export const routes: Routes = [
  {
    path: '',
    component: HomeComponent
  },
  {
    path: 'players',
    component: PlayersComponent
  },
  {
    path: 'matches',
    component: MatchesComponent
  },
  {
    path: 'leaderboard',
    component: LeaderboardComponent
  }
];
