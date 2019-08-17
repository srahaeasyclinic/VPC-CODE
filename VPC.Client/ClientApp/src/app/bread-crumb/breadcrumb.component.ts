import { Component, OnInit, Pipe, PipeTransform, OnChanges, Input, EventEmitter, Output } from '@angular/core';
import { Router, UrlTree, UrlSegment, UrlSegmentGroup, PRIMARY_OUTLET, RouterState, ActivatedRoute, NavigationStart, Params } from '@angular/router';
import { filter, first } from 'rxjs/operators';
import { BreadcrumbsService,Ibreadcrumbs } from './BreadcrumbsService';
import { CommonService } from '../services/common.service';
import { GlobalResourceService } from '../global-resource/global-resource.service';
import { MenuService } from '../services/menu.service';
import { NewMenuItem } from '../model/menuItem';
import * as _ from 'lodash';
import { RoutelocalizationService } from '../services/routelocalization.service';

@Component({
  selector: 'app-breadcrumb',
  templateUrl: './breadcrumb.component.html',
  styleUrls: ['./breadcrumb.component.css']
})
export class BreadcrumbComponent implements OnInit, OnChanges {

  @Input() segmenttree: Ibreadcrumbs[];
  @Output() groupClickEvent: EventEmitter<any> = new EventEmitter();

  private breadcrumbComplete: boolean = false;
  public resource: any;
  public firstElement: NewMenuItem;
  constructor(
    private router: Router,
    private breadcrumService: BreadcrumbsService,
    public commonService: CommonService,
    private globalResourceService: GlobalResourceService,
    private menuService: MenuService,
    private localization:RoutelocalizationService,
  ) { }

  ngOnInit() {
    
  }

  ngOnChanges() {
    
    //console.log('onChange');
    //console.log('this.commonService.getDisplayName4Breadcrumb() ', this.commonService.getDisplayName4Breadcrumb());
  }
  openPageWithBreadcrumb(value: any) {
    //console.log(value);
    if (this.menuService.isGUID(value.elementURL))
    {
      this.getFirstelementfromGroup(value.elementURL);
      this.groupClickEvent.emit(this.firstElement);
      
    } else {
       let url = this.localization.getlocalizeUrl(value.elementURL);
      this.breadcrumService.splicebreadcrums(value);
      //this.groupClickEvent.emit(null);
      this.router.navigate([url]);
    }
   
  }

  getResourceByKey(key: any) {
    if(this.resource[this.generateResourceName(key)]){
      return this.resource[this.generateResourceName(key)];
    }else{
      return key;
    }
  }
  generateResourceName(word)
  {
     if (!word) return word;
     return word[0].toLowerCase() + word.substr(1);
  }
  
  getFirstelementfromGroup(group:any)
  {
    if (group == undefined && group == null) return null;
    
    let subgroup = this.menuService.getCacheMenus().filter(d => d.parentId == group && d.isMenuGroup==true);
    let firystelemnt=_.sortBy(subgroup, 'sortItem');
    if (firystelemnt!=undefined && firystelemnt!=null) {
      this.firstElement=firystelemnt[0];
    } else {
      return this.firstElement=group;
    }
  }
}