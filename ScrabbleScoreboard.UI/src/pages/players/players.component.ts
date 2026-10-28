import { Component, inject, OnInit, signal } from '@angular/core';
import {RouterLink} from '@angular/router';
import { Player } from '../../app/core/models/player';
import { PlayerService } from '../../app/core/services/player.service';
import { ChangeDetectorRef } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CreatePlayerRequest } from '../../app/core/models/create-player-request';

@Component({
  selector: 'app-players',
  standalone: true,
  imports: [
    RouterLink,
    FormsModule
  ],
  templateUrl: './players.component.html',
  styleUrl: './players.component.scss'
})
export class PlayersComponent implements OnInit {
  displayName = 'Players Home';
    private readonly playerService = inject(PlayerService);


  public newPlayerName = '';
  public newPlayerEmail = '';

  players = this.playerService.players;
  selectedPlayer?: Player;

  ngOnInit(): void {
    console.log('ngOnInit running');
    this.playerService.loadPlayers();
  }

public addPlayer(): void {

    const player: CreatePlayerRequest = {
        name: this.newPlayerName,
        emailAddress: this.newPlayerEmail
    };

    this.playerService.addPlayer(player);
    this.newPlayerName = '';
    this.newPlayerEmail = '';
  }
}