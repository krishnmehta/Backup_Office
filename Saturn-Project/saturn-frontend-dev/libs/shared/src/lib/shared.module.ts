import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpService } from './_shared-services/http.service';
import { HttpClientModule } from '@angular/common/http';
import { BootstrapModule } from './_shared-modules/bootstrap.module';
import { AlertComponent } from './alert/alert.component';
import { RouterModule } from '@angular/router';
import { CheckboxComponent } from './checkbox/checkbox.component';
import { NgSelectModule } from '@ng-select/ng-select';
import { DropdownComponent } from './dropdown/dropdown.component';
import { LoaderComponent } from './loader/loader.component';
import { LowercaseHyphenPipe } from './_shared-pipes/lowercase-hyphen.pipe';
@NgModule({
  imports: [
    CommonModule,
    HttpClientModule,
    BootstrapModule,
    RouterModule,
    NgSelectModule,
  ],
  providers: [HttpService],
  declarations: [
    AlertComponent,
    CheckboxComponent,
    DropdownComponent,
    LoaderComponent,
    LowercaseHyphenPipe,
  ],
  exports: [
    AlertComponent,
    CheckboxComponent,
    DropdownComponent,
    LoaderComponent,
    LowercaseHyphenPipe,
  ],
})
export class SharedModule {}
