import { Component, OnInit, Input } from '@angular/core';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';
import { first } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { GridDataResult, DataStateChangeEvent } from '@progress/kendo-angular-grid';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { EntitySecurityService } from './entitysequrity.service';
import { TosterService } from 'src/app/services/toster.service';
import { GlobalResourceService } from '../global-resource/global-resource.service';
import { Resource } from '../model/resource';
import { MenuService } from '../services/menu.service';


@Component({
  selector: 'app-entitysequrity',
  templateUrl: './entitysequrity.component.html',
  styleUrls: ['./entitysequrity.component.css'],
  
})
export class EntitySecurityComponent implements OnInit {  
  @Input() entityNameParam;  
  @Input() roleIdParam;  
  entityName:string;
  roleId:string;
  entitySecurity:any;  
  public resource = Resource;

  constructor(private activatedRoute: ActivatedRoute,private router: Router,private entitySecurityService:EntitySecurityService,
    private toster:TosterService,  private globalResourceService: GlobalResourceService,private menuService: MenuService) { 
      
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
     let result=this.menuService.getMenuconext();
     this.entityName = result.param_name;
     this.roleId='';
    // this.activatedRoute.parent.parent.params.subscribe((params: Params) => {
    //   this.entityName = params['entityName'];
    //   this.roleId='';
    // });

   }

   this.getEntitySecurity();
  }

  private getEntitySecurity() {
    this.entitySecurityService.getEntitySecurity(this.entityName,this.roleId).pipe(first()).subscribe(
        data => {   
          if (data)   
            this.entitySecurity=data;

            this.entitySecurity.forEach(obj => {           
              
            var code=obj.entity.data.securityCode==0? '' : obj.entity.data.securityCode.toString();
            for (var i = 0; i < obj.entity.operationLevel.length; i++) { 
              if(code.charAt(i))             
                    obj.entity.operationLevel[i].scopeSelected= code.charAt(i);
                 else
                     obj.entity.operationLevel[i].scopeSelected= 1;
          }   
                        
        });                

        },
        error => {
          console.log(error);
        });
  }

  private addEntitySecurity(sequrityEntity) {
    this.entitySecurityService.addEntitySecurity(this.entityName,sequrityEntity).pipe(first()).subscribe(
        data => {   
          if (data)   
          {           

            this.entitySecurity.forEach(obj => {
              if(obj.roleId==sequrityEntity.roleId)
                {
                  obj.entity.data=data;
                }                       
          });  
           this.toster.showSuccess(this.getResourceValue("metadata_security_operation_save_success_message"));  
          }
          
        },
        error => {
          console.log(error);
        });
  }

  private updateEntitySecurity(sequrityEntity) {
    this.entitySecurityService.updateEntitySecurity(this.entityName,sequrityEntity).pipe(first()).subscribe(
        data => {   
          if (data)   
          this.toster.showSuccess(this.getResourceValue("metadata_security_operation_update_success_message"));  
        },
        error => {
          console.log(error);
        });
  }


   onAccessSelected(model,sequrityEntity) {
    
    this.entitySecurity.forEach(obj => {
      if(obj.roleId==sequrityEntity.roleId)
        {
          var securityCode = "";          
          obj.entity.operationLevel.forEach(objLvl => {           
            var securityData = objLvl.scopeSelected ? objLvl.scopeSelected : "1";
            securityCode += securityData;                      
        });

        if(sequrityEntity.entitySecurityId=='00000000-0000-0000-0000-000000000000')
        {
          sequrityEntity.entityId=this.entityName;    
          sequrityEntity.securityCode=securityCode;
          this.addEntitySecurity(sequrityEntity);
        }else{
          sequrityEntity.securityCode=securityCode;
          //delete this after get api
          sequrityEntity.entityId=this.entityName;
          this.updateEntitySecurity(sequrityEntity);
        }


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
