import { Component, OnInit } from '@angular/core';
import { GlobalResourceService } from '../global-resource/global-resource.service'
import { Resource } from '../model/resource';

@Component({
  selector: 'app-notfoundcomponent',
  templateUrl: './notfoundcomponent.component.html',
  styleUrls: ['./notfoundcomponent.component.css']
})
export class NotfoundcomponentComponent implements OnInit {
  message:string;
  public resource: Resource;

  constructor(private globalResourceService: GlobalResourceService,) { }

  ngOnInit() {
    this.resource = this.globalResourceService.getGlobalResources();
    this.message = this.getResourceValue("TheComponentDoesNotExists");
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
