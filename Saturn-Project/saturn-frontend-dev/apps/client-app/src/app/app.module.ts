import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from '@saturn-frontend/shared';
import { environment } from '../environments/environment';
import { AppComponent } from './app.component';
import { AccountConfigModule } from '@abp/ng.account/config';
import { IdentityConfigModule } from '@abp/ng.identity/config';
import { CoreModule } from '@abp/ng.core';
import { registerLocale } from '@abp/ng.core/locale';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { MsalModule, MsalService, MSAL_INSTANCE } from '@azure/msal-angular';
import { IPublicClientApplication, PublicClientApplication } from '@azure/msal-browser';

export function MSALInstanceFactory(): IPublicClientApplication {
  return new PublicClientApplication({
    auth: {
      clientId: "24fc602c-a68e-4f2a-9f9f-dba7474958f8",
      redirectUri: "https://dev.app.stratformx.com/",
      postLogoutRedirectUri: "https://dev.app.stratformx.com/",
      authority:"https://login.microsoftonline.com/fe273177-c1fd-490b-9023-29e85a209dab"
    
    }
  });
}

@NgModule({
  declarations: [AppComponent],
  imports: [BrowserModule,
    SharedModule,
    AppRoutingModule,
    HttpClientModule,
    MsalModule,
    CoreModule.forRoot({
      environment,
      registerLocaleFn: registerLocale(),
    }),
    FormsModule, 
    ReactiveFormsModule,
    AccountConfigModule.forRoot(),
    IdentityConfigModule.forRoot(),
  ],
  bootstrap: [AppComponent],
  providers: [{
    provide: 'BASE_API_URL',
    useValue: environment.apis.default.url,
  },{
    provide: MSAL_INSTANCE,
    useFactory: MSALInstanceFactory
  },
  MsalService]
})
export class AppModule { }
