import { Component, OnInit, Input } from '@angular/core';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';
import { first } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { GridDataResult, DataStateChangeEvent } from '@progress/kendo-angular-grid';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { FunctionSecurityService } from './functionsequrity.service';
import { TosterService } from 'src/app/services/toster.service';
import { GlobalResourceService } from '../global-resource/global-resource.service';
import { Resource } from '../model/resource';

@Component({
  selector: 'app-functionsequrity',
  templateUrl: './functionsequrity.component.html',
  styleUrls: ['./functionsequrity.component.css'],
  
})
export class FunctionSecurityComponent implements OnInit {  
  @Input() entityNameParam;  
  @Input() roleIdParam;  
  entityName:string;
  roleId:string;
  functionSecurity:any;  
  public resource = Resource;
  
  constructor(private activatedRoute: ActivatedRoute,private router: Router,private functionSecurityService:FunctionSecurityService,
    private toster:TosterService,private globalResourceService: GlobalResourceService ) { 
      
    //   this.router.routeReuseStrategy.shouldReuseRoute = function() {
    //     return false;
    // };
    }

 

  ngOnInit() {
    this.resource = this.globalResourceService.getGlobalResources();    
    this.entityName = this.entityNameParam;
    this.roleId=this.roleIdParam;
    
    
   if(!this.entityName )
   {
    this.activatedRoute.parent.parent.params.subscribe((params: Params) => {
      this.entityName = params['name'];
      this.roleId='';
    });

   }

   this.getFunctionSecurity();
  }

  private getFunctionSecurity() {
    this.functionSecurityService.getFunctionSecurity(this.entityName,this.roleId).pipe(first()).subscribe(
        data => {   
          if (data)   
            this.functionSecurity=data;
           

            this.functionSecurity.forEach(obj => { 
              obj.entity.functions.forEach(fun => { 
                var duplicateObject = JSON.parse(JSON.stringify( obj.entity.operationLevel ));
                fun.operationLevel = duplicateObject;
                var code=fun.securityCode==0? '' : fun.securityCode.toString();
                for (var i = 0; i < fun.operationLevel.length; i++) { 
                  if(code.charAt(i))             
                        fun.operationLevel[i].scopeSelected= code.charAt(i);
                     else
                         fun.operationLevel[i].scopeSelected= 1;
              }             
            });         
        });                

        },
        error => {
          console.log(error);
        });
  }

  private addFunctionSecurity(itsFunction) {
    this.functionSecurityService.addFunctionSecurity(this.entityName,itsFunction).pipe(first()).subscribe(
        data => {   
          if (data)   
          { 
            this.functionSecurity.forEach(obj => {
              if(obj.roleId==itsFunction.roleId)
                {
                  obj.entity.functions.forEach(fun => {
                    if(fun.functionContext==itsFunction.functionContext)
                    {
                      fun.entityId=data.entityId;
                      fun.entitySecurityId=data.entitySecurityId;
                      fun.securityCode=data.securityCode;
                      fun.roleId=data.roleId;
        
             } 
            });
          }                      
          });

           this.toster.showSuccess(this.getResourceValue("metadata_security_operation_save_success_message"));  
          }
          
        },
        error => {
          console.log(error);
        });
  }

  private updateFunctionSecurity(sequrityEntity) {
    this.functionSecurityService.updateFunctionSecurity(this.entityName,sequrityEntity).pipe(first()).subscribe(
        data => {   
          if (data)   
          this.toster.showSuccess(this.getResourceValue("metadata_security_operation_update_success_message"));  
        },
        error => {
          console.log(error);
        });
  }


   onAccessSelected(model,itsFunction) {
    
    this.functionSecurity.forEach(obj => {
      if(obj.roleId==itsFunction.roleId)
        {
          obj.entity.functions.forEach(fun => {
            if(fun.functionContext==itsFunction.functionContext)
            {
              var securityCode = "";          
              fun.operationLevel.forEach(objLvl => {           
                var securityData = objLvl.scopeSelected ? objLvl.scopeSelected : "1";
                securityCode += securityData;                      
            });

            if(itsFunction.entitySecurityId=='00000000-0000-0000-0000-000000000000')
              {
                //itsFunction.entityId=this.entityName;    
                itsFunction.securityCode=securityCode;
                this.addFunctionSecurity(itsFunction);
              }else{
                itsFunction.securityCode=securityCode;
                //delete this after get api
                //itsFunction.entityId=this.entityName;
                this.updateFunctionSecurity(itsFunction);
              }

            }
            
          });

     }                       
  });

   

 

}
getResourceByKey(key: any) {
  if(this.resource[this.generateResourceName(key)]){
    return this.resource[this.generateResourceName(key)];
  }else{
    return key;
  }
}
generateResourceName(word)
{
   if (!word) return word;
   return word[0].toLowerCase() + word.substr(1);
 }
  
 getResourceValue(key) {
  return this.globalResourceService.getResourceValueByKey(key);
}

}
