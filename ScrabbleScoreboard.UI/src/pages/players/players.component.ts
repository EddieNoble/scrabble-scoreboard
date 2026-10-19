import { Component, OnInit } from '@angular/core';
import {RouterLink} from '@angular/router';
import { Player } from '../../app/core/models/player';
import { PlayerService } from '../../app/core/services/player.service';
import { ChangeDetectorRef } from '@angular/core';

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
    private readonly playerService: PlayerService,
    private readonly changeDetector: ChangeDetectorRef
  ) { }

  ngOnInit(): void {
    console.log('ngOnInit running');
    this.playerService
      .getPlayers()
      .subscribe(players => {

        console.log(players);
        this.players = players;
        this.changeDetector.detectChanges();

        console.log('Assigned length:', this.players.length);

        setTimeout(() => {
          console.log('One second later:', this.players.length);
        });

      });
    }
}