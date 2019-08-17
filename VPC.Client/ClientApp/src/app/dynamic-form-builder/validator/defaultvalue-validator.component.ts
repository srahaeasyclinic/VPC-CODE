import { Component, OnInit, Input } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { GlobalResourceService } from '../../global-resource/global-resource.service';
import { Resource } from '../../model/resource';
import { TreeService } from '../service/tree.service';
import { first } from 'rxjs/operators';
import * as _ from 'lodash';
import { Key } from 'protractor';
import { NativeDateAdapter, DateAdapter, MAT_DATE_FORMATS, MatDatepickerInputEvent } from "@angular/material";
import { AppDateAdapter, APP_DATE_FORMATS } from '../filter/date.adapter';
@Component({
    selector: 'defaultvalue-validator',
    template: `
    <div>
        <div *ngFor="let option of validator.options">
            <label>{{getResourceValue('metadata_label_'+formatKey(option.name))}}</label>        
           

            <div [ngSwitch]="option.controlType | lowercase">
            
                <div *ngSwitchCase="'textbox'"><input type="text" class="input-control" [(ngModel)]="option.value"></div>
                <div *ngSwitchCase="'checkbox'">
                
                
                
                <label class="control control--checkbox">
                  &nbsp;
                  <input type="checkbox" [(ngModel)]="option.value">
                  <span class="control__indicator"></span>
                </label>
                
                
                </div>
                
                <div *ngSwitchCase="'calendar'">  
                                              
                    <mat-form-field>
                    <input matInput [min]="minDate"  [matDatepicker]="picker" [(ngModel)]="validator.defaultValue">
                    <mat-datepicker-toggle matSuffix [for]="picker"></mat-datepicker-toggle>
                    <mat-datepicker #picker></mat-datepicker>
                    </mat-form-field>
                </div>
                
                <div *ngSwitchCase="'dropdown'">
                    
                    <select class="input-control" [(ngModel)]="validator.defaultValue">
                        <option *ngFor="let opt of results" [value]="opt.internalId">
                            <ng-container>
                                {{datatype=='Lookup'?opt.itemName:opt.text}}
                            </ng-container>
                        </option>
                    </select>

                </div>
                <div *ngSwitchCase="'multiselectdropdown'">  
                                              
                <select class="input-control" [(ngModel)]="validator.defaultValue">
                <option *ngFor="let opt of results" [value]="opt.internalId">
                    {{opt.text}}
                </option>
                </select>
                </div>
                <div *ngSwitchCase="'multiseleclookup'">  
                                              
                <select class="input-control" [(ngModel)]="validator.defaultValue">
                <option *ngFor="let opt of results" [value]="opt.internalId">
                    {{opt.text}}
                </option>
                </select>
                </div>
               
            </div>
        </div>
    </div>
  `, providers: [
        {
            provide: DateAdapter, useClass: AppDateAdapter
        },
        {
            provide: MAT_DATE_FORMATS, useValue: APP_DATE_FORMATS
        }
    ]
})
export class DefaultValidatorComponent implements OnInit {
    @Input() validator: any;
    @Input() datatype: string;
    @Input() typeof: string;
    public minDate = new Date(1800, 0, 1);
    public maxDate = new Date();
    public resource = Resource;
    public results: [any];
    constructor(public modal: NgbActiveModal, public globalResourceService: GlobalResourceService, private treeService: TreeService) {

    }

    ngOnInit(): void {
        console.log('validator', this.validator)
        //this.resource = this.globalResourceService.getGlobalResources();
        this.loadData("");
    }

    getResourceValue(key: string) {
        return this.globalResourceService.getResourceValueByKey(key);
    }


    loadData(query: string) {
        if (this.datatype && (this.datatype.toLowerCase() == "picklist")) {
            this.pickListValues(this.typeof, query);
        } else if (this.datatype && (this.datatype.toLowerCase() == "lookup")) {
            this.lookUpValues(this.typeof, query);
        }
    }


    lookUpValues(name: string, query: string) {
        this.treeService.getLookupValue(name, query)
            .pipe(first())
            .subscribe(
                data => {
                    if (data) {
                        this.results = data.result;
                        this.results = _.sortBy(data.result, 'itemName');
                    }
                },
                error => {
                    console.log(error);
                });
    }

    pickListValues(name: string, query: string) {
        this.treeService.getPickListValues(name, query)
            .pipe(first())
            .subscribe(
                data => {
                    if (data) {
                        this.results = data.result;
                        this.results = _.sortBy(data.result, 'text');
                    }
                },
                error => {
                    console.log(error);
                });
    }
    formatKey(key) {
        var spacereplace = key.replace(' ', '');
        var dotreplace = spacereplace.replace('.', '_');
        return dotreplace.toLowerCase();
    }

}


