import { Component, ViewChild, OnInit } from '@angular/core';
import { Observable, BehaviorSubject, from } from 'rxjs';
import { delay, switchMap, map, tap, first } from 'rxjs/operators';
import { MetadataService } from '../meta-data/metadata.service';
import { Entities } from '../model/entities';
import { counter } from '../model/counter'
import { CounterService } from './counter.service'
import { NgbModal, ModalDismissReasons, NgbModalOptions } from '@ng-bootstrap/ng-bootstrap';
import { TosterService } from 'src/app/services/toster.service';
import { PicklistService } from '../picklist/picklist.service'
import { ContainsFilterOperatorComponent, StringFilterCellComponent } from '@progress/kendo-angular-grid';
import { MenuService } from '../services/menu.service';
import { GlobalResourceService } from '../global-resource/global-resource.service';
import { Resource } from '../model/resource';

@Component({
  selector: 'app-counter-component',
  templateUrl: './counter.component.html',
  styleUrls: ['./counter.component.css']
})
export class CounterComponent implements OnInit {
  @ViewChild("list") list;
  public showHide: boolean = false;
  public defaultItem: { text: string, value: number } = { text: "Select item...", value: null };
  public counters: counter[] = [];
  public metaDataList: Entities[];
  public entities: Entities[];
  public counterObj: counter | null;
  public myDate: any;
  public schedules: Array<any> = [];
  public entity: Entities;
  public editUpdate: string = '';
  validationMsg = "";
  public ftext: string = '';
  public resource: Resource;

  constructor(
    private metadataService: MetadataService,
    private modalService: NgbModal,
    private counterService: CounterService,
    private tosterService: TosterService,
    private pickListService: PicklistService,
    public menuService: MenuService,
    private globalResourceService: GlobalResourceService,


  ) {
    //this.gridData = this.dataList.slice();
  }
  ngOnInit() {
    this.resource = this.globalResourceService.getGlobalResources();
    this.editUpdate = this.getResourceValue("metadata_operation_save");
    this.counterObj = new counter();
    this.getEntities();
  }
  //for kendo dropdownlist events===>
  ngAfterViewInit() {
    const contains = value => s => s.displayName.toLowerCase().indexOf(value.toLowerCase()) !== -1;
    this.list.filterChange.asObservable().pipe(
      switchMap(value => from([this.metaDataList]).pipe(
        tap(() => this.list.loading = true),
        delay(1000),
        map((data) => data.filter(contains(value)))
      ))
    )
      .subscribe(x => {
        this.metaDataList = x;
        this.list.loading = false;
      });
  }

  onKendoDropdownChange(event) {
    this.entity = event;
    this.getCounters(this.entity.name);
    this.metaDataList = this.entities;
  }
  onKendoDropdownSearchChange(event) {
    this.metaDataList = this.entities;
  }
  //=============================>

  //==for entity list==>
  private getEntities() {
    this.metadataService.getEntities("primaryentity")
      .pipe(first())
      .subscribe(
        data => {
          if (data && data) {
            this.entities = data;
            this.metaDataList = data;
            this.getScheduleList('counterschedule');
          }
        },
        error => {
          console.log(error);
        });
  }
  //========for schedule picklist==>
  private getScheduleList(pickListName: string): void {
    this.pickListService.getPickListValues(pickListName)
      .pipe(first())
      .subscribe(data => {
        if (data && data.result) {
          this.schedules = data.result;
          this.schedules.forEach(element => {
            element.internalId = parseInt(element.internalId)
          });
        }
      }
        , error => {
          console.log(error);
        });
  }
  // == for counter list==>


  private getCounters(entityName: string) {
    this.counterService.getCountersByEntity(entityName)
      .pipe(first())
      .subscribe(
        data => {
          if (data) {
            this.getCounterDetails(data)
          }
          else {
            this.editUpdate = 'Save';
            this.counterObj = new counter();
            this.counterObj.entityName = this.entity.name;
            this.ftext = '';
            this.validationMsg = '';
            //this.tosterService.showWarning('No counters found.');
          }
        },
        error => {
          console.log(error);
        });
  }

  //==for kendo grid events====>

  //getCunter for edit
  getCounterDetails(c: counter) {
    this.editUpdate = 'Update';
    this.counterObj = c;
    this.getDetails(c.text.split('\\'));

  }

  deleteCounter(id: string) {
    this.counterService.deleteCounter(id).subscribe(result => {
      if (result) {
        this.tosterService.showSuccess(this.getResourceValue("CounterDeletedSuccessfully"));
        this.getCounters(this.entity.name);
      }
    },
      error => {
        this.tosterService.showError(error.error.text);
      }
    );
  }
  //=======End Grid events======>



