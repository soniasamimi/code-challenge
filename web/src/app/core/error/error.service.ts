import { HttpErrorResponse } from '@angular/common/http';
import { ErrorHandler, Injectable, Injector } from '@angular/core';
import { ToastrService } from 'ngx-toastr';

@Injectable()
export class CustomErrorHandler implements ErrorHandler {

  constructor(private injector: Injector) { }

  //  We need to use the Injector
  //  to get the ToastrService to
  //  prevent Circular Dependency error
  get toastr(): ToastrService {
    return this.injector.get(ToastrService);
  }

  handleError(error: Error | HttpErrorResponse): void {
    if (error instanceof HttpErrorResponse) {
      switch (error.status) {
        case 0:
          this.toastr.error('Service is not available', 'Error');
          break;
        case 500:
          this.toastr.error(error.error.Message, 'Error');
          break;
      }
    }
  }
}
