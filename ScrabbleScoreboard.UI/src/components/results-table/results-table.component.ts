import { Component, computed, inject, OnInit, signal } from '@angular/core';
import { ResultService } from '../../app/core/services/result.service';
import { ResultDto } from '../../app/core/models/result-dto';
import { Player } from '../../app/core/models/player';
import { PlayerService } from '../../app/core/services/player.service';
import { Result } from '../../app/core/models/result';

@Component({
  selector: 'app-results-table',
  standalone: true,
  templateUrl: './results-table.component.html',
  styleUrl: './results-table.component.scss'
})
export class ResultsTableComponent implements OnInit  {
  private readonly resultService = inject(ResultService);
  private readonly playerService = inject(PlayerService);

  displayName = 'Results';

  players = signal<Player[]>([]);
  results = this.resultService.results;
  resultDtos = computed<ResultDto[]>(() =>
    this.results().map((result: Result) => ({
      id: result.id,
      playerName:
        this.players().find(player => player.id === result.playerId)?.name ??
        'Unknown player',
      score: result.score
    }))
  );

  ngOnInit(): void {
    this.resultService.loadResults();
    this.playerService.getPlayers().subscribe(players => {
      this.players.set(players);
    });
  }
}
