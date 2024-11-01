import { Component, inject, OnDestroy } from '@angular/core';
import { DashboardService } from '@dashboard/services/dashboard.service';
import { Observable, Subscription, interval } from 'rxjs';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.css'
})
export default class DashboardComponent implements OnDestroy {

  subscriptions: Subscription[] = [];
  dashboardService = inject(DashboardService);
  progress = 0;
  connectionHub!: HubConnection;
  connectionId!: string;

  ngOnInit(): void {
  }

  startHub() {
    this.connectionHub = new HubConnectionBuilder().withUrl(`/hub`).build();
    this.connectionHub.start().then(() => {

      this.connectionHub.invoke('StartJob').then(() => {
        this.connectionHub.on('JobProgress', (progress) => {
          this.progress = progress;
        });
      })
    });

  }

  startJob() {
    this.subscriptions.push(this.dashboardService.startJob().subscribe({
      next: () => {
        this.getStatus();
      }
    }));
  }

  getStatus() {
    this.subscriptions.push(interval(1000).subscribe(x => {
      this.dashboardService.getStatus().subscribe(progress => {
        this.progress = progress;
      });
    }));
  }

  ngOnDestroy(): void {
    this.subscriptions.forEach(sub => sub.unsubscribe());
  }

}
