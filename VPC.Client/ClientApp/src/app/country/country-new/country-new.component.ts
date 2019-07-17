import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { LayoutModel } from '../../model/layoutmodel';
import { ITreeNode } from 'src/app/dynamic-form-builder/tree.module';
import { TosterService } from 'src/app/services/toster.service';
import { CountryService } from "../../country/country.service";
import { Observable, Subject } from 'rxjs';
import { first } from "rxjs/operators";
import {CommonService} from 'src/app/services/common.service';
import { GlobalResourceService } from '../../global-resource/global-resource.service';
import { Resource } from '../../model/resource';
@Component({
  selector: 'app-country-new',
  templateUrl: './country-new.component.html',
  styleUrls: ['./country-new.component.css']
})
export class CountryNewComponent implements OnInit {

  public layoutInfo: LayoutModel = new LayoutModel();
  public tree: ITreeNode;
  public selectedTreeNode: ITreeNode | null;
  public isTreeReady: boolean = false;
  id: string;
  entityname: string;
  currentPage: number = 1;
  pageSize: number = 10;
  result: any;
  private layoutType: number = 2;//List page
  private layoutContext: number = 1;
  public resource: Resource;

  constructor(
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private countryService: CountryService,
    private toster: TosterService,
    private commonService: CommonService,
    private globalResourceService: GlobalResourceService,
  ) { }

  ngOnInit() {
    this.resource = this.globalResourceService.getGlobalResources();
    this.entityname = "Country";
    this.getDefaultLayout(this.entityname);

  }


  private getDefaultLayout(entityName) {
    this.countryService.getDefaultLayout(entityName, this.layoutType, this.layoutContext)
      .pipe(first())
      .subscribe(
        data => {
          if (data) {
            this.tree = data.formLayoutDetails;
            this.isTreeReady = true;
          }
        },
        error => {
          console.log(error);
        });
  }

  public saveUser() {
    // var value = {};
    // this.createKeyValue(this.tree.fields, value);

    let value = {};
    value = this.commonService.createKeyValue(this.tree.fields, value);

    this.countryService.saveCountry(this.entityname, value)
      .pipe(first())
      .subscribe(
        data => {
          //console.log('data');
          this.toster.showSuccess(this.resource[this.generateResourceName("CountrySavedSuccessfully")]);
          this.router.navigate(['country']);
        },
        error => {
          console.log(error);
        });
  }

  // private createKeyValue(data: TreeNode[], savedData: any) {
  //   data.forEach(element => {
  //     if (element.controlType.toLocaleLowerCase() != "section" && element.controlType.toLocaleLowerCase() != "tabs") {
  //       console.log(element);
  //       savedData[element.name] = element.value;
  //     }
  //     if (element.fields) {
  //       this.createKeyValue(element.fields, savedData);
  //     }
  //   });
  // }
  generateResourceName(word)
  {
     if (!word) return word;
     return word[0].toLowerCase() + word.substr(1);
   }
}
