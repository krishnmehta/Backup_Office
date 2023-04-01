import { Injectable } from '@angular/core';
import { Subject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LoaderService {

  subLoader = new Subject<boolean>();   // Loader Subject

  /**
   * Method to show loader
   */
  showLoader(): void {
    this.subLoader.next(true);
  }

  /**
   * Method to hide loader
   */
  hideLoader(): void{
    this.subLoader.next(false);
  }

  /**
   * Method to Observe loader subject
   */
  getLoader(): Observable<boolean>{
    return this.subLoader.asObservable();
  }
}
