import { Component, OnInit } from '@angular/core';
import { DropDownsModule } from '@progress/kendo-angular-dropdowns';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';
import { first } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { QueryService } from './query.service';
import { Entities } from '../../model/entities';
import { GridDataResult, DataStateChangeEvent, PageChangeEvent } from '@progress/kendo-angular-grid';
import { NgMultiSelectDropDownModule } from 'ng-multiselect-dropdown';
import { anyChanged } from '@progress/kendo-angular-grid/dist/es2015/utils';
import { TosterService } from '../../services/toster.service';
import { GlobalResourceService } from 'src/app/global-resource/global-resource.service';
import { Resource } from 'src/app/model/resource';
import { MetadataService } from '../metadata.service';

@Component({
  selector: 'app-query',
  templateUrl: './query.component.html',
  styleUrls: ['./query.component.css']
})
export class QueryComponent implements OnInit {

  //* Variables */
  private entityName: string;
  public fields: Array<any>;
  selectedFields: Array<any>;
  selectedSortOrder: any;
  filterArray: Array<any> = [];
  newFilterAttribute: any = {};
  public skip = "0";
  public pageSize = 10;
  public pageIndex = 1;
  results: any;
  public query: string;
  public operators: Array<any>;
  public resource: Resource;
  sortResults: any;
  maxvalue: any;
  public columns = [];
  public selectedOrderOptions = ["ASC", "DESC"];
  public dropdownSettings = {
    singleSelection: false,
    idField: 'name',
    textField: 'name',
    selectAllText: 'Select All',
    unSelectAllText: 'UnSelect All',
    itemsShowLimit: '10',
    allowSearchFilter: true
  };

  constructor(private route: ActivatedRoute,
    private router: Router,
    private queryService: QueryService,
    private toster: TosterService,
    private metadataService: MetadataService,
    public globalResourceService: GlobalResourceService) {

  }

  ngOnInit() {

    this.route.parent.url.subscribe((urlPath) => {
      this.entityName = urlPath[urlPath.length - 1].path;
      this.getEntitiesByName(this.entityName);
      this.getOperators();
    });


  }

  private getOperators() {
    this.queryService.getOperators()
      .pipe(first())
      .subscribe(
        data => {
          if (data) {
            this.operators = data.result;
          }
        },
        error => {
          console.log(error);
        });
  }

  private getEntitiesByName(name) {

    if (this.metadataService.get_metadataByName(name)) {
      let data = this.metadataService.get_metadataByName(name)
      this.fields = data.fields.filter(function (filed) {
        return filed.isQueryable;
      });
      //console.log('if');
    }
    else {
      this.metadataService.getMetadataByName(name)
        .pipe(first())
        .subscribe(
          data => {
            if (data) {
              this.metadataService.set_metadataByName(data, name);
              this.fields = data.fields.filter(function (filed) {
                return filed.isQueryable;
              });
            }
          },
          error => {
            console.log(error);
          });
      //console.log('else');
    }
  }

  filterOnlyQuryableFileds(element, index, array) {
    return (element >= 10);
  }

  addFilterValue() {
    if (this.filterArray && this.filterArray.length == 0) {
      this.filterArray.push({ name: null, value: null, operator: null });
    } else {
      this.filterArray.push(this.newFilterAttribute);
      this.newFilterAttribute = {};
    }
  }

  removeFilterValue(index) {
    this.filterArray.splice(index, 1);
  }


  private buildQueryParam() {
    var query: string = "fields=" + this.selectedFields;
    if (this.filterArray && this.filterArray.length > 0) {
      query += "&filter=";
      this.filterArray.forEach(element => {
        if (element.name != null && element.operator != null && element.value != null) {
          query += element.name + "," + element.operator + "," + element.value + '|';
        }
      });

      var n = query.lastIndexOf("|");
      if (n > 0) {
        query = query.slice(0, n);
      }
    }
    var orderBy = (this.sortResults && this.sortResults != null && this.selectedSortOrder && this.selectedSortOrder != null) ? this.sortResults + "," + this.selectedSortOrder : "";
    if (orderBy != "") {
      query += '&orderBy=' + orderBy;
    }

    query += '&pageIndex=' + this.pageIndex
    query += '&pageSize=' + this.pageSize;
    if (this.maxvalue > 0) {
      query += '&max=' + this.maxvalue;
    }
    return query;
  }

  private validateUserInput() {
    var validationMsg = "";
    if (!this.selectedFields || this.selectedFields.length == 0) {
      validationMsg = this.globalResourceService.requiredValidator("metadata_field_query_selectedfield");
    }
    this.filterArray.forEach(element => {
      if (!element.name || !element.operator || !element.value) {
        validationMsg = this.getResourceValue("metadata_improper_query_message");
        return;
      }
    });
    return validationMsg;
  }
  public executeQuery() {
    var validationMessage = this.validateUserInput();
    if (validationMessage != "") {
      return this.toster.showWarning(validationMessage);
    }
    var query = this.buildQueryParam();
    this.queryService.getResult(this.entityName, query)
      .pipe(first())
      .subscribe(
        data => {
          this.results = data.result;
          this.generateColumns();
        },
        error => {
          console.log(error);
        });

  }

  buildQuery() {
    var validationMessage = this.validateUserInput();
    if (validationMessage != "") {
      return this.toster.showWarning(validationMessage);
    }


    var query = this.buildQueryParam();
    this.queryService.buildQuery(this.entityName, query)
      .pipe(first())
      .subscribe(
        data => {
          this.query = data.query;
        },
        error => {
          console.log(error);
        });


  }

  generateColumns() {
    var that = this;
    that.columns = [];

    if (this.results.length > 0) {
      let i = 0;
      Object.keys(this.results[0]).forEach(function (key, index) {
        i++;

        let columnObj = { field: '', title: '', width: 40, isVisible: true, position: 0 };
        columnObj.field = key;
        columnObj.title = this.getResourceValue(key);
        columnObj.position = i;
        that.columns.push(columnObj);
      });
    }

  }

  getResourceValue(key) {
    return this.globalResourceService.getResourceValueByKey(key);
  }


}
