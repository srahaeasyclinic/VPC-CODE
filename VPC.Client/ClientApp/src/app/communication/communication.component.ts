import { Component, OnInit } from '@angular/core';
import { ResourceService } from '../services/resource.service';
import { first } from 'rxjs/operators';
import { CommunicationService } from '../services/communication.service';
import { Communication, SmsCommunication, EmailCommunication } from '../model/communication';
import { TosterService } from 'src/app/services/toster.service';
import { CommonService } from '../services/common.service';
import { NgbModal, NgbModalOptions } from '@ng-bootstrap/ng-bootstrap';
import { GlobalResourceService } from '../global-resource/global-resource.service';

@Component({
  selector: 'app-communication',
  templateUrl: './communication.component.html',
  styleUrls: ['./communication.component.css']
})
export class CommunicationComponent implements OnInit {
 public resource: any | [];
  
  private communication: any[];
  public context: any = this.communication;
  smsInfoModel: SmsCommunication;
  emailInfoModel: EmailCommunication;
 public validateMessages: Array<string> = [];
 public helpBlock:boolean = true;
  constructor(
   // private resourceService: ResourceService,
    private communicationService: CommunicationService,
    private toster: TosterService,
    private commonService: CommonService,
    private modalService: NgbModal,
     private globalResourceService: GlobalResourceService
  ) { }

  ngOnInit() {
    this.smsInfoModel = new SmsCommunication();
    this.emailInfoModel = new EmailCommunication();
   // this.getResource();
    this.getCommunicationContext();
    this.getCommunication();

    //this.DeserializeContent();
  }

  //private getResource(): void {
  //  this.resource = this.resourceService.getResources();
  //}

  private getCommunicationContext(): void {
    this.communicationService.getCommunicationContext()
      .pipe(first())
      .subscribe(
        data => {
          if (data && data) {
            this.context = data;
          }
        },
        error => {
          console.log(error)
        }
      );
  }



  private getCommunication(): void {
    this.communicationService.getCommunication()
      .pipe(first())
      .subscribe(
        data => {
          if (data.items) {
            //console.log('data.item ' +JSON.stringify(data));
            this.communication = data.items;
            this.GetContent();
          }
        },
        error => {
          console.log(error)
        }
      );
  }



  saveCommunication(content: string): string {
    this.validateMessages = [];
    this.validateMessages = this.validate();
    let ngbModalOptions: NgbModalOptions = {
      backdrop : 'static',
      keyboard : false
    };
    if (this.validateMessages.length > 0) {
      this.modalService.open(content, ngbModalOptions);
      return
    }
   
    this.SetContent();
    // this.communication.forEach(setting => {
    //console.log(JSON.stringify(this.communication));
    let Isnew = this.communication.find(s => s.id === '');
    //console.log(Isnew);
    if (Isnew !== undefined) {
      this.communicationService.saveCommunication(this.communication)
        .pipe(first())
        .subscribe(
          data => {
            if (data) {

              this.toster.showSuccess(this.globalResourceService.saveSuccessMessage("communication_displayname"));
            }
          },
          error => {
            this.toster.showError(error.message);
          });

    } else {
      this.communicationService.UpdateCommunication(this.communication)
        .pipe(first())
        .subscribe(
          data => {
            if (data) {

              this.toster.showSuccess(this.globalResourceService.updateSuccessMessage("communication_displayname"));
            }
          },
          error => {
            this.toster.showError(error.error.message);
          });
    }
    // });


  }
  private GetContent() {
    //console.log('GetContent ' + JSON.stringify(this.communication[0]));
    //console.log('communication ' +JSON.stringify(this.communication));
    this.communication.forEach(item => {
      if (item.contextName.toLowerCase() === 'email' && item.content.length > 0) {
        this.emailInfoModel = <EmailCommunication>JSON.parse(item.content);

      }
      if (item.contextName.toLowerCase() === 'sms' && item.content.length > 0) {
        this.smsInfoModel = <SmsCommunication>JSON.parse(item.content);


      }

    });


  }

private closeHelpBlock(){
  this.helpBlock = false;
}

