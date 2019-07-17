export class Communication {
    id:string;
    context:string;
    contextName:string;
    content:string;
    updatedOn: Date;
    updatedBy: string;
    
}

  export class SmsCommunication{
    smsBaseUrl:string;
    smsSender:string;
    smsShortCode:string;
    smsUserName:string;
    smsHash:string;
    smsApiKey:string;
    // smsPassword:string;
    smsReceiptUrl:string;
  }

  export class EmailCommunication{
    emailServer:string;
    emailPort:string;
    emailEmail:string;
    emailUserName:string;
    emailPassword:string;
    // emailKey:string;
    emailSender:string;
  }