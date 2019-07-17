import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';

@Injectable({
    providedIn: 'root'
  })
export class LogService {
    log(text: string){
        if (!environment.production) {
          console.log('logged ', text);
        }
   }
}