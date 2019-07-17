import { Injectable, Output, EventEmitter } from '@angular/core';
import { Resource } from '../model/resource';
import { ResourceService } from '../resource/resource.service';
import { first } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class GlobalResourceService {
  public resource: Resource;
  public languages: any;

  @Output() resourcePopulated: EventEmitter<any> = new EventEmitter();
  constructor(private resourceService: ResourceService) { }


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
    var lang = JSON.parse(localStorage.getItem('langInfo'));
    if (lang === null) {
      lang = {
        key: ''
      }
    }
    
    this.resourceService.getAllResources(lang.key)
      .pipe(first())
      .subscribe(
        data => {
          if (data) {
            this.resource = data;
            this.resourcePopulated.emit('Resource populated')
          }
        },
        error => {
          console.log(error)
        }
      );
  }
  public getGlobalResources(): any {
    return this.resource}




  public setGlobalresources(resource: Resource) {
    this.resource = resource;
    //console.log('Global Resource set : '+JSON.stringify( this.resource));
  }
  getResourceByKey(key: any) {
    if (this.resource[this.generateResourceName(key)]) {
      return this.resource[this.generateResourceName(key)];
    } else {
      return '[' + key + ']';
    }
  }
  getResourceValueByKey(key: any) {
    if (this.resource[key]) {
      return this.resource[key];
    } else {
      return '[' + key + ']';
    }
  }
  generateResourceName(word) {
    if (!word) return word;
    return word[0].toLowerCase() + word.substr(1);
  }

}
