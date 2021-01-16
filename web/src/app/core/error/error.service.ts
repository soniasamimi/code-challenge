import { HttpErrorResponse } from '@angular/common/http';
import { ErrorHandler, Injectable, Injector } from '@angular/core';
import { ToastrService } from 'ngx-toastr';

@Injectable()
export class CustomErrorHandler implements ErrorHandler {

  constructor(private injector: Injector) { }

  get toastr(): ToastrService {
    return this.injector.get(ToastrService);
  }

  handleError(error: Error | HttpErrorResponse): void {
    if (error instanceof HttpErrorResponse) {
      if (error.status === 500) {
        this.toastr.error(error.error.Message, 'Error');
      }
    }
  }
}
