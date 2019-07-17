import { Injectable } from '@angular/core';

@Injectable({
    providedIn: 'root'
})
export class RequiredfieldService {

    
    constructor() { }    

    requiredfield(obj:any):string {  
       // console.log(obj.name+' -:',obj.value)
        
        if(!obj.value){              
          return  obj.name;
        }
    }

    

}


