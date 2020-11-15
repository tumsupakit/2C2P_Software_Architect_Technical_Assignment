import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { TransactionUploadComponent } from './transaction-upload/transaction-upload.component';
import { TransactionLogComponent } from './transaction-log/transaction-log.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    TransactionUploadComponent,
    TransactionLogComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: TransactionLogComponent, pathMatch: 'full' },
      { path: 'Upload', component: TransactionUploadComponent },
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
