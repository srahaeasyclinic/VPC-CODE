
import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { LayoutService } from '../layout.service';
import { LayoutModel } from '../../../model/layoutmodel';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { NgbModal, NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';


import { MetadataService } from '../../metadata.service';
import { first } from 'rxjs/operators';
import { TosterService } from '../../../services/toster.service';
import { Resource } from 'src/app/model/resource';
import { GlobalResourceService } from 'src/app/global-resource/global-resource.service';

@Component({
  selector: 'app-modal-layout',
  templateUrl: './modal-layout.component.html',
  styleUrls: ['./modal-layout.component.css']
})


export class ModalLayoutComponent implements OnInit {
  private layoutModel = new LayoutModel();
  public subTypes = new Array<SubType>();
  public subtypename: string;
  private id: number = 0;
  public resource: Resource;
  @Input() layout: any;
  @Input() metaDataName: string;
  @Input() title = 'Information';

  @Output() saveEvent: EventEmitter<any> = new EventEmitter();

  myForm: FormGroup;
  submitted = false;
  public showSubType: boolean = false;

  layoutTypes = [
    new LayoutType(1, 'View'),
    new LayoutType(2, 'Form'),
    new LayoutType(3, 'List')
  ];

  contexts = [
    new Context(1, 'New'),
    new Context(2, 'Edit')
  ];

  constructor(
    private route: ActivatedRoute,
    public activeModal: NgbActiveModal,
    private formBuilder: FormBuilder,
    private layoutService: LayoutService,
    private modalService: NgbModal,
    private metadataService: MetadataService,
    private toster: TosterService,
    private router: Router,
    public globalResourceService: GlobalResourceService
  ) {

  }

  ngOnInit() {
    this.getMetadataFieldsByName(this.metaDataName);
    this.myForm = this.formBuilder.group({
      layoutName: ['', Validators.required],
      drpType: ['', Validators.required]
      // drpSubtype:['', Validators.required],
      // drpContext:['', Validators.required]     
    });
    this.resource = this.globalResourceService.getGlobalResources();
  }

  // private createForm() {
  //   this.myForm = this.formBuilder.group({
  //     layoutName: '',
  //     drpType: '',
  //     drpSubType: '',
  //     drpContext: ''
  //   });
  // }

  // convenience getter for easy access to form fields
  get f() { return this.myForm.controls; }





  private getMetadataFieldsByName(name) {
    this.metadataService.getMetadataByName(name)
      .pipe(first())
      .subscribe(
        data => {
          //console.log("data", data);
          if (data && data.subtypes) {
            for (var k = 0; k < data.subtypes.length; k++) {
              this.subTypes.push(new SubType(k, data.subtypes[k]));
            }
          }
        },
        error => {
          console.log(error);
        });
  }

  private getSubTypeName(id) {
    for (var k = 0; k < this.subTypes.length; k++) {
      if (this.subTypes[k].id === id) {
        return this.subtypename = this.subTypes[k].name;
      }
    }
  }
  getResourceValue(key) {
    return this.globalResourceService.getResourceValueByKey(key);
  }
  public submitForm() {
    this.submitted = true;

    if (this.myForm.invalid) {
      return;
    }


    this.layoutModel.name = this.myForm.value.layoutName;
    this.layoutModel.layoutType = this.myForm.value.drpType;
    this.layoutModel.subtypeeName = (this.myForm.value.drpSubtype == '') ? '' : this.getSubTypeName(parseInt(this.myForm.value.drpSubtype));
    this.layoutModel.Context = (this.myForm.value.drpContext == '') ? 0 : this.myForm.value.drpContext;

    this.layoutService.saveLayout(this.layoutModel, this.layout.name).subscribe(result => {
      this.toster.showSuccess(this.getResourceValue("LayoutSavedSuccessfully"));
      this.activeModal.close();
      var myObj = {
        id:result,
        layoutType:this.layoutModel.layoutType
      }
      this.saveEvent.emit(myObj);
      //console.log(this.route);
      // if (Number(this.layoutModel.layoutType) === 1) {
      //   this.router.navigate(["./view"+"/"+this.id], { relativeTo: this.route });
      //   //this.router.navigate(["metadata/" + this.metaDataName + "/layout/" + this.id + "/View" ]);
      // }
      // else if (Number(this.layoutModel.layoutType) === 2) {
      //   this.router.navigate(["./form"+"/"+this.id], { relativeTo: this.route });
      //   // this.router.navigate(["metadata/" + this.metaDataName + "/layout/" + this.id + "/Form" ]);
      // }
      // else if (Number(this.layoutModel.layoutType) === 3) {
      //   this.router.navigate(["./list"+"/"+this.id], { relativeTo: this.route });
      //   //this.router.navigate(["metadata/" + this.metaDataName + "/layout/" + this.id + "/List" ]);
      // }
      // else {
      //   this.layout.loadLayout(this.layout.name);
      // }
    });


  }



  onTypeChange(value) {
    console.log(value);
    if (value == "2") {
      this.showSubType = true;
      this.formTypeValidation();
    } else {
      this.showSubType = false;
    }
  }

  private formTypeValidation() {
    //   this.myForm = this.formBuilder.group({
    //     layoutName: ['', Validators.required],
    //     drpType: ['', Validators.required],
    //     drpSubtype:['', Validators.required],
    //     drpContext:['', Validators.required]     
    // });
    this.myForm.addControl('drpSubtype', new FormControl('', Validators.required));
    this.myForm.addControl('drpContext', new FormControl('', Validators.required));
  }
  // generateResourceName(word) {
  //   if (!word) return word;
  //   return word[0].toLowerCase() + word.substr(1);
  // }

}



export class LayoutType {
  constructor(public id: number, public name: string) { }
}

//export class SubType {
//  constructor(public id: number, public name: string) { }
//}

export class SubType {
  constructor(public id: number, public name: string) { }
}

export class Context {
  constructor(public id: number, public name: string) { }
}
