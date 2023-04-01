import { Injectable } from '@angular/core';
import { Router, NavigationStart } from '@angular/router';
import { Subject, Observable } from 'rxjs';
@Injectable({
  providedIn: 'root'
})
export class AlertService {

  subject = new Subject<MessageData>(); //Declare Subject
  showAfterNavigationChange: boolean;

  constructor(private router: Router){
    this.showAfterNavigationChange = false;
    router.events.subscribe(event=> {
      if(event instanceof NavigationStart){
        if (this.showAfterNavigationChange) {
            this.showAfterNavigationChange = false;
        } else {
            this.subject.next({}); //Hide the notification on navigation change
        }
      }
    })
  } 

  /**
   * Method to show success message
   * @param message get success message
   * @param showAfterNavigationChange get boolean value to manage notification after navigation change
   */
  success(message: string, showAfterNavigationChange = false): void {
    this.showAfterNavigationChange = showAfterNavigationChange;
    this.subject.next({ type: 'success', text: message });
  }

  /**
   * Method to show error message
   * @param message get error message
   * @param showAfterNavigationChange get boolean value to manage notification after navigation change
   */
  error(message: string, showAfterNavigationChange = false): void{
    this.showAfterNavigationChange = showAfterNavigationChange;
    this.subject.next({ type: 'danger', text: message});
  }
  
  /**
   * Method to Observe subject
   */
  getMessage(): Observable<MessageData> {
    return this.subject.asObservable();
  }
}

/* Define Model */
export class MessageData {
  type?: string;
  text?: string;
}
