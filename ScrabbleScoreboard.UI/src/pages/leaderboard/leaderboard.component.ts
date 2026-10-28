import { Component, inject } from '@angular/core';
import { LeaderboardTableComponent } from '../../components/leaderboard-table/leaderboard-table.component';
import { LeaderboardActiveComponent } from '../../components/leaderboard-active/leaderboard-active.component';
import {RouterLink} from '@angular/router';
import { MessagingService } from '../../app/core/services/messaging.service';
import { ChangeDetectorRef } from '@angular/core';

@Component({
  selector: 'app-leaderboard',
  standalone: true,
  imports: [LeaderboardActiveComponent, RouterLink],
  templateUrl: './leaderboard.component.html',
  styleUrl: './leaderboard.component.scss'
})

export class LeaderboardComponent {
  displayName = 'Leaderboard';

  private readonly messagingService = inject(MessagingService);

  public pipelineSendStatus = 'Unknown';
  public pipelineRetrieveStatus = 'Unknown';

  constructor(
      private readonly changeDetector: ChangeDetectorRef
    ) { }

  public runSendHealthCheck(): void {
    this.messagingService
      .runHealthCheck()
      .subscribe({
        next: () => {
          this.pipelineSendStatus = 'Health Send Test Passed';
          this.changeDetector.detectChanges();
        },
        error: error =>  {
          console.error(error);
          this.pipelineSendStatus = 'Health Send Test Failed';
          this.changeDetector.detectChanges();
        }
      });
  }
  
    public runRetrieveHealthCheck(): void {
    this.messagingService
    .getHealthMessages()
      .subscribe({
        next: () => {
          this.pipelineRetrieveStatus = 'Health Retrieve Test Passed';
          this.changeDetector.detectChanges();
        },
        error: error =>  {
          console.error(error);
          this.pipelineRetrieveStatus = 'Health Retrieve Test Failed';
          this.changeDetector.detectChanges();
        }
      });
    }
}