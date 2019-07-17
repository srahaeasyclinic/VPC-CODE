import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { LayoutService } from '../layout.service';
import { first } from 'rxjs/operators';
import { LayoutModel } from '../../../model/layoutmodel';
import { MetadataService } from "../../metadata.service";
import { GlobalResourceService } from 'src/app/global-resource/global-resource.service';
import { Resource } from 'src/app/model/resource';

@Component({
  selector: 'app-layoutdetail',
  templateUrl: './layoutdetail.component.html',
  styleUrls: ['./layoutdetail.component.css']
})
export class LayoutdetailComponent implements OnInit {
  private layoutDetail = new LayoutModel();
  layoutInfo: any = this.layoutDetail;
  id: string;
  public resource: Resource;
  navLinks = [
    { path: 'layoutfields', label: 'Fields' },
    { path: 'freetextsearch', label: 'Free text search' },
    { path: 'simplesearch', label: 'Simple search' },
    { path: 'advancesearch', label: 'Advance search' },
    { path: 'layouttoolbar', label: 'Toolbar' },
    { path: 'layoutaction', label: 'Actions' }
  ];

  constructor(
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private layoutService: LayoutService,
    private metadataService: MetadataService,
    public globalResourceService: GlobalResourceService
  ) { }



  ngOnInit() {
    this.activatedRoute.params.subscribe((params: Params) => {
      this.id = params['id'];
      if (this.id) {
        //this.getLayoutById(this.id);
      }
    });
    this.resource = this.globalResourceService.getGlobalResources();
  }

  //private getLayoutById(layoutId) {
  //  this.layoutService.getLayoutById(layoutId)
  //    .pipe(first())
  //    .subscribe(
  //      data => {
  //        console.log("data", data);
  //        if (data && data) {
  //          console.table(data);
  //          this.layoutInfo = data;
  //        }

  //      },
  //      error => {
  //        console.log(error);
  //      });
  //}
  getResourceValue(key) {
    return this.globalResourceService.getResourceValueByKey(key);
  }

}
