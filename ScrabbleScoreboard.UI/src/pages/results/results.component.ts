import { Component, signal } from '@angular/core';
import { ResultsTableComponent } from '../../components/results-table/results-table.component';
import { ResultEntryComponent } from '../../components/result-entry/result-entry.component';
import {RouterLink} from '@angular/router';

@Component({
  selector: 'app-results',
  standalone: true,
  imports: [RouterLink, ResultsTableComponent, ResultEntryComponent],
  templateUrl: './results.component.html',
  styleUrl: './results.component.scss'
})

export class ResultsComponent {
  displayName = 'Results';

    displayMode = signal(true);

  toggleMode(): void {
    this.displayMode.update(current => !current);
  }
}