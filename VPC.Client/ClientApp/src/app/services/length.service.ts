import { Injectable } from '@angular/core';
import { TosterService } from 'src/app/services/toster.service';

@Injectable({
  providedIn: 'root'
})
export class LengthService {
  public errorMessage: string = "";
  public validatorName:string;

  constructor(private toster: TosterService) { }

  lengthfield(obj:any):string {      
   
   obj.validators.forEach(element1=>{    
    element1.options.forEach(element2=>{     
      
   
    })
   })

    if(obj.value.length < 10){   
     
      return  obj.name + " should be 10 characters";
     
    }
    
  }
} 