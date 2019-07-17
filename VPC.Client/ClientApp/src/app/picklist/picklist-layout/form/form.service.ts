import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { HttpHeaders } from '@angular/common/http';
import { environment } from '../../../../environments/environment';
import { LayoutModel } from '../../../model/layoutmodel';

import { ListLayoutDetails } from '../../../model/listlayoutdetails';
import { OrderDetails } from '../../../model/orderdetails';

export class FormService{

    private layout: string = '/api/meta-data';
    private cacheLayoutDetail: LayoutModel;
    query: string = '?&pagingParameter.pageNumber=1' + '&pagingParameter.pageSize=10';
    constructor(private http: HttpClient) { }
  
    getLayouts(name): Observable<any> {
      var layoutUrl = `${environment.apiUrl}` + this.layout + '/' + name + '/layouts';
      return this.http.get<LayoutModel[]>(layoutUrl);
    }
  
    saveLayout(layoutModel, name): Observable<any> {
      var layoutUrl = `${environment.apiUrl}` + this.layout + '/' + name + '/layouts';
  
      var layout = new LayoutModel();
      layout.name = layoutModel.name;
      layout.layoutType = layoutModel.layoutType;
      //layout.Subtype = layoutModel.Subtype;
      layout.subtypeeName = layoutModel.SubtypeeName;
      layout.Context = layoutModel.Context;
      return this.http.post(layoutUrl, layout);
      //return this.http.post(layoutUrl, layout)
      //  .subscribe(
      //    data => {
      //      console.log("POST Request is successful ", data);
      //    },
      //    error => {
      //      console.log("Error", error);
      //    }
  
      //  );
    }
  
    saveLayoutDefault(layoutModel, name): Observable<any> {
      var layoutUrl = `${environment.apiUrl}` + this.layout + '/' + name + '/layouts';
  
      return this.http.patch(layoutUrl, layoutModel);
    }
  
  
    updateFormLayout(entityname: string, layoutId: string, layoutInfo: LayoutModel): any {
      var layoutUrl = `${environment.apiUrl}` + this.layout + '/' + entityname + '/layouts/' + layoutId;
      return this.http.post(layoutUrl, layoutInfo);
    }
    
