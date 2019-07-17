import { Injectable, OnInit } from "@angular/core";
import { ValidationService } from "src/app/services/validation.service";
import { Broadcaster } from "../messaging/broadcaster";
import { AgeCalculation } from "../messaging/result/methods/agecalculation";
import { MessageEvent } from '../messaging/message.event';
import { Payload } from "../messaging/payload";
@Injectable()
export abstract class AtomBase implements OnInit {
    constructor(public broadcaster: Broadcaster, public messageEvent: MessageEvent) { }
    // registerTypeBroadcast(fieldName:string, receivingType: string, receivingDataType:string) {
    //     if (receivingType) {
    //         this.messageEvent.on()
    //             .subscribe(message => {
    //                 if (message && receivingType.toLowerCase() == message.method.toLowerCase()) {
    //                     console.log("message", message);
    //                     console.log("receivingType", receivingType);
    //                     console.log("receivingDataType", receivingDataType);
    //                     return message.data;
    //                 }
    //             });
    //     }

    // }
    brodcastData(fieldName:string, methodName: string, value: any) {
        console.log("<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<");
        var payload = new Payload();
        payload.name = fieldName;
        payload.data = value;
        payload.method = methodName;//this.field.broadcastingType;
        this.messageEvent.fire(payload);
    }
    ngOnInit() {

    }
}