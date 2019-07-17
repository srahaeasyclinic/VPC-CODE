
import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { first } from 'rxjs/operators';
import { SubscriptionDetailService } from './subscription-detail.service';
import { TosterService } from 'src/app/services/toster.service';
import { SubscriptionService } from './subscription.service';
import { TenantSubscriptionInfo } from '../model/tenantsubscription/tenantsubscriptioninfo';
import { Entities } from '../model/entities';
import { GridDataResult, DataStateChangeEvent } from '@progress/kendo-angular-grid'; 
import { MetadataService } from '../meta-data/metadata.service';
import {TenantSubscriptionEntityInfo} from '../model/tenantsubscription/tenantsubscriptionentityinfo'
import {TenantSubscriptionEntityDetailInfo} from '../model/tenantsubscription/tenantsubscriptionentitydetailinfo'
import { Observable } from 'rxjs';
import {GlobalResourceService} from '../global-resource/global-resource.service';
import{Resource} from '../model/resource';

@Component({
  selector: 'app-subscription-detail',
  templateUrl: './subscription-detail.component.html',
  styleUrls: ['./subscription-detail.component.css']
})
export class SubscriptionDetailComponent implements OnInit {  

  subscriptionId:string; 
  groupTypes= []; 
  subsGroups=[];
  limitTypes=[];
  recurringDurations= []; 
  public subscriptionInfo:TenantSubscriptionInfo;
  subscriptionEntity:TenantSubscriptionEntityInfo;
  selectedSubscriptionEntity:TenantSubscriptionEntityInfo;
  subscriptionEntityDetails:TenantSubscriptionEntityDetailInfo[];
  subscriptionEntityDetailFeatures:TenantSubscriptionEntityDetailInfo[];
  subscriptionEntityDetailReports:TenantSubscriptionEntityDetailInfo[];
  subscriptionEntityDetailDashlets:TenantSubscriptionEntityDetailInfo[];
  
  private entityList: Entities[]; 
  public gridData: any = this.entityList;
  public view: Observable<GridDataResult>;
  private showSubscription: boolean = false;
  public resource:Resource;

  constructor(private activatedRoute:ActivatedRoute,private subscriptionService:SubscriptionService,private toster:TosterService,
    private subscriptionDetailService:SubscriptionDetailService,private metadataService:MetadataService,private globalResourceService:GlobalResourceService,
  ) { }

  ngOnInit() {  
    // this.resource=this.globalResourceService.getGlobalResources();
    this.activatedRoute.params.subscribe((params: Params) => {
      this.subscriptionId = params['subscriptionId'];
    });
      
    this.getSubscriptions();       
    this.getRecurringDuration();
    this.getLimitCount();

  }

  getSubscriptions()
  {
    this.subscriptionService.getSubscription(this.subscriptionId).pipe(first()).subscribe(
      data => {        
        if (data) {
         this.subscriptionInfo = data; 
         //console.log('this.subscriptionInfo ', this.subscriptionInfo);
         this.getEntities();
         this.getGroupTypes(); 
        }
      },
      error => {
        console.log(error);
      });
  }

  updateSubscription(){  
    
    if(this.subscriptionInfo.name=="")
    {
      this.toster.showWarning(this.getResourceValue("Nameisrequired"));
      return false;
    }else if(this.subscriptionInfo.group==null)
    {
      this.toster.showWarning(this.getResourceValue("GroupIsRequired"));
      return false;
    }else if(this.subscriptionInfo.group.id=="")
    {
      this.toster.showWarning(this.getResourceValue("GroupIsRequired"));
      return false;
    }
   
    this.subscriptionService.updateSubscription(this.subscriptionInfo).pipe(first()).subscribe(
      data => {   
        if (data) {       
          this.toster.showSuccess(this.getResourceValue("SubscriptionSavedSuccessfully")); 
        }
      },
      error => {
        console.log(error);
      });
}



private getGroupTypes()
  {
    this.subscriptionService.getSubscriptionGroup(null).pipe(first()).subscribe(data => {       
          if (data) {
           this.groupTypes = data.result;  
           this.subsGroups = this.groupTypes.slice();
           this.subsGroups.forEach(subsGroup=>{
           if(subsGroup.internalId==this.subscriptionInfo.group.id)
           {
            this.subscriptionInfo.group.name=subsGroup.text;
           }
          });

          }
        },
        error => {
          console.log(error);
        });

  //this.groupTypes.push({text:"Manufacturer",internalId:"960587e3-15cd-4e1e-b1d0-471b0bada300"},{text:"Provider",internalId:"360587e3-15cd-4e1e-b1d0-471b0bada300"})
    
 
 }

