import { Component, OnInit, Input, OnChanges, SimpleChange } from '@angular/core';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';
import { first } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { GridDataResult, DataStateChangeEvent } from '@progress/kendo-angular-grid';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { TosterService } from 'src/app/services/toster.service';
import { LayoutService } from '../meta-data/layout/layout.service';
import { LayoutModel } from '../model/layoutmodel';
import { ResourceService } from '../services/resource.service';
import{GlobalResourceService} from '../global-resource/global-resource.service';
import { MenuService } from '../services/menu.service'

@Component({
  selector: 'general-ui-display',
  templateUrl: './general-ui-display.component.html',
  styleUrls: ['./general-ui-display.component.css'],

})
export class GeneralUiDisplayComponent implements OnInit {

  public view: Observable<GridDataResult>;
  public gridRoleData: any[];
  public roleInfo = { name: '', roleType: '' };
  private roleTypes = [];

  private freesearchQuery: string;
  private simplesearchQuery: string;
  public displaysearchQuery: string;
  public resource: any;
  private entityName: string = '';
  private subType: string = '';
  private pageType: string = '';
  public defaultLayout: LayoutModel = null;

  constructor(private activatedRoute: ActivatedRoute,
    private router: Router,
    private resourceService: ResourceService,
    private toster: TosterService,

    public globalResourceService:GlobalResourceService,
    private layoutService: LayoutService,
    public menuService: MenuService) { }

  ngOnInit() {
    this.resource = this.globalResourceService.getGlobalResources();
    this.subType = this.activatedRoute.snapshot.queryParams["subType"];

    //Get the entity name from URL route 
    this.activatedRoute.parent.params.subscribe((urlPath) => {
      this.entityName = urlPath["name"];

      if (this.activatedRoute.snapshot.url !== null && this.activatedRoute.snapshot.url.length > 0)
        this.pageType = this.activatedRoute.snapshot.url[0].path

      if (this.entityName) {
        if (this.pageType !== null && this.pageType !== "" && this.pageType === "new") {
          this.getDefaultLayout(this.entityName, 'Form', this.subType, 'New');
        }
        else if (this.pageType !== null && this.pageType !== "" && this.pageType === "edit") {
          this.getDefaultLayout(this.entityName, 'Form', this.subType, 'Edit');
        }
        else if (this.pageType !== null && this.pageType !== "" && this.pageType === "preview") {
          this.getDefaultLayout(this.entityName, 'Form', this.subType, 'Edit');
        }
        else {
          this.getDefaultLayout(this.entityName, 'List', '', '');
        }
      } else {
        this.toster.showWarning(this.getResourceValue('UrlTemperedorNoEntityNameoridFoundorEntityNotYetDecorated'));
      }
    });
  }

  // private getlayoutDetails() {
  //   if (this.entityName) {
  //     if (this.pageType !== null && this.pageType !== "" && this.pageType === "new") {
  //       this.getDefaultLayout(this.entityName, 'Form', this.subType, 'New');
  //     }
  //   });
  // }

  public onSearchQueryEvent($event) {
    this.displaysearchQuery = $event;
  }
  private onFreeSearchEvent($event) {
    //this.freesearchQuery = $event;

    //console.log('onFreeSearchEvent GUID :: '+this.displaysearchQuery);
  }
  private onSimpleSearchEvent($event) {
    //this.simplesearchQuery = $event;
    this.displaysearchQuery = $event;
    //console.log('onSimpleSearchEvent GUID :: '+this.displaysearchQuery);
  }


  private getDefaultLayout(entityName: string, type: string, subtype: string, context: string): void {
    this.layoutService.getDefaultLayout(entityName, type, subtype, context)
      .pipe(first())
      .subscribe(
        data => {
          if (data) {
            this.defaultLayout = data;
          }
        },
        error => {
          console.log(error);
        });
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
