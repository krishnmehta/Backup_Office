import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ForgotPasswordComponent, LoginComponent, RegisterComponent, ResetPasswordComponent, MainLayoutComponent, 
         EngagementComponent, PersonalInfoComponent, UserOnboardComponent, DashboardComponent, DiagnoseComponent, DiagnoseDetailComponent, CompanyInfoComponent } 
from '@saturn-frontend/client';

const routes: Routes = [
  { 
    path: 'login',
    component: LoginComponent
  },
  {
    path: 'register',
    component: RegisterComponent
  },
  {
    path: 'forgot-password',
    component: ForgotPasswordComponent
  },
  {
    path: 'reset-password',
    component: ResetPasswordComponent
  },
  {
    path: 'app',
    component: MainLayoutComponent,
    children: [
      // Onboarding Process Routes (Sign Engagement, Personal Profile, Company Profile)
      { 
        path: 'onboarding', component: UserOnboardComponent,
        children: [
          { path: 'engagement', component: EngagementComponent },
          { path: 'personal-info', component: PersonalInfoComponent },
          { path: 'company-info', component: CompanyInfoComponent } 
        ]
      },
      // After Onboarding Process Routes (Dashboard, Diagnosis, etc.)
      { path: 'dashboard', component: DashboardComponent},
      { path: 'diagnose', component: DiagnoseComponent },
      { path: 'diagnose/:id', component: DiagnoseDetailComponent },
      { path: '', redirectTo: '/app/dashboard', pathMatch: 'full' },
    ],
  },
  {
    path: '',
    redirectTo: '/login',
    pathMatch: 'full'
  }
]

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forRoot(routes)
  ],
  exports: [
    RouterModule
  ]
})
export class AppRoutingModule { }
