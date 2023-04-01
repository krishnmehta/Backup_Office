import { Component } from '@angular/core';

@Component({
  selector: 'saturn-frontend-diagnose-detail',
  templateUrl: './diagnose-detail.component.html',
  styleUrls: ['./diagnose-detail.component.scss'],
})
export class DiagnoseDetailComponent {

  stepCount: number;
  isQuestionnaireFinish: boolean;
  isDataUploadFinish: boolean;

  constructor() {
    this.stepCount = 1;
    this.isQuestionnaireFinish = false;
    this.isDataUploadFinish = false;
  }

  /**
   * Method to redirect user to step
   * @param step get step count. Based on that display respective step.
   */
  redirectStep(step: number): void {
    switch(step){
      case 1:
        this.stepCount = step;
        break;
      case 2:
        this.stepCount = step;
        break;
      default:
        break;
    }
  }
}