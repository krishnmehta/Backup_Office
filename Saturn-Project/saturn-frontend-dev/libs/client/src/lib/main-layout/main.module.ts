import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { MainLayoutComponent } from './main-layout.component';
import { SideNavigationComponent } from './side-navigation/side-navigation.component';
import { HeaderComponent } from './header/header.component';
import { SharedModule } from '@saturn-frontend/shared';
import { UserOnboardModule } from './user-onboard/user-onboard.module';
import { DashboardComponent } from './dashboard/dashboard.component';
import { DiagnoseComponent } from './diagnose/diagnose.component';
import { DiagnoseDetailComponent } from './diagnose/diagnose-detail/diagnose-detail.component';
import { QuestionnaireComponent } from './diagnose/diagnose-detail/questionnaire/questionnaire.component';
import { DataUploadComponent } from './diagnose/diagnose-detail/data-upload/data-upload.component';
import { PowerBIEmbedModule } from 'powerbi-client-angular';

@NgModule({
  declarations: [
    MainLayoutComponent,
    SideNavigationComponent,
    HeaderComponent,
    DashboardComponent,
    DiagnoseComponent,
    DiagnoseDetailComponent,
    QuestionnaireComponent,
    DataUploadComponent,
  ],
  imports: [CommonModule, RouterModule, SharedModule, UserOnboardModule, PowerBIEmbedModule],
  exports: [
    MainLayoutComponent,
    SideNavigationComponent,
    HeaderComponent,
    DashboardComponent,
    DiagnoseComponent,
    DiagnoseDetailComponent,
    QuestionnaireComponent,
    DataUploadComponent,
  ],
})
export class MainModule {}
