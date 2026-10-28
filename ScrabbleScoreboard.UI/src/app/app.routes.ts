import { Routes } from '@angular/router';
import { HomeComponent } from '../pages/home/home.component';
import {LeaderboardComponent} from '../pages/leaderboard/leaderboard.component';
import {PlayersComponent} from '../pages/players/players.component';
import {GamesComponent} from '../pages/games/games.component';
import {ResultsComponent} from '../pages/results/results.component';
import { SignalComponent } from '../pages/signal/signal.component';

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
    path: 'games',
    component: GamesComponent
  },
  {
    path: 'leaderboard',
    component: LeaderboardComponent
  },
  {
    path: 'results',
    component: ResultsComponent
  },
  {
    path: 'signal',
    component: SignalComponent
  }
];