  private SetContent() {
    if (this.communication.length <= 0) {
      this.communication = [];
      var comm: any;
      //console.log(JSON.stringify(this.communication));
      this.context.forEach(d => {

        if (d.name.toLowerCase() === 'email') {
          comm = {
            id: '',
            contextType: d.id,
            context: d.id,
            contextName: d.name,
            content: JSON.stringify(this.emailInfoModel),
            updatedON: null,
            updatedBy: ''

          };

        }
        if (d.name.toLowerCase() === 'sms') {
          comm = {
            id: '',
            context: d.id,
            contextName: d.name,

            content: JSON.stringify(this.smsInfoModel),
            updatedOn: null,
            updatedBy: ''

          };
        }

        this.communication.push(comm);
      });
      //console.log(JSON.stringify(this.communication));
      //console.log(JSON.stringify(this.emailInfoModel))
    } else {
      this.communication.map((toupdate, i) => {
        if (toupdate.contextName.toLowerCase() === 'email') {
          this.communication[i].content = JSON.stringify(this.emailInfoModel);
        }
        if (toupdate.contextName.toLowerCase() === 'sms') {
          this.communication[i].content = JSON.stringify(this.smsInfoModel);
        }

      });
    }

  }

  generateResourceName(key: string) {

    // var resourceData = "Resource missing";
		// if (key == null ||key == undefined) return key;

		// var camelCase = key[0].toLowerCase() + key.substr(1);
		// try {
		// 	resourceData = this.resource[camelCase];
		// } catch (error) {

		// }
    // return resourceData;
     return this.globalResourceService.getResourceValueByKey(key);
  }






  validate():Array<string>
  {
    let messageArrays = new Array<string>();
     if (this.emailInfoModel.emailServer == "") {
      messageArrays.push(this.globalResourceService.requiredValidator("communication_field_emaillabelserver"));
    }
    if (this.emailInfoModel.emailPort == "") {
      messageArrays.push(this.globalResourceService.requiredValidator("communication_field_emaillabelport"));
    }
    if (this.emailInfoModel.emailEmail == "") {
      messageArrays.push(this.globalResourceService.requiredValidator("communication_field_emaillabelemail"));
    }
    if (this.emailInfoModel.emailUserName == "") {
     messageArrays.push(this.globalResourceService.requiredValidator("communication_field_emaillabelusername"));
    }
    if (this.emailInfoModel.emailPassword == "") {
      messageArrays.push(this.globalResourceService.requiredValidator("communication_field_emaillabelpassword"));
    }
    if (this.emailInfoModel.emailSender == "") {
      messageArrays.push(this.globalResourceService.requiredValidator("communication_field_emaillabelsender"));
    }

    // if (this.smsInfoModel.smsBaseUrl == "") {
    //  messageArrays.push("emailServer")
    // }
    // if (this.smsInfoModel.smsSender == "") {
    //   messageArrays.push("emailServer")
    // }
    // if (this.smsInfoModel.smsShortCode == "") {
    //   messageArrays.push("emailServer")
    // }
    // if (this.smsInfoModel.smsUserName == "") {
    //   messageArrays.push("emailServer")
    // }
    // if (this.smsInfoModel.smsHash == "") {
    //   messageArrays.push("emailServer")
    // }
    // if (this.smsInfoModel.smsReceiptUrl == "") {
    //   messageArrays.push("emailServer")
    // }
    return messageArrays;
  }
  // 	makeCamelCase(key: string) {

	// 	var resourceData = "Resource missing";
	// 	if (key == null ||key == undefined) return key;

	// 	var camelCase = key[0].toLowerCase() + key.substr(1);
	// 	try {
	// 		resourceData = this.resource[camelCase];
	// 	} catch (error) {

	// 	}
	// 	return resourceData;
	// }

}