  //================for modal events====>
  saveOrUpdate() {

    let errorMessage: string = "";
    if (this.entity === null || this.entity === undefined) {
      errorMessage += this.globalResourceService.requiredValidator("counter_field_entity") + "<br/>";
    }

    if (this.counterObj.description === "" || this.counterObj.description === undefined) {
      errorMessage += this.globalResourceService.requiredValidator("counter_field_description") + "<br/>";
    }

    if (this.counterObj.text === "" || this.counterObj.text === undefined) {
      errorMessage += this.globalResourceService.requiredValidator("counter_field_format") + "<br/>";
    }

    if (this.counterObj.counterN === null || this.counterObj.counterN === undefined) {
      errorMessage += this.globalResourceService.requiredValidator("counter_field_counterN") + "<br/>";
    }
    if (this.counterObj.counterO === null || this.counterObj.counterO === undefined) {
      errorMessage += this.globalResourceService.requiredValidator("counter_field_counterO") + "<br/>";
    }

    if (this.counterObj.counterP === null || this.counterObj.counterP === undefined) {
      errorMessage += this.globalResourceService.requiredValidator("counter_field_counterP") + "<br/>";
    }

    if (this.counterObj.resetCounterN === null || this.counterObj.resetCounterN === undefined) {
      errorMessage += this.globalResourceService.requiredValidator("counter_field_resetN") + "<br/>";
    }
    if (this.counterObj.resetCounterO === null || this.counterObj.resetCounterO === undefined) {
      errorMessage += this.globalResourceService.requiredValidator("counter_field_resetO") + "<br/>";
    }

    if (this.counterObj.resetCounterP === null || this.counterObj.resetCounterP === undefined) {
      errorMessage += this.globalResourceService.requiredValidator("counter_field_resetP") + "<br/>";
    }



    if (errorMessage != "") {
      this.tosterService.showError(errorMessage);
      return;
    }

    if (this.validationMsg != '') {
      this.tosterService.showError(this.getResourceValue("counter_correctformat_warning_message"));
      return;
    }
    if (this.counterObj.counterId != undefined) {
      //for update
      this.updateCounter(this.counterObj);
    }
    else {
      //for Save;
      this.saveCounter(this.counterObj);
    }


  }

  //===========end modal events=========>



  saveCounter(counter: counter) {
    this.counterObj.text = this.counterObj.text.toUpperCase();
    this.counterService.saveCounter(counter)
      .pipe(first())
      .subscribe(
        data => {
          if (data) {
            var returnData = data;
            this.tosterService.showSuccess(this.globalResourceService.createSuccessMessage("counter_displayname"));
            this.getCounters(this.entity.name);
          }
        },
        error => {
          this.tosterService.showError(error.error.text);
        }
      );
  }

  updateCounter(counter: counter) {
    this.counterObj.text = this.counterObj.text.toUpperCase();
    this.counterService.updateCounter(counter)
      .pipe(first())
      .subscribe(
        data => {
          if (data) {
            var returnData = data;
            const modalRef = this.modalService.dismissAll();
            this.tosterService.showSuccess(this.globalResourceService.updateSuccessMessage("counter_displayname"));
            this.getCounters(this.entity.name);
          }
        },
        error => {
          this.tosterService.showError(error.error.text);
        }
      );
  }
  validate(textArry: string[]) {
    if (textArry.length > 1) {
      var validation = { isValid: true, message: "" }

      if (textArry.some(x => x == "")) {
        validation = { isValid: false, message: this.getResourceValue("counter_field_format_invalid_message") }
      }

      let substrings: string[] = ['y', 'm', 'd', 'w', 'q', 's', 'p', 'o', 'n', 'r']
      for (var s of textArry) 
      {
        if (s.length > 0 && Number(s.substr(1)) >= 100) {
          if (substrings.some(function (v) { return s[0].toUpperCase().indexOf(v.toUpperCase()) >= 0; })) {
            validation = { isValid: false, message: this.getResourceValue("counter_field_format_invalid_message") }
            break;
          }
        }
        else if (s.length > 0 && s[s.length - 1] == '-' && Number(s.substr(1, s.length - 2)) >= 100) {
          if (substrings.some(function (v) { return s[0].toUpperCase().indexOf(v.toUpperCase()) >= 0; })) {
            validation = { isValid: false, message: this.getResourceValue("counter_field_format_invalid_message") }
            break;
          }
        }

        else if (s.length > 0 && s[0] == '-' && Number(s.substr(2, s.length)) >= 100) {
          if (substrings.some(function (v) { return s[1].toUpperCase().indexOf(v.toUpperCase()) >= 0; })) {
            validation = { isValid: false, message: this.getResourceValue("counter_field_format_invalid_message") }
            break;
          }
        }
        else if (s.length > 0 && s[0] == '-' && s[s.length - 1] == '-' && Number(s.substr(2, s.length - 3)) >= 100) {
          if (substrings.some(function (v) { return s[1].toUpperCase().indexOf(v.toUpperCase()) >= 0; })) {
            validation = { isValid: false, message: this.getResourceValue("counter_field_format_invalid_message") }
            break;
          }
        }
      }
      return validation;
    }
    else {
      return validation = { isValid: false, message: this.getResourceValue("counter_field_format_invalid_message") }
    }
  }

