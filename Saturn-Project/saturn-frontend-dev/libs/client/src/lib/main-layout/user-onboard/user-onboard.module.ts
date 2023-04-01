import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EngagementComponent } from './engagement/engagement.component';
import { PersonalInfoComponent } from './personal-info/personal-info.component';
import { UserOnboardComponent } from './user-onboard.component';
import { SharedModule } from '@saturn-frontend/shared';
import { RouterModule } from '@angular/router';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { NgSelectModule } from '@ng-select/ng-select';
import { CompanyInfoComponent } from './company-info/company-info.component';
@NgModule({
  declarations: [
    EngagementComponent,
    PersonalInfoComponent,
    UserOnboardComponent,
    CompanyInfoComponent,
  ],
  imports: [
    CommonModule,
    SharedModule,
    RouterModule,
    ReactiveFormsModule,
    NgSelectModule,
    FormsModule,
  ],
  exports: [CompanyInfoComponent],
})
export class UserOnboardModule {}
