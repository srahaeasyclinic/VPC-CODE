import { Injectable, Output, EventEmitter } from '@angular/core';
import { Resource } from '../model/resource';
import { ResourceService } from '../resource/resource.service';
import { RoutelocalizationService } from '../services/routelocalization.service';
import { first } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class GlobalResourceService {
  public resource: Resource;
  public languages: any;
  @Output() notifyConfirmationDelete: EventEmitter<any> = new EventEmitter();
  @Output() openDeleteModal: EventEmitter<any> = new EventEmitter();
  @Output() resourcePopulated: EventEmitter<any> = new EventEmitter();
  constructor(private resourceService: ResourceService, private localization: RoutelocalizationService) { }


  private GetTenantLanguage() {
    this.resourceService.getDefaultLanguage()
      .pipe(first())
      .subscribe(
        data => {
          if (data && data.retVal && data.retVal[0]) {
            var defaultLanguage = data.retVal[0];
          }
        },
        error => {
          console.log(error);
        }
      );
  }

  public getResource(): void {
    var lang = this.localization.getDefaultlanguageKey();
    if (lang === null) {
      lang = '';
    }

    this.resourceService.getAllResources(lang)
      .pipe(first())
      .subscribe(
        data => {
          if (data) {
            this.resource = data;
            this.resourcePopulated.emit();
          }
        },
        error => {
          console.log(error)
        }
      );
  }
  public getGlobalResources(): any {
    return this.resource
  }




  public setGlobalresources(resource: Resource) {
    this.resource = resource;
    //console.log('Global Resource set : '+JSON.stringify( this.resource));
  }
  getResourceByKey(key: any) {
    if (this.resource) {
      if (this.resource[this.generateResourceName(key)]) {
        return this.resource[this.generateResourceName(key)];
      } else {
        return '[' + key + ']';
      }
    } else {
      return '[' + key + ']';
    }
  }
  getResourceValueByKey(key: any) {
    if (this.resource) {
      if (this.resource[key]) {
        return this.resource[key];
      } else {
        return '[' + key + ']';
      }
    }
    else {
      return '[' + key + ']';
    }
  }
  generateResourceName(word) {
    if (!word) return word;
    return word[0].toLowerCase() + word.substr(1);
  }

  public getResourceByStringReplacer(validationName: string, object: any[]) {
    return this.getResourceValueByKey(validationName.toLocaleLowerCase())
      .replace(/\{\s*([^}\s]+)\s*\}/g, function (m, p1, offset, string) {
        return object[p1];
      });
  }

  //region For Validation Messages
  public requiredValidator(fieldName: string) {
    return this.getResourceByStringReplacer('validation_requiredvalidator', [this.getResourceValueByKey(fieldName.toLowerCase())]);
  }

  public numberValidator(fieldName: string) {
    return this.getResourceByStringReplacer('validavalidation_numbervalidator', [this.getResourceValueByKey(fieldName.toLowerCase())]);
  }

  public maxLengthValidator(fieldName: string, dbLength: any) {
    return this.getResourceByStringReplacer('validation_maxlengthvalidator', [this.getResourceValueByKey(fieldName.toLowerCase()), dbLength]);
  }

  public minLengthValidator(fieldName: string, dbLength: any) {
    return this.getResourceByStringReplacer('validatoion_minlengthvalidator', [this.getResourceValueByKey(fieldName.toLowerCase()), dbLength]);
  }
  public rangeValidator(fieldName: string, minLength: any,maxLength:any) {
    return this.getResourceByStringReplacer('validation_rangevalidator', [this.getResourceValueByKey(fieldName.toLowerCase()), minLength,maxLength]);
  }

  public regularExpressionValidator(fieldName: string) {
    return this.getResourceByStringReplacer('validation_regularexpression', [this.getResourceValueByKey(fieldName.toLowerCase())]);
  }
  public percentageValidator(fieldName: string) {
    return this.getResourceByStringReplacer('validation_percentvalidator', [this.getResourceValueByKey(fieldName.toLowerCase())]);
  }

  public emailValidator(fieldName: string) {
    return this.getResourceByStringReplacer('validation_emailformatvalidator', [this.getResourceValueByKey(fieldName.toLowerCase())]);
  }
  // end region

  //region Success Messages
  public createSuccessMessage(fieldName: string) {
    return this.getResourceByStringReplacer('operation_create_success_message', [this.getResourceValueByKey(fieldName.toLowerCase())]);
  }
  public addSuccessMessage(fieldName: string) {
    return this.getResourceByStringReplacer('operation_add_success_message', [this.getResourceValueByKey(fieldName.toLowerCase())]);
  }
  public saveSuccessMessage(fieldName: string) {
    return this.getResourceByStringReplacer('operation_save_success_message', [this.getResourceValueByKey(fieldName.toLowerCase())]);
  }
  public updateSuccessMessage(fieldName: string) {
    return this.getResourceByStringReplacer('operation_update_success_message', [this.getResourceValueByKey(fieldName.toLowerCase())]);
  }
  public printSuccessMessage(fieldName: string) {
    return this.getResourceByStringReplacer('operation_print_success_message', [this.getResourceValueByKey(fieldName.toLowerCase())]);
  }
  public repairSuccessMessage(fieldName: string) {
    return this.getResourceByStringReplacer('operation_repair_success_message', [this.getResourceValueByKey(fieldName.toLowerCase())]);
  }
  public deleteSuccessMessage(fieldName: string) {
    return this.getResourceByStringReplacer('operation_delete_success_message', [this.getResourceValueByKey(fieldName.toLowerCase())]);
  }
  public resetSuccessMessage(fieldName: string) {
    return this.getResourceByStringReplacer('operation_reset_success_message', [this.getResourceValueByKey(fieldName.toLowerCase())]);
  }
  public updatestatusSuccessMessage(fieldName: string) {
    return this.getResourceByStringReplacer('operation_updatestatus_success_message', [this.getResourceValueByKey(fieldName.toLowerCase())]);
  }
  public enableSuccessMessage(fieldName: string) {
    return this.getResourceByStringReplacer('operation_enable_success_message', [this.getResourceValueByKey(fieldName.toLowerCase())]);
  }
  public disableSuccessMessage(fieldName: string) {
    return this.getResourceByStringReplacer('operation_disable_success_message', [this.getResourceValueByKey(fieldName.toLowerCase())]);
  }
  public cloneSuccessMessage(fieldName: string) {
    return this.getResourceByStringReplacer('operation_clone_success_message', [this.getResourceValueByKey(fieldName.toLowerCase())]);
  }
  // end region
}
