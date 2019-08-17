import { Injectable, OnInit } from '@angular/core';
import { RequiredfieldService } from '../services/requiredfield.service';
import { ActivatedRoute, Router ,Params} from '@angular/router';
import { LengthService } from '../services/length.service';
import { first } from 'rxjs/operators';
import { GlobalResourceService } from '../global-resource/global-resource.service';
import { timingSafeEqual } from 'crypto';
import { DebugRenderer2 } from '@angular/core/src/view/services';


@Injectable({
  providedIn: 'root'
})
export class ValidationService implements OnInit {
  public entityName: string;
  public validateMessage: string;
  public errorMessage = '';
  public minLengthValidatormsg='validation_field_minlength_';
  //public messageArrays: Array<string> = [];
  private regex = {
    number: new RegExp(/^\d+$/),
    decimal: new RegExp(/^[0-9]+(\.[0-9]*){0,1}$/g)
  };


  constructor(private requiredfieldservice: RequiredfieldService,
    private lengthservice: LengthService,
    private globalResourceService: GlobalResourceService,
    private route: ActivatedRoute,
  ) {


  }
  ngOnInit() {


  }

  public validate(obj: any[],entityName:any): Array<string> {

    this.entityName=entityName.toLowerCase();
    let messageArrays = new Array<string>();
    this.valdiateHelper(obj, messageArrays);
    return messageArrays;
  }

