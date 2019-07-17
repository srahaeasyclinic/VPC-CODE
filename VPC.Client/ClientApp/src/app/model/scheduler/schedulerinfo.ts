import { schedulerdailyinfo } from "./schedulerdailyinfo";
import { schedulermonthlyinfo } from "./schedulermonthlyinfo";
import { schedulerweeklyinfo } from "./schedulerweeklyinfo";
import { scheduleryearlyinfo } from "./scheduleryearlyinfo";

export class schedulerinfo {
    schedulerId:string
    batchTypeId:string
    syncTime:number
    recurrenceType:number     
    daily:schedulerdailyinfo
    weekly:schedulerweeklyinfo
    monthly:schedulermonthlyinfo
    yearly:scheduleryearlyinfo
}


  