import { Component, OnInit } from '@angular/core';
import {RouterLink} from '@angular/router';
import { Player } from '../../app/core/models/player';
import { PlayerService } from '../../app/core/services/player.service';

@Component({
  selector: 'app-players',
  standalone: true,
  imports: [
    RouterLink
  ],
  templateUrl: './players.component.html',
  styleUrl: './players.component.scss'
})
export class PlayersComponent implements OnInit {
  displayName = 'Players Home';
  
  players: Player[] = [];
  
  selectedPlayer?: Player;

  constructor(
    private readonly playerService: PlayerService
  ) { }

  ngOnInit(): void {
    console.log('ngOnInit running');
    this.playerService
      .getPlayers()
      .subscribe(players => {

        console.log(players);

        this.players = players;

        console.log('Assigned length:', this.players.length);

        setTimeout(() => {
          console.log('One second later:', this.players.length);
        });

      });

    this.playerService
      .getPlayer('87c95bdb-2793-46c9-849c-2213a5c9178f')
      .subscribe(player => {

        this.selectedPlayer = player;

    });
}

}
