import { Component, OnInit, Input } from '@angular/core';
import { first } from 'rxjs/operators';
import { SchedulerService } from './scheduler.service';
import { TosterService } from 'src/app/services/toster.service';
import { schedulerinfo } from 'src/app/model/scheduler/schedulerinfo';
import { schedulerdailyinfo } from 'src/app/model/scheduler/schedulerdailyinfo';
import { schedulermonthlyinfo } from 'src/app/model/scheduler/schedulermonthlyinfo';
import { schedulerweeklyinfo } from 'src/app/model/scheduler/schedulerweeklyinfo';
import { scheduleryearlyinfo } from 'src/app/model/scheduler/scheduleryearlyinfo';
import{GlobalResourceService} from '../global-resource/global-resource.service';
import{Resource} from '../model/resource';

@Component({
  selector: 'app-scheduler',
  templateUrl: './scheduler.component.html',
  styleUrls: ['./scheduler.component.css']
})
export class SchedulerComponent implements OnInit {
  public resource:Resource;
  schedulerInfo = new schedulerinfo();  
  schedulerDailyInfo=new schedulerdailyinfo();
  schedulerWeeklyInfo=new schedulerweeklyinfo();
  schedulerMonthlyInfo=new schedulermonthlyinfo();  
  schedulerYearlyInfo=new scheduleryearlyinfo();

  @Input() batchId 
  timeLists:any[]; 
  monthLists:any[];
  unitLists:any[];
  weekDaysLists:any[];
  hourLists:any[];
    
  constructor( private schedulerService: SchedulerService,private toster: TosterService,private globalResourceService:GlobalResourceService,) {} 

  ngOnInit() {  
    this.getMonths();
    this.getUnits();
    this.getWeekDays();  
    this.getHours()
    this.getScheduler();
  }  

  addScheduler(){ 
    this.resource=this.globalResourceService.getGlobalResources(); 
    this.schedulerService.addScheduler(this.schedulerInfo).pipe(first()).subscribe(
      data => {   
        if (data) {
          if(this.schedulerInfo.schedulerId=='00000000-0000-0000-0000-000000000000')
           {
            this.schedulerInfo.schedulerId=data;
           }               
          this.toster.showSuccess(this.getResourceValue("SchedulerSavedSuccessfully")); 
        }
      },
      error => {
        console.log(error);
      });
}


  private getScheduler() {
    this.schedulerService.getScheduler(this.batchId).pipe(first()).subscribe( data => {  
      if (data)
           this.schedulerInfo = data;          
           if(this.schedulerInfo.schedulerId=='00000000-0000-0000-0000-000000000000')
           {            
              if(this.schedulerInfo.recurrenceType==0)
                {
                  this.schedulerInfo.recurrenceType=1;
                }
                if(this.schedulerInfo.daily.unit==0)
                  {
                    this.schedulerInfo.daily.unit=1;
                  }
           }           
        },
        error => {
          console.log(error);
        });
  }

  recurrenceTypeChange(value)
  {
    this.schedulerInfo.daily=Object.assign({}, this.schedulerDailyInfo);
    this.schedulerInfo.weekly=Object.assign({}, this.schedulerWeeklyInfo);
    this.schedulerInfo.monthly=Object.assign({}, this.schedulerMonthlyInfo);
    this.schedulerInfo.yearly=Object.assign({}, this.schedulerYearlyInfo);

    this.schedulerInfo.recurrenceType=value;
    if(this.schedulerInfo.recurrenceType==1){
      this.schedulerInfo.daily.unit=1;
    }else if(this.schedulerInfo.recurrenceType==2){     
    }else if(this.schedulerInfo.recurrenceType==3){
      this.schedulerInfo.monthly.unit=1; 
    }else if(this.schedulerInfo.recurrenceType==4){
      this.schedulerInfo.yearly.unit=1; 
    }
  }


  dailyRadioClick(value)
  {
    this.schedulerInfo.daily.unit=value
    if(value==2)
    {
      this.schedulerInfo.daily.value=this.schedulerDailyInfo.value;
    }
  }

  weekDayChecked(event,info)
  {
    info=event;
  }
  monthltRadioClick(value)
  {
    this.schedulerInfo.monthly.unit=value;
    if(this.schedulerInfo.monthly.unit==1)
    {
      this.schedulerInfo.monthly.theValue1=this.schedulerMonthlyInfo.theValue1;
      this.schedulerInfo.monthly.theValue2=this.schedulerMonthlyInfo.theValue2;
      this.schedulerInfo.monthly.theValue3=this.schedulerMonthlyInfo.theValue3;
    }else{
      this.schedulerInfo.monthly.dayValue1=this.schedulerMonthlyInfo.dayValue1;
      this.schedulerInfo.monthly.dayValue2=this.schedulerMonthlyInfo.dayValue2;
    }
  }

  yearlyRadioClick(value)
  {
    this.schedulerInfo.yearly.unit=value;
    if(value==1)
    {
      this.schedulerInfo.yearly.theValue1= this.schedulerYearlyInfo.theValue1;
      this.schedulerInfo.yearly.theValue2=this.schedulerYearlyInfo.theValue2;
      this.schedulerInfo.yearly.theValue3=this.schedulerYearlyInfo.theValue3;

    }else{
      this.schedulerInfo.yearly.onValue1= this.schedulerYearlyInfo.onValue1;
      this.schedulerInfo.yearly.onValue2= this.schedulerYearlyInfo.onValue2;
    }

  }

  private getMonths() {
    this.schedulerService.getMonths().pipe(first()).subscribe( data => {  
      if (data)
           this.monthLists = data;            
        },
        error => {
          console.log(error);
        });
  }

  private getUnits()
  {
    this.schedulerService.getUnits().pipe(first()).subscribe( data => {  
      if (data)
           this.unitLists = data;            
        },
        error => {
          console.log(error);
        });
  }

  private getWeekDays()
  {
    this.schedulerService.getWeekDays().pipe(first()).subscribe( data => {  
      if (data)
           this.weekDaysLists = data;            
        },
        error => {
          console.log(error);
        });
  }

  private getHours()
  {
    this.schedulerService.getHours().pipe(first()).subscribe( data => {  
      if (data)
           this.hourLists = data;            
        },
        error => {
          console.log(error);
        });
  }

  getResourceValue(key) {
    return this.globalResourceService.getResourceValueByKey(key);
  }

}