    updateLayout(name, layoutId, layoutDetails): Observable<any> {
      var layoutUrl = `${environment.apiUrl}` + this.layout + '/' + name + '/layouts/' + layoutId;
  
      var layout1 = new LayoutModel();
      layout1.name = layoutDetails.name;
      layout1.layoutType = layoutDetails.layoutType;
      layout1.layoutTypeName = layoutDetails.layoutTypeName;
      //layout1.listLayoutDetails.Fields = [];
      layout1.listLayoutDetails = new ListLayoutDetails();
      layout1.listLayoutDetails.fields = [];
  
      if (layoutDetails != null && layoutDetails.layoutType == 3 && layoutDetails.listLayoutDetails) {
  
        if (layoutDetails.listLayoutDetails.fields != null) {
          var fields = [];
          for (var j = 0; j < layoutDetails.listLayoutDetails.fields.length; j++) {
            var myObj1 = {
              Name: layoutDetails.listLayoutDetails.fields[j].name,
              Sequence: layoutDetails.listLayoutDetails.fields[j].sequence,
              Hidden: layoutDetails.listLayoutDetails.fields[j].hidden,
              DataType: layoutDetails.listLayoutDetails.fields[j].dataType,
              refId: layoutDetails.listLayoutDetails.fields[j].refId,
              DefaultValue: layoutDetails.listLayoutDetails.fields[j].defaultValue,
              Properties: layoutDetails.listLayoutDetails.fields[j].properties,
              Values: layoutDetails.listLayoutDetails.fields[j].values 
            };
            fields.push(myObj1);
          }
          layout1.listLayoutDetails.fields = [];
          layout1.listLayoutDetails.fields = fields;
        }
  
        if (layoutDetails.listLayoutDetails.defaultSortOrder != null) {
          var defaultSortOrder = {
            name: "",
            value: "",
            groupBy: ""
          };
          if (layoutDetails.listLayoutDetails.defaultSortOrder.name) {
            defaultSortOrder.name = layoutDetails.listLayoutDetails.defaultSortOrder.name;
          }
          if (layoutDetails.listLayoutDetails.defaultSortOrder.value) {
            defaultSortOrder.value = layoutDetails.listLayoutDetails.defaultSortOrder.value;
          }
          if (layoutDetails.listLayoutDetails.defaultSortOrder.groupBy) {
            defaultSortOrder.groupBy = layoutDetails.listLayoutDetails.defaultSortOrder.groupBy;
          }
          //layout1.ListLayoutDetails.DefaultSortOrder = {};
          layout1.listLayoutDetails.defaultSortOrder = new OrderDetails();
          layout1.listLayoutDetails.defaultSortOrder = defaultSortOrder;
        }
  
        if (layoutDetails.listLayoutDetails.maxResult !== null) {
          layout1.listLayoutDetails.maxResult = layoutDetails.listLayoutDetails.maxResult;
        }
  
        if (layoutDetails.listLayoutDetails.toolbar !== null) {
          var buttonList = [];
          for (var k = 0; k < layoutDetails.listLayoutDetails.toolbar.length; k++) {
            var myObj12 = {
              Name: layoutDetails.listLayoutDetails.toolbar[k].name,
              //status: layoutDetails.listLayoutDetails.toolbar[k].status,
              Type: layoutDetails.listLayoutDetails.toolbar[k].type,
              Sequence: layoutDetails.listLayoutDetails.toolbar[k].sequence,
              Group: layoutDetails.listLayoutDetails.toolbar[k].group,
              Properties: layoutDetails.listLayoutDetails.toolbar[k].properties
            };
            buttonList.push(myObj12);
          }
          layout1.listLayoutDetails.toolbar = [];
          layout1.listLayoutDetails.toolbar = buttonList;
        }
  
        if (layoutDetails.listLayoutDetails.actions !== null) {
          var actionList = [];
          for (var x = 0; x < layoutDetails.listLayoutDetails.actions.length; x++) {
            var myObj11 = {
              name: layoutDetails.listLayoutDetails.actions[x].name,
              status: layoutDetails.listLayoutDetails.actions[x].status,
              sequence: layoutDetails.listLayoutDetails.actions[x].sequence
            };
            actionList.push(myObj11);
          }
          layout1.listLayoutDetails.actions = [];
          layout1.listLayoutDetails.actions = actionList;
        }
  
        if (layoutDetails.listLayoutDetails.searchProperties !== null) {
          layout1.listLayoutDetails.searchProperties = layoutDetails.listLayoutDetails.searchProperties;
        }
      }
  
      
  
      //return this.http.post(layoutUrl, JSON.stringify(layout1));
      return this.http.post(layoutUrl, layout1);
        //.subscribe(
        //  data => {
        //    console.log("POST Request is successful ", data);
        //  },
        //  error => {
        //    console.log("Error", error);
        //  }
  
        //);
    }
  
    getLayoutById(id): Observable<any> {
      var layoutById = `${environment.apiUrl}` + this.layout + '/layouts/' + id;
      return this.http.get<LayoutModel[]>(layoutById);
    }  

    
  
    displayPreview(name, fields): Observable<any> {
      var layoutUrl = `${environment.apiUrl}` + '/api/entities' + '/' + name + '?fields=' + fields;
      return this.http.get<any[]>(layoutUrl);
    }
  
    deleteLayout(id, name): Observable<any> {
      const httpOptions = {
        headers: new HttpHeaders({
          'Content-Type': 'application/json',
          'Access-Control-Allow-Origin': '*'
        })
      };
  
      var layoutUrl = `${environment.apiUrl}` + this.layout + '/' + name + '/layouts/' + id;
  
      return this.http.delete(layoutUrl);
    }
  
    getPickList(name): Observable<any> {
      var layoutUrl = `${environment.apiUrl}` + '/api/picklists' + '/' + name + '/values';
      return this.http.get<any[]>(layoutUrl);
    }
  }
  
  

  