  getFormatedText() {
    this.validationMsg = '';
    this.ftext = '';
    if (this.counterObj.text != '') {
      var textArry = (this.counterObj.text).trim().split('\\');
      if (textArry.length > 0) {
        if (this.validate(textArry).isValid) {
          this.getDetails(textArry);
        }
        this.validationMsg = this.validate(textArry).message;
      }
    }
  }


  getDetails(array: string[]) {
    this.validationMsg = '';
    this.ftext = '';

    for (var s of array) {
      if (s.length > 0) {
        if (Number(s.substr(1))) {
          this.ftext += (this.getValue(s[0].toLowerCase(), Number(s.substr(1).trim().toUpperCase())));
        }
        else if (s[s.length - 1] == '-' && Number(s.substr(1, s.length - 2))) {
          this.ftext += (this.getValue(s[0].toLowerCase(), Number(s.substr(1, s.length - 2).trim().toUpperCase())));
          this.ftext += '-'
        }
        else if (s[0] == '-' && Number(s.substr(2, s.length))) {
          this.ftext += '-'
          this.ftext += (this.getValue(s[1].toLowerCase(), Number(s.substr(2, s.length).trim().toUpperCase())));
        }
        else if (s[0] == '-' && s[s.length - 1] == '-' && Number(s.substr(2, s.length - 3))) {
          this.ftext += '-'
          this.ftext += (this.getValue(s[1].toLowerCase(), Number(s.substr(2, s.length - 3).trim().toUpperCase())));
          this.ftext += '-'
        }
        else {
          this.ftext += s.toUpperCase();
        }
      }
    }
  }
  keyPress(event: any) {
    if (event.charCode >= 48 && event.charCode <= 57) {

    } else {
      event.preventDefault();
    }
  }
  getValue(char: string, padd: number): string {
    var result: string = '';
    switch (char) {
      case "y":
        const year = (new Date().getFullYear()).toString();
        result = (year.substring(year.length - padd, year.length).padStart(padd, '0'));
        break;
      case "m":
        const month = (new Date().getMonth()).toString();
        result = (month.substring(month.length - padd, month.length).padStart(padd, '0'));
        break;
      case "d":
        const day = (new Date().getDay()).toString();
        result = (day.substring(day.length - padd, day.length).padStart(padd, '0'));
        break;
      case "w":
        const week = this.getNumberOfWeek().toString();
        result = (week.substring(week.length - padd, week.length).padStart(padd, '0'));
        break;
      case "q":
        var counterQ = this.counterObj.counterN != null ? this.counterObj.counterN : 0;
        result = (counterQ.toString().padStart(padd, '0'));
        break;
      case "s":
        var counterS = this.counterObj.counterP != null ? this.counterObj.counterP : 0;
        result = (counterS.toString().padStart(padd, '0'));
        break;
      case "p":
        var counterP = this.counterObj.counterP != null ? this.counterObj.counterP : 0;
        result = (counterP.toString().padStart(padd, '0'));
        break;
      case "o":
        var counterO = this.counterObj.counterO != null ? this.counterObj.counterO : 0;
        result = (counterO.toString().padStart(padd, '0'));
        break;
      case "n":
        var counterN = this.counterObj.counterN != null ? this.counterObj.counterN : 0;
        result = (counterN.toString().padStart(padd, '0'));
        break;
      case "r":
        var counterR = this.counterObj.counterO != null ? this.counterObj.counterO : 0;
        result = (counterR.toString().padStart(padd, '0'));
        break;
      default:
        result = char + padd.toString();
    }
    return result;
  }
  getNumberOfWeek() {
    const today: any = new Date();
    const firstDayOfYear: any = new Date(today.getFullYear(), 0, 1);
    const pastDaysOfYear = (today - firstDayOfYear) / 86400000;
    return Math.ceil((pastDaysOfYear + firstDayOfYear.getDay() + 1) / 7);
  }
  convertValue(val: number): string {
    return val.toString();
  }
  generateResourceName(word) {
    if (!word) return word;
    return word[0].toLowerCase() + word.substr(1);
  }

  getResourceValue(key) {
    return this.globalResourceService.getResourceValueByKey(key);
  }

}
