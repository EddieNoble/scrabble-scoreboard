import { Component, OnInit } from '@angular/core';
import {RouterLink} from '@angular/router';
import { Player } from '../../app/core/models/player';
import { PlayerService } from '../../app/core/services/player.service';
import { ChangeDetectorRef } from '@angular/core';
import { FormsModule } from '@angular/forms';

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
  
  public newPlayerName = '';
  public newPlayerEmail = '';

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
      });
    }

    public addPlayer(): void {
    console.log({
        name: this.newPlayerName,
        emailAddress: this.newPlayerEmail
    });


    // public addPlayer(): void {

    //     const player: Player = {
    //         id: crypto.randomUUID(),
    //         name: this.newPlayerName,
    //         emailAddress: this.newPlayerEmail
    //     };

    //     this.playerService
    //         .addPlayer(player)
    //         .subscribe(savedPlayer => {

    //             this.players.push(savedPlayer);

    //             this.newPlayerName = '';
    //             this.newPlayerEmail = '';

    //         });
    //}

  }
}