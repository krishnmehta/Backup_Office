import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs';
import { AlertService } from '../_shared-services/alert.service';

@Component({
  selector: 'saturn-frontend-alert',
  templateUrl: './alert.component.html',
  styleUrls: ['./alert.component.scss'],
})
export class AlertComponent implements OnInit, OnDestroy {

  isMessageVisible: boolean;
  message: Message = new Message();
  subscription!: Subscription;

  constructor(private alertService: AlertService) {
    this.isMessageVisible = false;
  }

  /**
   * Method to call on component initialized
   */
  ngOnInit(): void {
    this.subscription = this.alertService.getMessage().subscribe(data=> {
      if(Object.keys(data).length > 0){
        this.message = data;
        this.isMessageVisible = true;
      }
      else {
        this.isMessageVisible = false;
      }
    });
  }

  /**
   * Method to call on component destroy
   */
  ngOnDestroy() {
    this.subscription.unsubscribe();
  }
}

/* Define Model */
export class Message {
  type?: string;
  text?: string;
}