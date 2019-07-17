import { Component, OnInit, Input, ViewChild, Output, EventEmitter } from '@angular/core';
import { FormGroup } from '@angular/forms';
// import { FormsModule } from '@angular/forms';
// import { NgModule } from '@angular/core';
// import { BrowserModule } from '@angular/platform-browser';
//import { ValidationService } from 'src/app/services/validation.service';
import { AngularEditorConfig } from '@kolkov/angular-editor';
import { NgbModal, ModalDismissReasons, NgbModalOptions } from '@ng-bootstrap/ng-bootstrap';
import { SelectableSettings } from '@progress/kendo-angular-grid';
import { CommunicationService, Tags } from '../../../services/communication.service';
import { first } from 'rxjs/operators';
import { Broadcaster } from '../../messaging/broadcaster';
import { DomSanitizer } from '@angular/platform-browser';

//import { HierarchyDropdownService } from '../../../services/hierarchy-dropdown.service';
import { GlobalResourceService } from '../../../global-resource/global-resource.service';
import { Resource } from '../../../model/resource';


@Component({
  selector: 'richtextbox',
  templateUrl: './richtextbox.component.html',
  styleUrls: ['./richtextbox.component.css']
})
export class RichtextboxComponent implements OnInit {

  @Input() field: any = {};
  @Input() form: FormGroup;
  @Input() mode: number;
  @Input() ruleinfo: any = {};
  @Output() changeEmitter = new EventEmitter();
  
  // @Input() SelectedMetadata: string;
  get isValid() { return this.form.controls[this.field.name].valid; }
  get isDirty() { return this.form.controls[this.field.name].dirty; }
  private Isvalid: boolean;
  private modalReference: any;
  private addTagsLabel: string;
  public TagsItemName: Array<Tags>;
  public mySelection: number[] = [];
  public selectedtagsValues: any = [];
  // private Dropdowndata: any = [];
  model: any = {};
  editorConfig: any;
  @ViewChild("richeditor") richeditor: any;
 
  public entityName: string;


  constructor(
    private modalService: NgbModal,
    public broadcaster: Broadcaster, private communicationservice: CommunicationService,
    private sanitizer: DomSanitizer,
    private globalResourceService: GlobalResourceService,
    // private dropdownservice:HierarchyDropdownService
  ) {
    this.field.value = this.field.value == undefined ? '' : this.field.value;
     
  }
  sanatizeHtml(data) {

    return this.sanitizer.bypassSecurityTrustHtml(data);
  }
  ngOnInit() {
    
    // this.Dropdowndata = this.dropdownservice.dropdowndatagrouping(this.arr,"parentPicklist");
    // console.log('drop->' + JSON.stringify(this.Dropdowndata));
    
    this.editorConfig = {
      allowedContent: false,
      extraPlugins: 'divarea',
      forcePasteAsPlainText: true,
      
    };

    //------------------------------------------------------------------------
    this.broadcaster.dependencyRules$.subscribe(
      t => {
       // console.log('t.method '+JSON.stringify(t.method));
       // console.log('t.data '+JSON.stringify(t.data));
        this.preparedTagableData(t.method, t.data);
      });
    //------------------------------------------------------------------------
   if (this.entityName == undefined) {

      let entityname:string = localStorage.getItem('tagsname');
      this.entityName = entityname;
      //console.log('entity '+entityname);
     if (this.entityName!=undefined&&this.entityName!="")
     {
        this.Inittags(this.entityName);
       }
     
    }
     
   
  }
  
  preparedTagableData(method: string, data: string) {
    //console.log("this.messages", data);
    if ((method != null && this.field.receivingTypes)) {
      this.field.receivingTypes.forEach(element => {
        if (method.toLowerCase() == element.toLowerCase()) {
          console.log("preparedTagableData", data);
          this.entityName = data;
          this.Inittags(data);
        }
      });
    }
  }

  private addTagsPopup(managetags): void {
    

    this.selectedtagsValues = [];
    this.addTagsLabel = this.getResourceValue("Addtags");
    let ngbModalOptions: NgbModalOptions = {
      backdrop: 'static',
      keyboard: false
    };
    this.modalReference = this.modalService.open(managetags, ngbModalOptions);
  }
  saveTags(): void {
    //console.log(this.mySelection);
    // console.log(JSON.stringify(this.selectedtagsValues));
    this.mySelection.forEach(v => {
      this.field.value = '[' + v + '] ' + this.field.value
    });
    this.mySelection = [];
    this.modalReference.close();
  }

  resetValue() {
    this.model.TagsItemName = [];
    this.mySelection = [];
    //console.log(JSON.stringify(this.selectedtagsValues));
  }

  Inittags(entityname) {
    // console.log('Inittags() ' + this.entityName);
    this.communicationservice.GetTagsByEntityname(entityname).pipe(first()).subscribe(data => {
     // console.log(JSON.stringify(data));
      if (data) {
        //console.log('Inittags- '+JSON.stringify(data));
        this.Filtertagabledata(data);
      }
    }, error => {
      this.TagsItemName = [];
    });
  }

  Filtertagabledata(data: any) {
    this.TagsItemName = [];
    // console.log('Filtertagabledata(data:any) '+JSON.stringify(data.fields));
    if (data.fields) {
      let filterdata = data.fields.filter(w => w.isTagable === true);
      filterdata.forEach(dta => {
        let tg = new Tags();
        tg.name = dta.name
        this.TagsItemName.push(tg);
      })
      this.model.TagsItemName = this.TagsItemName;
    }
    //console.log(this.entityName);
     //console.log('Filtertagabledata-this.TagsItemName ' + JSON.stringify(this.TagsItemName));
     //console.log('Filtertagabledata-this.this.model.TagsItemName ' + JSON.stringify(this.model.TagsItemName));
  }
  private onChangeEvent = ($event: any) => {
    this.changeEmitter.emit(this.field);
  
  }
  getResourceValue(key: string) {
    return this.globalResourceService.getResourceValueByKey(key);
  }
}