 handleFilter(value) {
  this.subsGroups = this.groupTypes.filter((s) => s.text.toLowerCase().indexOf(value.toLowerCase()) !== -1);
}
  

 private getRecurringDuration()
 {
  this.subscriptionService.getDurations().pipe(first())  .subscribe(    data => {        
      if (data) {
         this.recurringDurations = data;       
      }
    },
    error => {
      console.log(error);
    });  
 }

 onGroupChange(groupId)
 {
   if(groupId=="")
   {
    this.toster.showWarning(this.getResourceValue("GroupIsRequired"));
     return false;
   }
  var selectedGroup = this.groupTypes.filter((s) => s.text.toLowerCase().indexOf(groupId.toLowerCase()) !== -1);
  if(selectedGroup.length==1)
  {
    this.subscriptionInfo.group.id=selectedGroup[0].internalId;
    this.updateSubscription();
  }

 }

 onRecurringDurationChange(recurringDuration)
 {
  this.subscriptionInfo.recurringDuration=recurringDuration;
  this.updateSubscription();
 }

 private getEntities() {
  this.metadataService.getEntities().pipe(first()).subscribe(
      data => {
        if(data){          
          data.forEach((obj) => {
           obj.showSubscription=false;
          });
          this.gridData = data; 
          this.getSubscribedEntities(this.gridData) ;
          //console.log('this.gridData ', this.gridData);     
        }
      },
      error => {
        console.log(error);
      });
}

getSubscribedEntities(entities)
{
  this.subscriptionDetailService.getSubscriptionEntities(this.subscriptionInfo.tenantSubscriptionId).pipe(first()).subscribe(
    data => {
      if(data){

        for (let i=0; i<entities.length; i++) {
          entities[i].subscriptionEntity={};
          entities[i].subscriptionEntity.entityId=entities[i].name;
          entities[i].entityChecked=false;    
          var checkSubscribedEntity = data.filter(p => p.entityId === entities[i].name);
          if(checkSubscribedEntity.length>0)
          {
            entities[i].subscriptionEntity=checkSubscribedEntity[0];
            entities[i].entityChecked=true;    
          }

          if(i==0)
          {
            this.currentSubscriptionEntity(entities[i])             
          }
        }       
      }
    },
    error => {
      console.log(error);
    });
}

entityOnCheck(entity)
{
  if(entity.entityChecked)
  {    
    entity.subscriptionEntity.tenantSubscriptionId=this.subscriptionInfo.tenantSubscriptionId;
    this.subscriptionDetailService.addSubscriptionEntity(entity.subscriptionEntity).pipe(first()).subscribe(
      data => {
        if(data){
          entity.subscriptionEntity.tenantSubscriptionEntityId = data;    
          this.toster.showSuccess(this.getResourceValue("SubscriptionSavedSuccessfully"));  
          this.currentSubscriptionEntity(entity);   
        }
      },
      error => {
        console.log(error);
      });
  }else{
        this.subscriptionDetailService.deleteSubscriptionEntity(entity.subscriptionEntity.tenantSubscriptionEntityId).pipe(first()).subscribe(
            data => {
              if(data){
                entity.subscriptionEntity={};
                entity.subscriptionEntity.entityId=entity.name; 
                this.toster.showSuccess(this.getResourceValue("SubscriptionSavedSuccessfully"));  
                this.clearControlOnEntityUnCheck();   
              }
            },
      error => {
        console.log(error);
      });
  }
  
}

currentSubscriptionEntity(entitySubscription)
{
  this.gridData.forEach((obj) => {
    if(obj.name==entitySubscription.name){
      obj.showSubscription=true;
    }  else{
      obj.showSubscription=false;
    }  
   }); 
  this.selectedSubscriptionEntity=entitySubscription;  
  this.subscriptionEntity=entitySubscription.subscriptionEntity;
  this.getEntityDetails(this.subscriptionEntity.entityId,this.subscriptionEntity.tenantSubscriptionEntityId);
  
}

clearControlOnEntityUnCheck()
{
  this.subscriptionEntity.limtNumber=null;
  this.subscriptionEntity.limitType=0;

  this.subscriptionEntityDetailFeatures.forEach(feature=>{
    feature.subscriptionEntityDetailId='00000000-0000-0000-0000-000000000000';            
    feature.recurringPrice=null;
    feature.recurringDuration=0;
    feature.oneTimePrice=null;
    feature.oneTimeDuration=0;
    feature.infoChecked=false;
  });

  this.subscriptionEntityDetailReports.forEach(report=>{
    report.subscriptionEntityDetailId='00000000-0000-0000-0000-000000000000';            
    report.recurringPrice=null;
    report.recurringDuration=0;
    report.oneTimePrice=null;
    report.oneTimeDuration=0;
    report.infoChecked=false;
  });

  this.subscriptionEntityDetailDashlets.forEach(dashlet=>{
    dashlet.subscriptionEntityDetailId='00000000-0000-0000-0000-000000000000';            
    dashlet.recurringPrice=null;
    dashlet.recurringDuration=0;
    dashlet.oneTimePrice=null;
    dashlet.oneTimeDuration=0;
    dashlet.infoChecked=false;
  });
           
}

updateSubscriptionEntity()
{
    this.subscriptionDetailService.updateSubscriptionEntity(this.subscriptionEntity).pipe(first()).subscribe(
      data => {
        if(data){
          this.toster.showSuccess(this.getResourceValue("SubscriptionSavedSuccessfully")); 
        }
          },
      error => {
      console.log(error);
      });
}

