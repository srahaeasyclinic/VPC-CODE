import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute, Params, Route, UrlTree, UrlSegmentGroup, UrlSegment, UrlSerializer } from '@angular/router';
import { Spinkit } from 'ng-http-loader';
import { MenuService } from '../services/menu.service';
import { MenuItem } from '../model/menuItem';
import { NewMenuItem } from '../model/menuItem';
import { first } from 'rxjs/operators';

import { MenuItemComponent } from '../menu-item/menu-item.component';
import { MetadataComponent } from '../meta-data/metadata.component';
import { MetadataDetailComponent } from '../meta-data/metadatadetail.component';
import { FieldsComponent } from '../meta-data/fields/fields.component';
import { AuthorizationCheck } from '../interceptor/authorizationCheck'

import { Type } from '@angular/compiler';
import { BaseIntialiser } from './baseintialiser.component';
import { CommonService } from '../services/common.service';
import{GlobalResourceService} from '../global-resource/global-resource.service';

@Component({
    selector: 'picklist-designer-init',
    templateUrl: './initialiser.component.html',
    styleUrls: ['./initialiser.component.css']
})
export class PicklistDesignerIntialiser extends BaseIntialiser implements OnInit {
    name: string;
    public spinkit = Spinkit;
    public leftmenuItems: Array<MenuItem>;
    public leftmenuItemsNew: any;
    group: string;
    isMenuopen: boolean = false;
    public groupName: string = "";
    public menuItem: any;
    public componentName: any;
    public childMenu: any;
    public menus: Array<NewMenuItem>;
    public currentGroup: NewMenuItem;
    public currentMenu: NewMenuItem;
    constructor(
        public router: Router,
        public activatedRoute: ActivatedRoute,
        public menuService: MenuService,
        public serializer: UrlSerializer,
        public commonService:CommonService,
        public globalResource:GlobalResourceService
    ) {
        super(router, activatedRoute, menuService, serializer, commonService,globalResource);
    }

    ngOnInit() {
        super.ngOnInit();
    }
   
}
