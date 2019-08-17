import { Component, OnInit } from '@angular/core';
import { NgbModal, NgbModalOptions, ModalDismissReasons } from '@ng-bootstrap/ng-bootstrap';
import { TosterService } from 'src/app/services/toster.service';
import { BatchItemService } from './batchitem.service';
import { first } from 'rxjs/operators';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { MenuService } from '../services/menu.service';
import{GlobalResourceService} from '../global-resource/global-resource.service';
import { Resource } from '../model/resource';
import {CommonService } from 'src/app/services/common.service';

@Component({
  selector: 'app-batchitem',
  templateUrl: './batchitem.component.html',
  styleUrls: ['./batchitem.component.css']
})
export class BatchItemComponent implements OnInit {  
  public gridData: any[];
  resource:Resource; 
  public pageSize: number = this.commonService.defaultPageSize();
  batchTypeId:string;
  itemType:number;

  
  constructor(private activatedRoute: ActivatedRoute, private router: Router, private commonService: CommonService, private batchItemService: BatchItemService, private toster: TosterService,
       private globalResourceService: GlobalResourceService, public menuService: MenuService
  ) { }

  ngOnInit() {  
    this.itemType = this.activatedRoute.snapshot.queryParams["type"];
    this.activatedRoute.params.subscribe((params: Params) => {
      this.batchTypeId = params['id']; 
    });
    this.getBatchItems();
  }

  getBatchItems() {
  
    this.batchItemService.getBatchItems(this.batchTypeId,this.itemType).pipe(first()).subscribe(
        data => {
          if (data) {
            this.gridData = data;
          }
        },
        error => {
          console.log(error);
        });
  }  

 public showReadyToSend()
  {
    return this.itemType==1;
  }

 public showFailed()
  {
    return this.itemType==3;
  }
 
  getResourceValue(key) {
    return this.globalResourceService.getResourceValueByKey(key);
  }

}
