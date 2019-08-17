import { Component, Input, OnInit, Output, EventEmitter, ChangeDetectorRef, DoCheck } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { nodeName } from '../helper/utils';
import { TreeService } from '../service/tree.service';
import { first } from 'rxjs/operators';
import { CommunicationService } from '../../services/communication.service';
import { AtomBase } from './atombase';
import { Broadcaster } from '../messaging/broadcaster';
import { MessageEvent } from '../messaging/message.event';
import { Payload } from '../messaging/payload';
import * as _ from 'lodash';
import { NgbModalOptions, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { MODALS } from '../tree.config';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { query } from '@angular/animations';


@Component({
  selector: 'link-item',
  template: `  
    <div *ngIf="mode!==2" >
        <div> 
            <a class="text-link" (click)="gotoUrl(field)">{{field.value}}</a>
        </div> 
    </div>
    <div *ngIf="mode===2"> 
      <label class="text-label-preview">{{field.value}}</label>
    </div>
    `,
  styles: [`
     
  div.dropdown-wrapper select {  
      background-color:transparent; 
      background-image:none; 
      -webkit-appearance: none; 
      border:none; 
      box-shadow:none; 
      padding:0px; 
  }
  `]
})

export class LinkComponent implements OnInit, DoCheck {
  

  @Input() field: any = {};
  @Input() form: FormGroup;
  @Input() mode: Number;

  public entityname: any;

  @Output() changeEmitter: EventEmitter<any>


  // @Output() OnMetadatapicklistChangeevent = new EventEmitter<string>();
  
  constructor(
    private refChangedetect: ChangeDetectorRef,
    private treeService: TreeService,
    public broadcaster: Broadcaster,
    private communicationService: CommunicationService,
    private modalService: NgbModal,
    private activatedRoute:ActivatedRoute,
    private router:Router,
  ) {
    this.changeEmitter = new EventEmitter();
  }
  ngOnInit(): void {

  }
  ngDoCheck(): void {
    
  }
  gotoUrl(){
    if(this.field.setting && this.field.setting.linkSetting 
      && this.field.setting.linkSetting.type!=null && this.field.setting.linkSetting.url!=""){
      //var mode = (this.field.setting.linkSetting.type==1)?"_self":"_blank";
       // var id='';
     // this.activatedRoute.params.subscribe((params: Params) => {
      //  id = params['id'];
      //});

     // this.router.navigate(["item/"], {relativeTo: this.activatedRoute});
    // var aaa="['item'], { queryParams: { type: 1 }, relativeTo: this.activatedRoute";
      //this.router.navigate(aaa);
     // var url = this.field.setting.linkSetting.url.replace("{id}", id); 
     // window.open(url, mode);

    //  var rout="item";
    //  var query="type: 3"
    //  var rela=this.activatedRoute

     //var arr=["item","1","this.activatedRoute"]
     var route=null;
     var queryParameter=null;
     var whicPath=null;
     var itsRouteNavigation=eval(this.field.setting.linkSetting.url);
     for(var i=0; i<2 ; i++)
     {
       if(i==0)
           route=itsRouteNavigation[i];
        else  if(i==1)
           queryParameter= itsRouteNavigation[i];
        else  if(i==2)
           whicPath=itsRouteNavigation[i];

     }
     

     //var aaa="['item'], { queryParams: { type: 3 }, relativeTo: this.activatedRoute}";
     this.router.navigate([route],{queryParams: { type: queryParameter }, relativeTo: (whicPath==null ? this.activatedRoute : null)});
    }
  }
}
