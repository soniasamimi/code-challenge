import { BrowserModule } from '@angular/platform-browser';
import { APP_INITIALIZER, NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { ToastrModule } from 'ngx-toastr';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ConfigProvider, CoreModule } from './core';

export function init(config: ConfigProvider): any {
  return () => config.init();
}

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot(),
    AppRoutingModule,
    CoreModule
  ],
  providers: [
    { provide: APP_INITIALIZER, useFactory: init, deps: [ConfigProvider], multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
