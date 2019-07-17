import { Injectable } from '@angular/core';
import { ToastrManager } from 'ng6-toastr-notifications';

//Taken from : https://www.npmjs.com/package/ng6-toastr-notifications
@Injectable({
  providedIn: 'root'
})
export class TosterService {

  constructor(public toastr: ToastrManager) { }

  showSuccess(message) {
   
    this.toastr.successToastr(message, 'Success!', { position: 'top-left', toastTimeout: 3000, animate: 'slideFromLeft' });
  }

  showError(message: any) {
    //console.log(' showError(message)'+ message);

    this.toastr.errorToastr(message, 'Oops!', { position: 'top-left', animate: 'slideFromLeft', showCloseButton: true, dismiss: 'click', enableHTML: true });
  }

  showWarning(message: any) {
    this.toastr.warningToastr(message, 'Alert!', { position: 'top-left', toastTimeout: 3000, animate: 'slideFromLeft' });
  }

  showInfo(message) {
    this.toastr.infoToastr(message, 'Info', { position: 'top-left', toastTimeout: 3000, animate: 'slideFromLeft' });
  }

  showCustom(message) {
    this.toastr.customToastr(message, null, { enableHTML: true });
  }

  showToast(position: any = 'top-left') {
    this.toastr.infoToastr('This is a toast.', 'Toast', { position: position });
  }
  dismissAllToastr() {
    this.toastr.dismissAllToastr();
  }

  showErrorWithTitle(title: any, message: any) {
    this.toastr.errorToastr(message, title, { position: 'top-left', animate: 'slideFromLeft', showCloseButton: true, dismiss: 'click', enableHTML: true });
  }
}

export const globalErrorMessage = "Something went wrong. Try after sometimes.";
