import {Component } from '@angular/core';
import { ResultService } from '../../app/core/services/result.service';
import { ResultDto } from '../../app/core/models/result-dto';
import { ChangeDetectorRef } from '@angular/core';


@Component({
  selector: 'app-results-table',
  standalone: true,
  templateUrl: './results-table.component.html',
  styleUrl: './results-table.component.scss'
})
export class ResultsTableComponent {

  displayName = 'Results';
  resultDtos: ResultDto[] = [];

  constructor(
    private readonly resultService: ResultService,
    private readonly changeDetector: ChangeDetectorRef
  ) {
    this.resultService.getResultDtos()
    .subscribe(results => {
      console.log(results);
      this.resultDtos = results;
      this.changeDetector.detectChanges();
    });
  }

}