 private getLimitCount()
{
  this.subscriptionDetailService.getLimitCount().pipe(first()).subscribe(
    data => {
      if(data){
        this.limitTypes=data;  
      }
        },
    error => {
    console.log(error);
    });
}

onLimitTypeChange(event)
{
  this.subscriptionEntity.limitType=event;
  this.updateSubscriptionEntity();
}


private getEntityDetails(entityName,tenantSubscriptionEntityId)
{
  if(tenantSubscriptionEntityId!=null && tenantSubscriptionEntityId!=undefined)
  {
    this.subscriptionDetailService.getEntityDetails(tenantSubscriptionEntityId).pipe(first()).subscribe(
      data => {
        if(data){
          this.subscriptionEntityDetails=data;  
        }
                      this.getFeatures(entityName,tenantSubscriptionEntityId);
                      this.getReports(entityName,tenantSubscriptionEntityId);
                      this.getDashlets(entityName,tenantSubscriptionEntityId);
          },
      error => {
      console.log(error);
      });
  }else{
    this.subscriptionEntityDetails=[]; 
    this.getFeatures(entityName,tenantSubscriptionEntityId);
    this.getReports(entityName,tenantSubscriptionEntityId);
    this.getDashlets(entityName,tenantSubscriptionEntityId);
  }
  
}
  private getFeatures(entityName,tenantSubscriptionEntityId)
  {
    this.subscriptionDetailService.getFeatures(entityName).pipe(first()).subscribe(
      data => {
        if(data){
          this.subscriptionEntityDetailFeatures=data;  

          for (let i=0; i<this.subscriptionEntityDetailFeatures.length; i++) {          
            this.subscriptionEntityDetailFeatures[i].infoChecked=false;            
            var checkSubscribedEntity = this.subscriptionEntityDetails.filter(p => p.context === this.subscriptionEntityDetailFeatures[i].context);
            if(checkSubscribedEntity.length>0)
            {
              checkSubscribedEntity[0].name=this.subscriptionEntityDetailFeatures[i].name;
              this.subscriptionEntityDetailFeatures[i]=checkSubscribedEntity[0];
              this.subscriptionEntityDetailFeatures[i].infoChecked=true;    
            }else{
              this.subscriptionEntityDetailFeatures[i].subscriptionEntityId=tenantSubscriptionEntityId;
            }  
          } 
        }
          },
      error => {
      console.log(error);
      });
  }

