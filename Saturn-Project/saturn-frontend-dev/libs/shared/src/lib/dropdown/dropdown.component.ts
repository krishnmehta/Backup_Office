import { Component, Input, Output, EventEmitter, OnInit } from '@angular/core';
import { Options } from './_models/options.model';

@Component({
  selector: 'saturn-frontend-dropdown',
  templateUrl: './dropdown.component.html',
  styleUrls: ['./dropdown.component.scss'],
})
export class DropdownComponent implements OnInit{
  @Input() dropdownId: string | undefined;
  @Input() selected: string | undefined;
  @Input() options: Options[] = [];
  @Output() currentSelectionEventEmit = new EventEmitter<string>();

  ngOnInit(): void {
      this.options.forEach((item) => {
        this.selected === item.name ? item.isActive = true : item.isActive = false;
      })
  }

  /**
   * Method to select current item from dropdown
   * @param event get selected item event
   */
  setCurrentOption(event: Event, optionIndex: number) {
    this.options.forEach((item, index) => {
      item.isActive = optionIndex === index;
    })
   const currentValue = (event.target as HTMLButtonElement).value;
   this.selected = currentValue
   this.currentSelectionEventEmit.emit(this.selected);
  }
}
