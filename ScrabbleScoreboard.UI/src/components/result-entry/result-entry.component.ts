import { Component, OnInit } from '@angular/core';
import { PlayerService } from '../../app/core/services/player.service';
import { ResultService } from '../../app/core/services/result.service';
import { Player } from '../../app/core/models/player';
import { FormsModule } from '@angular/forms';
import { ChangeDetectorRef } from '@angular/core';

@Component({
  selector: 'app-result-entry',
  templateUrl: './result-entry.component.html',
  imports: [FormsModule]
})
export class ResultEntryComponent {

  players: Player[] =[];

  selectedPlayerId = '';
  score = 0;

  constructor(
    private playerService: PlayerService,
    private resultService: ResultService,
    private changeDetector: ChangeDetectorRef
  ) {  }


    ngOnInit(): void {
    this.playerService.getPlayers().subscribe(players => {
      console.log('Players retrieved:', players);
      this.players = players;
      this.changeDetector.detectChanges();
    });
  }

  save(): void {

    const player = this.players
      .find(p => p.id === this.selectedPlayerId);

    if (!player) {
      return;
    }

    this.resultService.addResult({
      gameId: 'TEMP',
      playerName: player.name,
      score: this.score
    });

    console.log('Result added');
  }
}