  featureReportDashletOnCheck(featureReportDashletInfo)
  {
    if(featureReportDashletInfo.infoChecked)
    {
      this.subscriptionDetailService.addEntityDetail(featureReportDashletInfo).pipe(first()).subscribe(
        data => {
          if(data){
            featureReportDashletInfo.subscriptionEntityDetailId=data;
            this.toster.showSuccess(this.getResourceValue("SubscriptionSavedSuccessfully")); 
          }
            },
        error => {
        console.log(error);
        });
    }else{
      this.subscriptionDetailService.deleteEntityDetail(featureReportDashletInfo.subscriptionEntityDetailId).pipe(first()).subscribe(
        data => {
          if(data){
            featureReportDashletInfo.subscriptionEntityDetailId='00000000-0000-0000-0000-000000000000';            
            featureReportDashletInfo.recurringPrice=null;
            featureReportDashletInfo.recurringDuration=0;
            featureReportDashletInfo.oneTimePrice=null;
            featureReportDashletInfo.oneTimeDuration=0;
            this.toster.showSuccess(this.getResourceValue("SubscriptionSavedSuccessfully")); 
          }
            },
        error => {
        console.log(error);
        });

    }
  
  }

  detailRecurringDuration_OnChange(event,info)
  {
    info.recurringDuration=event;
    this.featureReportDashlet_Update(info);
  }

  detailOneTimeDuration_OnChange(event,info)
  {
    info.oneTimeDuration=event;
    this.featureReportDashlet_Update(info);
  }


  featureReportDashlet_Update(featureReportDashletInfo)
  {
    this.subscriptionDetailService.updateEntityDetail(featureReportDashletInfo).pipe(first()).subscribe(
      data => {
        if(data){   
          this.toster.showSuccess(this.getResourceValue("SubscriptionSavedSuccessfully")); 
        }
          },
      error => {
      console.log(error);
      });
  
  }


  private getReports(entityName,tenantSubscriptionEntityId)
  {

    this.subscriptionDetailService.getReports(entityName).pipe(first()).subscribe(
      data => {
        if(data){
          this.subscriptionEntityDetailReports=data;  

          for (let i=0; i<this.subscriptionEntityDetailReports.length; i++) {          
            this.subscriptionEntityDetailReports[i].infoChecked=false;            
            var checkSubscribedEntity = this.subscriptionEntityDetails.filter(p => p.context === this.subscriptionEntityDetailReports[i].context);
            if(checkSubscribedEntity.length>0)
            {
              checkSubscribedEntity[0].name=this.subscriptionEntityDetailReports[i].name;
              this.subscriptionEntityDetailReports[i]=checkSubscribedEntity[0];
              this.subscriptionEntityDetailReports[i].infoChecked=true;    
            }else{
              this.subscriptionEntityDetailReports[i].subscriptionEntityId=tenantSubscriptionEntityId;
            }  
          } 
        }
          },
      error => {
      console.log(error);
      });


  }

  private getDashlets(entityName,tenantSubscriptionEntityId)
  {

    this.subscriptionDetailService.getDashlets(entityName).pipe(first()).subscribe(
      data => {
        if(data){
          this.subscriptionEntityDetailDashlets=data;  

          for (let i=0; i<this.subscriptionEntityDetailDashlets.length; i++) {          
            this.subscriptionEntityDetailDashlets[i].infoChecked=false;            
            var checkSubscribedEntity = this.subscriptionEntityDetails.filter(p => p.context === this.subscriptionEntityDetailDashlets[i].context);
            if(checkSubscribedEntity.length>0)
            {
              checkSubscribedEntity[0].name=this.subscriptionEntityDetailDashlets[i].name;
              this.subscriptionEntityDetailDashlets[i]=checkSubscribedEntity[0];
              this.subscriptionEntityDetailDashlets[i].infoChecked=true;    
            }else{
              this.subscriptionEntityDetailDashlets[i].subscriptionEntityId=tenantSubscriptionEntityId;
            }  
          } 
        }
          },
      error => {
      console.log(error);
      });

  }
   getResourceValue(key) {
    return this.globalResourceService.getResourceValueByKey(key);
  }

}