  private valdiateHelper(obj: any[], store: Array<string>) {
    ///console.log('valdiateHelper'+ JSON.stringify(obj));
    for (let i = 0; i < obj.length; i++) {
      if (obj && Array.isArray(obj)) {
       // this.entityName=obj[i].entityName===''? obj[i].entityName=this.entityName:obj[i].entityName.toLowerCase();
        if (obj[i].controlType && obj[i].controlType.toLowerCase() !== 'section' || obj[i].controlType.toLowerCase() !== 'tabs') {
          if (obj[i].validators && !obj[i].required) {
            for (let j = 0; j < obj[i].validators.length; j++) {

              if (obj[i].typeOf && obj[i].typeOf.toLowerCase() != 'booleantype' && (obj[i].validators[j].name === 'RequiredValidator' || obj[i].validators[j].name === 'RequiredFieldValidator')) {
                let message = this.requiredfieldservice.requiredfield(obj[i]);
                //console.log('vLUE'+message);
                let isrequired = (obj[i].validators[j].options[0] != null || obj[i].validators[j].options[0] != undefined) ? obj[i].validators[j].options[0].value : false;
                //console.log('vLUE '+isrequired);
                if (message && message !== '' && isrequired == true) {
                  store.push(this.getResourceByStringReplacer(obj[i].validators[j].name,[this.generateFieldKey(obj[i])]));
                  //store.push(this.getResourceValue(this.requiredmsg+this.entityName+'_'+ message.replace('.','_').toLowerCase())); // by debdut
                }
              }
            }
          }
          if (obj[i].required && obj[i].typeOf && obj[i].typeOf.toLowerCase() != 'booleantype') {
            // tslint:disable-next-line: no-trailing-whitespace

            let messages = this.requiredfieldservice.requiredfield(obj[i]);
            //console.log(messages);
            if (messages && messages !== '') {
              store.push(this.getResourceByStringReplacer('RequiredValidator',[this.generateFieldKey(obj[i])]));
              //store.push(this.getResourceValue(this.requiredmsg+this.entityName+'_'+messages.replace('.','_').toLowerCase()));// by debdut
            }
          }
          if (obj[i] != null && obj[i].value != null && obj[i].value != undefined) {
            let messages = this.Validatefield(obj[i], obj[i].value,this.entityName);
            if (messages && messages !== '') {
              store.push(messages);
            }
          }


          if (obj[i].fields) {
            this.valdiateHelper(obj[i].fields, store);
          }
        }

        if (obj[i].controlType && obj[i].controlType.toLowerCase() === 'section' || obj[i].controlType.toLowerCase() === 'tabs') {

          if (obj[i].tabs) {
            this.valdiateHelper(obj[i].tabs, store);
          }
        }
      }
    }
  }
  public requiredFieldMsg(obj: any): string {
    return this.getResourceByStringReplacer('RequiredValidator',[this.generateFieldKey(obj)]);
    //return  this.getResourceValue(this.requiredmsg+this.entityName+'_'+obj.name.replace('.','_').toLowerCase()); //by debdut
  }
  public Validatefield(obj: any, inputvalue: string,entityName:string): string {
    this.entityName= obj.entityName==='' ||obj.entityName===undefined ? entityName: obj.entityName.toLowerCase();
    // console.log(inputvalue);
    if (inputvalue == "" || inputvalue == undefined) {
      return '';
    }
    //debugger;
    let Nooflength = 0;
    // console.log(JSON.stringify(obj));
    if (obj != undefined && obj.controlType.toLowerCase() !== 'section' || obj.controlType.toLowerCase() !== 'tabs') {

      if (obj.dataType.toLowerCase() === 'number' && inputvalue.length > 0 && !String(inputvalue).match(this.regex.number) && obj.typeOf.toLowerCase() != 'decimaltype') {
       return this.getResourceByStringReplacer('numbervalidator',[this.generateFieldKey(obj)]);
        //return this.getResourceValue(this.numberValidatormsg+this.entityName+'_'+ obj.name.replace('.','_').toLowerCase()); // by debdut
      }
      if (obj.dataType.toLowerCase() === 'number' && inputvalue.length > 0 && !String(inputvalue).match(this.regex.decimal) && obj.typeOf.toLowerCase() == 'decimaltype') {
        return this.getResourceByStringReplacer('numbervalidator',[this.generateFieldKey(obj)]);
        //return this.getResourceValue(this.numberValidatormsg+this.entityName+'_'+ obj.name.replace('.','_').toLowerCase()); //by debdut
      }
      //console.log('length '+obj.validators.length);
      if (obj.validators) {
        for (let j = 0; j < obj.validators.length; j++) {

          // console.log("test" + obj.validators[j].name);
          if (obj.validators[j].name === 'LengthValidator') {
            //console.log('LengthValidator');
            // console.log(obj.validators[j].UserSetlength+" DB lenght:-"+ obj.validators[j].dblength);

            if (obj.validators[j].userSetlength === null && obj.validators[j].dblength === null) {
              return '';
            }

            Nooflength = (obj.validators[j].userSetlength != null) ? obj.validators[j].userSetlength : obj.validators[j].dblength;
            Nooflength = Nooflength == undefined ? obj.validators[j].userSetlenght : Nooflength;

            // console.log(" length" + Nooflength);
            // console.log(" input length" + inputvalue.length);
            //console.log(' resoucre'+JSON.stringify(obj.validators[j]));
            
            if (Nooflength !== undefined && Nooflength !== 0 && inputvalue.length!=undefined) {
              if (!(inputvalue.length <= Nooflength)) {
                return this.getResourceByStringReplacer('maxlengthvalidator',[this.generateFieldKey(obj),Nooflength]);
                //return this.getResourceValue(this.maxLengthValidatormsg+this.entityName+'_'+ obj.name.replace('.','_').toLowerCase()) ; // by debdut

              }
            }
            //Nov-345
            Nooflength = (obj.validators[j].userSetlength != null) ? obj.validators[j].userSetlength : obj.validators[j].mindblength;
            Nooflength = Nooflength == undefined ? obj.validators[j].userSetlenght : Nooflength;
            
            if (Nooflength !== undefined && Nooflength !== 0 && inputvalue.length!=undefined) {
              if (!(inputvalue.length <= Nooflength)) {
                return this.getResourceValue(this.minLengthValidatormsg+this.entityName+'_'+ obj.name.replace('.','_').toLowerCase()) ;
                return this.getResourceByStringReplacer('minlengthvalidator',[this.generateFieldKey(obj),Nooflength]);

              }
            } // end

          }
          if (obj.validators[j].name === 'RangeValidator') {
            //console.log('RangeValidator');
            // console.log(JSON.stringify(obj.validators[j]));
            const minLength = obj.validators[j].options.find((x: { name: string; }) => x.name === 'minLength').value;
            const maxLength = obj.validators[j].options.find((x: { name: string; }) => x.name === 'maxLength').value;

            // console.log(minLength);
            // console.log(maxLength);
            // console.log(inputvalue);

            if (!isNaN(Number(inputvalue)) && (Number(inputvalue) < minLength || Number(inputvalue) > maxLength)) {
              return this.getResourceByStringReplacer(obj.validators[j].name,[this.generateFieldKey(obj),minLength,maxLength]);
              //return this.getResourceValue(this.rangeValidatormsg+this.entityName+'_'+obj.name.replace('.','_').toLowerCase()); // by debdut
            }


          }
          if (obj.validators[j].name === 'RegularExpression') {
            //console.log('RegularExpression');
            const pattern = obj.validators[j].options.find((x: { name: string; }) => x.name === 'regEx').value;
            return inputvalue.match("/\b(" + pattern + ")\b/") ? '' : this.getResourceByStringReplacer(obj.validators[j].name,[this.generateFieldKey(obj)]);
            //return inputvalue.match("/\b(" + pattern + ")\b/") ? '' : this.getResourceValue(this.regularExpressionValidatormsg+this.entityName+'_'+obj.name.replace('.','_').toLowerCase()); //by debdut
          }

          if (obj.validators[j].name === 'EmailFormatValidator') {
            //console.log('EmailFormatValidator');
            let pattern = /^\b[A-Z0-9._%-]+@[A-Z0-9.-]+\.[A-Z]{2,4}\b$/i;
            if (!pattern.test(inputvalue)) {
              return this.getResourceByStringReplacer(obj.validators[j].name,[this.generateFieldKey(obj)]);
              //return this.getResourceValue(this.emailFormatemsg+this.entityName +'_'+ obj.name.replace('.','_').toLowerCase()); // by debdut
            }
            // return inputvalue.match(regex) ? '' : this.resource[this.generateResourceName(obj.name)] + '\' is not invalid!';

          }
          if (obj.validators[j].name === 'PercentValidator') {
            let pattern = /(^100(\.0{1,2})?$)|(^([1-9]([0-9])?|0)(\.[0-9]{1,2})?$)/i;
            //console.log('PercentValidator');
            if (!pattern.test(inputvalue)) {
              return this.getResourceByStringReplacer(obj.validators[j].name,[this.generateFieldKey(obj)]);
              //return this.getResourceValue(this.percentValidatoemsg+this.entityName+'_'+obj.name.replace('.','_').toLowerCase()); //y debdut
            }
          }
        }
      }
      if (obj.fields.any) {

        this.Validatefield(obj.fields, inputvalue,this.entityName);
      }
    }

    return '';
  }

  getResourceValue(key: string) {
    return this.globalResourceService.getResourceValueByKey(key);
  }
  generateFieldKey(obj:any){
    return this.globalResourceService.getResourceValueByKey(this.entityName.toLowerCase()+'_field_'+obj.name.replace('.','_').toLowerCase());
    
}

  getResourceByStringReplacer(validationName:string,object:any[]) {
    return this.globalResourceService.getResourceByStringReplacer('validation_'+validationName,object);
  }
}
