import { ErrorHandler, NgModule } from '@angular/core';
import { ConfigProvider } from './config';
import { CustomErrorHandler } from './error';

@NgModule({
  providers: [
    ConfigProvider,
    { provide: ErrorHandler, useClass: CustomErrorHandler },
  ]
})
export class CoreModule {
}
