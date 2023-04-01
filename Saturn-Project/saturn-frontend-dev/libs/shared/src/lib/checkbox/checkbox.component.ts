import { Component, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'saturn-frontend-checkbox',
  templateUrl: './checkbox.component.html',
  styleUrls: ['./checkbox.component.scss'],
})
export class CheckboxComponent {
  @Input() className: string | undefined;     // Receive additional class 
  @Input() checkBoxId: string | undefined;    // Receive checkbox ID
  @Input() checkBoxData: string | undefined;  // Receive checkbox data 
  @Input() isChecked: boolean | undefined;    // Receive checkbox default checked status
  @Output() checkBoxEvent = new EventEmitter<boolean>;    // EventEmitter


  /**
   * Method to pass data to parent and checked/un-checked checkbox
   * @param event get event detail as parameter
   */
  checkBoxCheck(event: Event){
    if(!(event.target as HTMLElement).closest('a')){
      event.preventDefault();
      this.isChecked = !this.isChecked;
      this.checkBoxEvent.emit(this.isChecked);
    }
  }
}
