import { Component, inject } from '@angular/core';
import { ResultsTableComponent } from '../../components/results-table/results-table.component';
import {RouterLink} from '@angular/router';

@Component({
  selector: 'app-results',
  standalone: true,
  imports: [RouterLink, ResultsTableComponent],
  templateUrl: './results.component.html',
  styleUrl: './results.component.scss'
})

export class ResultsComponent {
  displayName = 'Results';
}