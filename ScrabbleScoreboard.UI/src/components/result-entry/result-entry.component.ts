import { Component, inject } from '@angular/core';
import { PlayerService } from '../../app/core/services/player.service';
import { ResultService } from '../../app/core/services/result.service';
import { Result } from '../../app/core/models/result';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-result-entry',
  templateUrl: './result-entry.component.html',
  styleUrl: './result-entry.component.scss',
  imports: [FormsModule]
})
export class ResultEntryComponent {
  private readonly resultService = inject(ResultService);
  private readonly playerService = inject(PlayerService);

  players = this.playerService.players;

  selectedPlayerId = '';
  score = 0;

    ngOnInit(): void {
      this.playerService.loadPlayers();
    }

  save(): void {

    const player = this.players().find(p => p.id === this.selectedPlayerId);

    if (!player) {
      return;
    }

    const result: Result = {
      id: crypto.randomUUID(),
      playerId: player.id,
      score: this.score
    };

    this.resultService.addResult(result)

  }
}