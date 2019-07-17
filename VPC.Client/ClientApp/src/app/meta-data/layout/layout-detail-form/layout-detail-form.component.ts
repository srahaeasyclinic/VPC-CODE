import { Component, OnInit, NgZone, ChangeDetectorRef, ChangeDetectionStrategy } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
//import { TreeNode } from "../../../dynamic-form-builder/tree.module";
import { ITreeNode, getTreenodeinstanceWithObject, generateRandomNo } from '../../../model/treeNode';

import { fromEvent } from 'rxjs';
import { findIndex, ignoreElements } from 'rxjs/operators';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { LayoutService } from '../layout.service';
import { first } from 'rxjs/operators';
import { LayoutModel } from '../../../model/layoutmodel';
import { CommonService } from "../../../services/common.service";
import { Observable } from 'rxjs/Observable';
import { Entities } from '../../../model/entities';
//import { FieldModel } from '../../../model/fieldmodel';
import { predifinedType } from 'src/app/dynamic-form-builder/helper/utils';
import { TosterService } from 'src/app/services/toster.service';

import { MODALS } from 'src/app/dynamic-form-builder/tree.config'
import swal from 'sweetalert2';
import { NgbModal, NgbModalOptions } from '@ng-bootstrap/ng-bootstrap';

import { Resource } from '../../../model/resource';
import { HostListener } from "@angular/core";
import { Tasks } from 'src/app/model/tasks';
import { jsonpCallbackContext } from '@angular/common/http/src/module';
import { ConvertActionBindingResult } from '@angular/compiler/src/compiler_util/expression_converter';


import { Target } from "../../../model/target";
import { MetadataService } from '../../metadata.service';
import { Operation } from '../../../model/operation';
import { DebugRenderer2 } from '@angular/core/src/view/services';
import { GlobalResourceService } from 'src/app/global-resource/global-resource.service';
import { FieldModel } from '../../../model/fieldmodel';


@Component({
	selector: 'app-layout-detail-form',
	templateUrl: './layout-detail-form.component.html',
	styleUrls: ['./layout-detail-form.component.css'],
	//changeDetection: ChangeDetectionStrategy.OnPush
})


export class LayoutDetailFormComponent implements OnInit {
	public tree: ITreeNode;
	private metadatafield: any;
	public selectedTreeNode: ITreeNode | null;
	private dropType: string = "section";
	public layoutInfo: LayoutModel = new LayoutModel();
	private defaultView: LayoutModel = new LayoutModel();
	public layoutId: string;
	private layoutName: string = '';
	public entityDeatils: Entities;
	private selectedField: FieldModel;
	public predefinedData: any[] = predifinedType();
	private dragStart: boolean = false;
	modalReference: any;
	public resource: Resource;
	public isConfigToggle: boolean = false;
	public screenHeight: number;
	public detailEntities: any;
	public TaskOpretaion: any[];

	public displayRule: any;

	public toolbarSource: any;
	private selectedItemFromMainList: any;
	public addedItemToMainList: any;
	private selectedItemFromAddedList: any;
	private isdatamainlist: boolean = false;
	public displayToolbarItem: boolean = false;
	private selectedTask: string;
	private toolbarGroup: string;
	private isRendered: boolean = false;
	public isSticky: boolean = false;
	private position: number = 0;

	private swapComplete: boolean = false;
	private InstallTrigger: any;
	private isFirefox = typeof this.InstallTrigger !== 'undefined';
	private isChrome = !!window['chrome'] && (!!window['chrome']['webstore'] || !!window['chrome']['runtime']);
	public showLoader = false;
	//
	// public layoutContext: string = "";
	// public subTypeName: string = "";

	// I initialize the app component.
	constructor(
		private router: Router,
		private activatedRoute: ActivatedRoute,
		private layoutService: LayoutService,
		private commonService: CommonService,
		private metadataService: MetadataService,
		private zone: NgZone,
		private cdr: ChangeDetectorRef,
		private modalService: NgbModal,
		private toster: TosterService,
		public globalResourceService: GlobalResourceService
	) {

		this.getScreenSize();
	}

	@HostListener('window:resize', ['$event'])
	getScreenSize(event?) {
		this.screenHeight = window.innerHeight;
	}

	private getScrollPosition() {
		if (document.getElementById('page-content-wrapper')) {
			var element = document.getElementById('content-block-wrapper')
			if (element) {
				var fromabove = document.getElementById('page-content-wrapper').scrollTop;
				// if (fromabove > 80) {
				if (fromabove > this.position) {

					element.classList.add('sticky')
				} else {
					element.classList.remove('sticky')
				}
			}
		}
	}

	ngDoCheck() {
		if (document.getElementById('page-content-wrapper') && document.getElementById('content-block-wrapper')) {
			if (!this.isRendered) {
				this.position = document.getElementById('content-block-wrapper').scrollTop
				let tracker = document.getElementById('page-content-wrapper');
				let windowYOffsetObservable = Observable.fromEvent(tracker, 'scroll').map(() => {
				});
				let scrollSubscription = windowYOffsetObservable.subscribe((scrollPos) => {
					this.getScrollPosition()
				});
				this.isRendered = true;
			}
		}
	}

	entityname: string;
	ngOnInit() {
		//this.getRuleList();
		this.selectedTreeNode = null;
		this.addedItemToMainList = [];
		this.toolbarSource = [];
		
		this.getResource();

	}


	private init() {
		this.layoutInfo = this.activatedRoute.snapshot.data['layoutDetails'];

		this.activatedRoute.params.subscribe((params: Params) => {
			this.layoutId = params['id'];
		});

		this.activatedRoute.parent.parent.params.subscribe((params: Params) => {
			this.entityname = params['entityName'];
		});


		this.getMetadataFieldsByName(this.entityname);

	}

	private addMandatoryFieldsAndActivityEntity(item, entityName) {
		var myObj =getTreenodeinstanceWithObject({
			name: item.name,
			value: "",
			required: item.required, //need to change
			dataType: item.dataType,
			fields: [],
			controlType: item.controlType,
			decimalPrecision: null,
			defaultValue: item.defaultValue,
			properties: "",
			tabs: [],
			setting: { columnWidth: 12, showHeader: true },
			validators: item.validators,
			selectedView: "",
			typeOf: item.typeOf,
			receivingTypes: item.receivingTypes,
			receiverDataTypes: item.receiverDataTypes,
			broadcastingTypes: item.broadcastingTypes,
			refId:'',
			readOnly: item.readOnly,
			accessibleLayoutTypes: item.accessibleLayoutTypes,
			toolbar: item.toolbar,
			entityName: entityName
		})

		this.tree.fields.push(myObj);
	}

	private initAfterDataFieldByName() {
		//console.log('this.layoutInfo '+JSON.stringify(this.layoutInfo)+'this.layoutInfo.formLayoutDetails '+JSON.stringify(this.layoutInfo.formLayoutDetails));
		if (this.layoutInfo != null && this.layoutInfo.formLayoutDetails != null) {
			this.tree = this.layoutInfo.formLayoutDetails;
			// this.layoutContext = this.layoutInfo.contextName;
			// this.subTypeName = this.layoutInfo.subtypeeName;
		} else { 
			//console.log('add access ');
			//FormLayoutDetails
			this.tree = getTreenodeinstanceWithObject({
				required: true,
				controlType: "section",
				readOnly: false,
				setting: { columnWidth: 12, showHeader: true }
			});

			if (this.metadatafield != null) {

				if (this.metadatafield.fields != undefined) {
					this.metadatafield.fields.forEach(item => {
						if (item.controlType.toLocaleLowerCase() !== 'label' && item.required == true && (item.accessibleLayoutTypes === undefined || item.accessibleLayoutTypes.find(x => x === 2))) {
							this.addMandatoryFieldsAndActivityEntity(item, this.metadatafield.name)
						}
					});
				}

				if (this.metadatafield.activityEntity != undefined) {
					this.metadatafield.activityEntity.fields.forEach(item => {
						if (item.controlType.toLocaleLowerCase() !== 'label' && item.required == true && (item.accessibleLayoutTypes === undefined || item.accessibleLayoutTypes.find(x => x === 2))) {
							this.addMandatoryFieldsAndActivityEntity(item, this.metadatafield.activityEntity.name)
						}
					});
				}
			}

		}
		this.setactivityOnMetadataFields();
		//console.log('beforeSetId '+JSON.stringify(this.tree));
		this.setidExistingfields(this.tree);

		if (this.tree.toolbar !== null && this.tree.toolbar !== undefined && this.tree.toolbar.length > 0) {
			this.addedItemToMainList = this.tree.toolbar;
		}

		if (this.addedItemToMainList !== null && this.addedItemToMainList !== undefined) {
			this.addedItemToMainList.forEach(item => {
				item.isRowSelected = '';
			});

			if (this.addedItemToMainList && this.addedItemToMainList.length > 0) {
				this.addedItemToMainList[0].isRowSelected = true;
				this.processSelectedField(this.addedItemToMainList[0]);
			}
		}

		this.setItemIndex();
	}


	public configToggle(): void {
		this.isConfigToggle = !this.isConfigToggle;
	}

	private getResource(): void {
		this.resource = this.globalResourceService.getGlobalResources();
		this.init();

	}
	getResourceValue(key) {
		return this.globalResourceService.getResourceValueByKey(key);
	  }
	

	public updateLayout() {

		if (this.addedItemToMainList && this.addedItemToMainList.length > 0) {
			this.tree.toolbar = this.addedItemToMainList;
		}
		this.layoutInfo.formLayoutDetails = this.tree;

		// console.log('Tree update ' + JSON.stringify(this.tree));

		// console.log('this.layoutInfo ' + JSON.stringify(this.layoutInfo));

		this.layoutService.updateFormLayout(this.entityname, this.layoutId, this.layoutInfo).subscribe(result => {
			//console.log(result);
			this.toster.showSuccess(this.getResourceValue("LayoutSavedSuccessfully"));
		});
		//console.log(' this.tree ', this.tree);
	}
	// private getMetadataFieldsByName(name) {
	// 	this.metadataService.getMetadataByName(name)
	// 		.pipe(first())
	// 		.subscribe(
	// 			data => {
	// 				if (data) {
	// 					console.log("calling getmedatdata files by name");
	// 					this.entityDeatils = data;
	// 					this.entityDeatils.fields = this.entityDeatils.fields.filter(f => f.accessibleLayoutTypes === undefined || f.accessibleLayoutTypes.find(e => e === 2));

	// 					if (data.tasks) {
	// 						for (var k = 0; k < data.tasks.length; k++) {
	// 							this.toolbarSource.push(data.tasks[k]);
	// 						}
	// 					}

	// 					//for left side gray
	// 					for (var i = 0; i < this.addedItemToMainList.length; i++) {
	// 						for (var j = 0; j < this.toolbarSource.length; j++) {
	// 							if (this.addedItemToMainList[i].name === this.toolbarSource[j].name) {
	// 								this.toolbarSource[j].isRowSelected = true;
	// 								this.toolbarSource[j].isAdded = true;
	// 							}
	// 						}
	// 					}

	// 					// console.log(data.fields);
	// 					// data.fields.forEach((item, index) => {	
	// 					// 	//console.log('access '+ JSON.stringify(item));
	// 					// 	//&&(item.accessibleLayoutTypes===null ||item.accessibleLayoutTypes.find(x => x.accessibleLayoutTypes ==2))
	// 					// 	if (item.controlType.toLocaleLowerCase()!=='label'&&item.required == true && (item.accessibleLayoutTypes === undefined || item.accessibleLayoutTypes.find(x => x === 2))) {
	// 					// 		//console.log('access '+ JSON.stringify(item));
	// 					// 		var myObj = {
	// 					// 			name: item.name,
	// 					// 			value: "",
	// 					// 			required: item.required, //need to change
	// 					// 			dataType: item.dataType,
	// 					// 			fields: [],
	// 					// 			controlType: item.controlType,
	// 					// 			decimalPrecision: null,
	// 					// 			defaultValue: item.defaultValue,
	// 					// 			properties: "",
	// 					// 			tabs:[],
	// 					// 			setting:{columnWidth:12, showHeader:true},
	// 					// 			validators:item.validators,
	// 					// 			selectedView:"",
	// 					// 			typeOf: item.typeOf,
	// 					// 			receivingTypes: item.receivingTypes,
	// 					//             receiverDataTypes: item.receiverDataTypes,
	// 					//             broadcastingTypes: item.broadcastingTypes,
	// 					// 			RefId: this.generatRandomno(),
	// 					// 			readOnly:item.readOnly,
	// 					// 			accessibleLayoutTypes:item.accessibleLayoutTypes,
	// 					// 		}
	// 					data.fields.forEach((item, index) => {
	// 						//console.log('access '+ JSON.stringify(item));
	// 						//&&(item.accessibleLayoutTypes===null ||item.accessibleLayoutTypes.find(x => x.accessibleLayoutTypes ==2))
	// 						if (item.controlType.toLocaleLowerCase() !== 'label' && item.required == true && (item.accessibleLayoutTypes === undefined || item.accessibleLayoutTypes.find(x => x === 2))) {
	// 							//console.log('access '+ JSON.stringify(item));
	// 							var myObj = {
	// 								name: item.name,
	// 								value: "",
	// 								required: item.required, //need to change
	// 								dataType: item.dataType,
	// 								fields: [],
	// 								controlType: item.controlType,
	// 								decimalPrecision: null,
	// 								defaultValue: "",
	// 								properties: "",
	// 								tabs: [],
	// 								setting: { columnWidth: 12, showHeader: true },
	// 								validators: item.validators,
	// 								selectedView: "",
	// 								typeOf: item.typeOf,
	// 								receivingTypes: item.receivingTypes,
	// 								receiverDataTypes: item.receiverDataTypes,
	// 								broadcastingTypes: item.broadcastingTypes,
	// 								RefId: this.generatRandomno(),
	// 								readOnly: item.readOnly,
	// 								accessibleLayoutTypes: item.accessibleLayoutTypes,
	// 								toolbar: [],
	// 							}

	// 							this.tree.fields.push(myObj);
	// 						}
	// 					});

	// 					this.tree.fields.forEach(item => {

	// 						// this.entityDeatils.fields.forEach(obj => {
	// 						// 	if (item.name === obj.name) {
	// 						// 			//console.log("entityDeatils by name", JSON.stringify(obj));
	// 						// 		obj.draggedItem = true;
	// 						// 	}
	// 						// });
	// 						// if (item.controlType.toLocaleLowerCase()==='tab'||item.controlType.toLocaleLowerCase()==='tabs'||item.controlType.toLocaleLowerCase()==='section')
	// 						// {
	// 						this.setdeactivefields(item);
	// 						//}
	// 					});

	// 				}
	// 			},
	// 			error => {
	// 				console.log(error);
	// 			});
	// 	//console.log('treenode'+JSON.stringify(this.tree.fields));
	// }
	// });
	// 	}

	private getMetadataFieldsByName(name) {
		this.metadataService.getMetadataByName(name)
			.pipe(first())
			.subscribe(
				data => {
					this.metadatafield = data;
					this.initAfterDataFieldByName();
					//console.log('getMetadataFieldsByName metadatafield '+ JSON.stringify(this.metadatafield));
				},
				error => {
					console.log(error);
				});
		//console.log('treenode'+JSON.stringify(this.tree.fields));
	}

	private setactivityOnMetadataFields() {

		if (this.metadatafield != null || this.metadatafield != undefined) {
			//console.log("calling getmedatdata files by name");
			//console.log('getMetadataFieldsByName metadatafield '+ JSON.stringify(this.metadatafield));
			if (this.metadatafield.operations) {
				for (var k = 0; k < this.metadatafield.operations.length; k++) {
					this.toolbarSource.push(this.metadatafield.operations[k]);
				}
			}

			if (this.metadatafield.tasks) {
				for (var k = 0; k < this.metadatafield.tasks.length; k++) {
					this.toolbarSource.push(this.metadatafield.tasks[k]);
				}
			}

			//for left side gray

			if (this.addedItemToMainList !== null && this.addedItemToMainList !== undefined) {
				for (var i = 0; i < this.addedItemToMainList.length; i++) {
					for (var j = 0; j < this.toolbarSource.length; j++) {
						if (this.addedItemToMainList[i].name === this.toolbarSource[j].name) {
							this.toolbarSource[j].isRowSelected = true;
							this.toolbarSource[j].isAdded = true;
						}
					}
				}
			}

			this.entityDeatils = this.metadatafield;
			this.entityDeatils.fields = this.metadatafield.fields.filter(f => (f.accessibleLayoutTypes && f.accessibleLayoutTypes.find(e => e === 2)));

			if (this.metadatafield.activityEntity && this.metadatafield.activityEntity.fields) {
				this.entityDeatils.activityEntity.fields = this.metadatafield.activityEntity.fields.filter(f => (f.accessibleLayoutTypes && f.accessibleLayoutTypes.find(e => e === 2)));
			}

			this.tree.fields.forEach(item => {
				this.setdeactivefields(item);
			});


		}

	}

	public fieldDragStartEvent(field: any, entityName: string) {
		//console.log('Drag '+JSON.stringify(field));

		//var count = this.layoutService.getnoOfcontroltype(this.tree,field.controlType);

		// this.tree.fields.forEach(item => {
		// 	this.entityDeatils.fields.forEach(obj => {
		// 		if (item.name === obj.name) {
		// 			obj.draggedItem = true;
		// 		}
		// 	});
		// });

		//event.preventDefault();
		event.stopPropagation();
		//console.log("field", field);
		if (field.type === "DetailEntity") {
			field.dataType = "custom";
			field.controlType = "custom";
		}
		if (field.type && field.type.toLowerCase() === "intersectentity") {
			field.dataType = field.relatedEntity;
			field.controlType = "multiselectdropdown";
		}

		this.selectedField = field;

		var myObj = getTreenodeinstanceWithObject({
			name: field.name,
			value: "",
			required: field.required, //need to change
			dataType: field.dataType,
			fields: [],
			controlType: field.controlType,
			readOnly: field.readOnly,
			decimalPrecision: null,
			defaultValue: field.defaultValue,
			properties: "",
			tabs: [],
			setting: { columnWidth: 12, showHeader: true },
			validators: field.validators,
			selectedView: "",
			typeOf: field.typeOf,
			receivingTypes: field.receivingTypes,
			receiverDataTypes: field.receiverDataTypes,
			broadcastingTypes: field.broadcastingTypes,
			refId:'',
			accessibleLayoutTypes: field.accessibleLayoutTypes,
			toolbar: [],
			entityName: entityName
		});

		if (field.controlType == "Tabs") {

			var tab = getTreenodeinstanceWithObject({
				name: "Tab 1",
				value: "",
				required: false, //need to change
				dataType: "tab",
				controlType: "tab",
				readOnly: false,
				setting: { columnWidth: 12, showHeader: true },
				entityName: entityName
			});
			var tabSection = getTreenodeinstanceWithObject({
				required: true,
				controlType: "section",
				readOnly: false,
				tabs: [],
				setting: { columnWidth: 12, showHeader: true },
				entityName: entityName
			});

			tab.fields.push(tabSection);
			myObj.tabs.push(tab);
		}

		//  if (field.type === "operations") {
		// 	var tabSection = {
		// 		name: "defaultSection",
		// 		value: "",
		// 		required: true,
		// 		dataType: "",
		// 		fields: [],
		// 		controlType: "section",
		// 		decimalPrecision: null,
		// 		defaultValue: "",
		// 		properties: "",
		// 		tabs: [],
		// 		setting: { columnWidth: 12, showHeader: true },
		// 		validators: [],
		// 		selectedView: ""
		// 	}

		// 	tab.fields.push(tabSection);
		// 	myObj.tabs.push(tab);
		// }

		// if (field.type === "tasks") {
		// 	var tasks = {
		// 		name: "tasks",
		// 		value: "",
		// 		required: true,
		// 		dataType: "",
		// 		fields: [],
		// 		controlType: "tooltasks",
		// 		decimalPrecision: null,
		// 		defaultValue: "",
		// 		properties: "",
		// 		tabs: [],
		// 		setting: { columnWidth: 12, showHeader: true },
		// 		validators: [],
		// 		selectedView: ""
		// 	}

		// 	tab.fields.push(tasks);
		// 	myObj.tabs.push(tab);
		// }

		// console.log(""+JSON.stringify(myObj));

		this.dragStart = true;
		this.selectedTreeNode = myObj;
	}

	// private swapfield(data: ITreeNode[], sourceData: ITreeNode, targetdata: ITreeNode) {

	// 	var sourceindex = data.findIndex(t => t.refId == sourceData.refId);
	// 	var tragetindex = data.findIndex(t => t.refId == targetdata.refId);

	// 	//console.log('\n\n\n\n sourceindex ', sourceindex);
	// 	//console.log('tragetindex ', tragetindex);

	// 	if (!this.swapComplete) {

	// 		if (sourceindex >= 0 && tragetindex >= 0) {

	// 			let sourcedata = data.find(t => t.refId == sourceData.refId);
	// 			let targetData = data.find(t => t.refId == targetdata.refId);

	// 			data[sourceindex] = Object.assign({}, targetData);
	// 			data[tragetindex] = Object.assign({}, sourcedata);

	// 			//console.log('data[sourceindex] ', data[sourceindex]);
	// 			//console.log('data[tragetindex] ', data[tragetindex]);


	// 			//console.log('treenodeswap'+JSON.stringify(data));
	// 			this.swapComplete = true;
	// 			return;
	// 		} else {

	// 			data.forEach(element => {

	// 				if (sourceData.controlType === "custom") {
	// 					element.tabs.forEach(tab => {
	// 						tab.fields.forEach(innerelement => {
	// 							innerelement.fields.forEach(customelement => {
	// 								if (customelement.name === sourceData.name) {
	// 									this.swapfield(innerelement.fields, sourceData, targetdata);
	// 								}
	// 							});
	// 						});
	// 					});
	// 				}
	// 				if (element.controlType.toLowerCase() === "tabs" || element.controlType.toLowerCase() === "tab") {

	// 					element.tabs.forEach(tab => {
	// 						tab.fields.forEach(innerelement => {
	// 							innerelement.fields.forEach(customelement => {
	// 								// if (customelement.name === sourceData.name) {
	// 								// 	this.swapfield(innerelement.fields, sourceData, targetdata);
	// 								// }
	// 								//console.log('customelement.controlType.toLowerCase() ', customelement.controlType.toLowerCase());
	// 								//console.log('customelement.name ', customelement);
	// 								//console.log('sourceData.name ', sourceData);
	// 								//console.log('targetdata.name ', targetdata);

	// 								// if(customelement.controlType.toLowerCase() === "section" || customelement.controlType.toLowerCase() === "tabs" || customelement.controlType.toLowerCase() === "tab")
	// 								// {
	// 								// 	//console.log('if');
	// 								// 	this.swapfield(tab.fields, sourceData, targetdata);
	// 								// }
	// 								// else{
	// 								//console.log('else');
	// 								if (customelement.name == sourceData.name) {
	// 									this.swapfield(innerelement.fields, sourceData, targetdata);
	// 								}
	// 								// }


	// 							});
	// 						});
	// 					});
	// 				}
	// 				else {

	// 					if (element.fields) {
	// 						this.swapfield(element.fields, sourceData, targetdata);
	// 					}
	// 				}

	// 			});
	// 		}
	// 	}
	// }

	private removeCommon(data: ITreeNode[], targetData: ITreeNode) {

		if (targetData.fields.length > 0) {
			targetData.fields.forEach(field => {
				this.entityDeatils.fields.forEach(obj => {
					if (obj.name === field.name && field.entityName === this.entityDeatils.name) {
						obj.draggedItem = false;
					}
				});

				if (this.entityDeatils.activityEntity != null && this.entityDeatils.activityEntity != undefined) {
					this.entityDeatils.activityEntity.fields.forEach(obj => {
						if (field.name === obj.name && this.entityDeatils.activityEntity.name === field.entityName) {
							obj.draggedItem = false;
						}
					});
				}

				if (field.tabs.length > 0) {
					field.tabs.forEach(tabObj => {
						this.removeCommon(data, tabObj)
					});

				}

				if (field.controlType.toLowerCase() === "section") {
					this.removeCommon(data, field)
				}
			});
		} else {
			if (targetData.tabs.length > 0) {
				targetData.tabs.forEach(tabObj => {
					this.removeCommon(data, tabObj)
				});

			}

			if (targetData.controlType.toLowerCase() === "section") {
				this.removeCommon(data, targetData)
			}
		}
	}


	private removeData(data: ITreeNode[], targetData: ITreeNode) {
		var index = data.findIndex(t => t.refId == targetData.refId);
		if (index >= 0) {
			//console.log(' if (index >= 0)');
			this.entityDeatils.fields.forEach(obj => {
				if (obj.name === targetData.name && targetData.entityName === this.entityDeatils.name) {
					//console.log(' if (index >= 0)');
					obj.draggedItem = false;
				}
			});

			if (this.entityDeatils.activityEntity != null && this.entityDeatils.activityEntity != undefined) {
				this.entityDeatils.activityEntity.fields.forEach(obj => {
					if (targetData.name === obj.name && this.entityDeatils.activityEntity.name === targetData.entityName) {
						obj.draggedItem = false;
					}
				});
			}


			data.splice(index, 1);
			//this.selectedTreeNode = null;

			return;
		} else {
			//console.log(' } else {');
			data.forEach(element => {

				if (targetData.controlType === "custom") {
					element.tabs.forEach(tab => {
						tab.fields.forEach(innerelement => {
							innerelement.fields.forEach(customelement => {
								if (customelement.name === targetData.name) {
									this.removeData(innerelement.fields, targetData);

								}
							});
						});
					});
				}
				//console.log('element.controlType.toLowerCase() ', element);

				if (element.controlType.toLowerCase() === "tabs" || element.controlType.toLowerCase() === "tab") {
					element.tabs.forEach(tab => {
						tab.fields.forEach(innerelement => {
							innerelement.fields.forEach(customelement => {
								if (customelement.controlType.toLowerCase() === "section" || customelement.controlType.toLowerCase() === "tabs" || customelement.controlType.toLowerCase() === "tab") {
									//console.log('if');
									this.removeData(tab.fields, targetData);

								}
								else {
									//console.log('else');
									if (customelement.name === targetData.name) {
										this.removeData(innerelement.fields, targetData);

									}
								}
							});
						});
					});
				}
				else {
					if (element.fields) {
						this.removeData(element.fields, targetData);
						//this.removeCommon(targetData);
					}

				}
			});

			//	this.removeCommon(targetData);
		}
	}

	// I handle the events from the tree component.
	public handleSelection(node: ITreeNode): void {
		this.selectedTreeNode = node;

		console.groupEnd();
	}

	public handleDropEvent(node: ITreeNode): void {

		// this.showLoader = true;
		// this.swapComplete = false;
		// var errormessage = "";
		// var isField = true;
		// //event.preventDefault();
		// event.stopPropagation();
		// if (node != null && node.controlType && (node.controlType.toLocaleLowerCase() == "section" || node.controlType.toLocaleLowerCase() == "tabs")) {

		// 	if (node.fields !== null && node.fields.length > 0) {

		// 		node.fields.forEach(item => {
		// 			if (item.controlType === "custom") {
		// 				isField = false;
		// 				return;
		// 			}
		// 		});

		// 		if (isField && this.selectedTreeNode.controlType === "custom") {
		// 			errormessage += this.getResourceValue("DetailEntitiesCannotbeAddedWithFields") +"<br/>";
		// 		}
		// 		else if (!isField && this.selectedTreeNode.controlType !== "custom") {
		// 			errormessage += this.getResourceValue("FieldsCannotbeAddedWithDetailEntities")+"<br/>";
		// 		}
		// 		else if (!isField && this.selectedTreeNode.controlType === "custom") {
		// 			errormessage += this.getResourceValue("OneDetailEntityisAllowedinOneTab") +"<br/>";
		// 		}

		// 		//validate whether field is already added
		// 		node.fields.forEach(item => {
		// 			//console.log('item ', item);
		// 			if (item.name === this.selectedTreeNode.name && (item.entityName === this.selectedTreeNode.entityName) && this.selectedTreeNode.controlType !== "Section" && this.selectedTreeNode.controlType !== "Tabs" && this.selectedTreeNode.controlType !== "Tab") {
		// 				errormessage += this.selectedTreeNode.name + this.getResourceValue("FieldIsAlreadyAdded") +"<br/>";
		// 			}

		// 		});
		// 		this.entityDeatils.fields.forEach(obj => {
		// 			// if (item.name === obj.name) {
		// 			if (this.selectedTreeNode.name === obj.name && this.entityDeatils.name === this.selectedTreeNode.entityName) {
		// 				obj.draggedItem = true;
		// 			}
		// 		});

		// 		if (this.entityDeatils.activityEntity != null && this.entityDeatils.activityEntity != undefined) {
		// 			this.entityDeatils.activityEntity.fields.forEach(obj => {
		// 				if (this.selectedTreeNode.name === obj.name && this.entityDeatils.activityEntity.name === this.selectedTreeNode.entityName) {
		// 					obj.draggedItem = true;
		// 				}
		// 			});
		// 		}

		// 	}

		// 	if (errormessage !== "") {
		// 		this.toster.showError(errormessage);
		// 	} else {
		// 		if (!this.dragStart) {
		// 			this.removeData(this.tree.fields, this.selectedTreeNode);
		// 		}

		// 		//if control type is custom then set view 
		// 		if (isField && this.selectedTreeNode.controlType === "custom") {
		// 			this.getDefaultLayout(this.entityname, "View", "", "", node);
		// 		} else {
		// 			//console.log('this.tree.fields if ', this.tree.fields);
		// 			//console.log('this.selectedTreeNode if ', this.selectedTreeNode);
		// 			//console.log('node if ', node);
		// 			node.fields.push(this.selectedTreeNode);
		// 			this.showLoader = false;
		// 			this.entityDeatils.fields.forEach(obj => {
		// 				// if (item.name === obj.name) {							
		// 				if (this.selectedTreeNode.name === obj.name && this.entityDeatils.name === this.selectedTreeNode.entityName) {
		// 					obj.draggedItem = true;
		// 				}
		// 			});

		// 			if (this.entityDeatils.activityEntity != null && this.entityDeatils.activityEntity != undefined) {
		// 				this.entityDeatils.activityEntity.fields.forEach(obj => {
		// 					if (this.selectedTreeNode.name === obj.name && this.entityDeatils.activityEntity.name === this.selectedTreeNode.entityName) {
		// 						obj.draggedItem = true;
		// 					}
		// 				});
		// 			}

		// 		}
		// 	}
		// }
		// else if (node != null && node.controlType && node.controlType.toLocaleLowerCase() != "section" && node.controlType.toLocaleLowerCase() != "tabs") {
		// 	//console.log('this.dragStart ', this.dragStart);

		// 	if (!this.dragStart) {
		// 		this.swapfield(this.tree.fields, this.selectedTreeNode, node);
		// 	}
		// 	else {
		// 		//console.log('this.tree.fields else ', this.tree.fields);
		// 		//console.log('this.selectedTreeNode else ', this.selectedTreeNode);
		// 		//console.log('node else ', node);
		// 		//this.tree.fields.push(this.selectedTreeNode);
		// 		//console.log('this.tree.fields 2 ', this.tree.fields);
		// 		if (node.controlType.toLocaleLowerCase() !== "section" && node.controlType.toLocaleLowerCase() !== "tabs") {
		// 			var sourceindex = this.tree.fields.findIndex(t => t.refId == node.refId);
		// 			//console.log('sourceindex if ', sourceindex);
		// 			//this.swapfield(this.tree.fields, this.selectedTreeNode, node);				
		// 			let fieldAndIndex: any;
		// 			if (sourceindex >= 0) {
		// 				this.tree.fields.splice(sourceindex, 0, this.selectedTreeNode);

		// 				this.entityDeatils.fields.forEach(obj => {
		// 					// if (item.name === obj.name) {								
		// 					if (this.selectedTreeNode.name === obj.name && this.entityDeatils.name === this.selectedTreeNode.entityName) {
		// 						obj.draggedItem = true;
		// 					}
		// 				});

		// 				if (this.entityDeatils.activityEntity != null && this.entityDeatils.activityEntity != undefined) {
		// 					this.entityDeatils.activityEntity.fields.forEach(obj => {
		// 						if (this.selectedTreeNode.name === obj.name && this.entityDeatils.activityEntity.name === this.selectedTreeNode.entityName) {
		// 							obj.draggedItem = true;
		// 						}
		// 					});
		// 				}

		// 			}
		// 			else {
		// 				this.setField(this.tree.fields, node);
		// 				//node.fields.splice(sourceindex, 0, this.selectedTreeNode);
		// 			}
		// 			//console.log('this.tree.fields 2 ', this.tree.fields);
		// 		}
		// 		//console.log('this.tree.fields 2 ', this.tree.fields);
		// 		// }
		// 		// else
		// 		// {
		// 		// 	let sourceindex = node.fields.findIndex(t => t.refId == node.fields.refId);
		// 		// 	console.log('sourceindex else ', sourceindex);
		// 		// 	node.fields.splice(sourceindex, 0, this.selectedTreeNode);
		// 		// }
		// 	}
		// }
		// this.dragStart = false;
		/////////////////////////////////////////////////////////////

		event.stopPropagation();

		//console.log('\n\n\nthis.selectedTreeNode ', this.selectedTreeNode);
		//console.log('node ', node);

		// if (this.selectedTreeNode && this.selectedTreeNode["itemIndex"]) {
		// 	var sourceIndex = this.selectedTreeNode["itemIndex"].split(",");
		// }

		// if (node && node["itemIndex"]) {
		// 	var targetIndex = node["itemIndex"].split(",");
		// }

		//if (!this.dragStart) {
		this.putElement(this.selectedTreeNode, node);
	}


	private splitTextNumber(inputText) {
		var output = [];
		var json = inputText.split(' ');
		json.forEach(function (item) {
			output.push(item.replace(/\'/g, '').split(/(\d+)/).filter(Boolean));
		});
		return output;
	}

	private putElement(sourceNode, targetNode) {

		//console.log('\n\n\n\n  tree ', this.tree);
		// console.log("\n\nsourceNode ", sourceNode);
		// console.log("targetNode ", targetNode);

		var buildSource = "";
		var buildSourceIndex;
		var buildSourceContainer = "";
		var buildTargetIndex;
		var buildTargetContainer = "";

		var tempSource = [];
		var tempTarget = [];

		if (sourceNode["itemIndex"]) {
			var sourceIndex = sourceNode["itemIndex"].split(",");
			sourceIndex.forEach((element, index) => {
				if (index != 0) {
					tempSource.push(this.splitTextNumber(element));
				}
			});

			tempSource.forEach(element => {
				if (element[0][0].indexOf("s") > -1) {
					buildSource += '["fields"][' + element[0][1] + ']';
					buildSourceContainer += '["fields"][' + element[0][1] + ']';
				}
				else if (element[0][0].indexOf("tg") > -1) {
					buildSource += '["fields"][' + element[0][1] + ']';
					buildSourceContainer += '["fields"][' + element[0][1] + ']';
				}
				else if (element[0][0].indexOf("ti") > -1) {
					buildSource += '["tabs"][' + element[0][1] + ']';
					buildSourceContainer += '["tabs"][' + element[0][1] + ']';
				}
				else {
					buildSource += '["fields"][' + element[0][0] + ']';
					buildSourceIndex = parseInt(element[0][0]);
				}
			});
		}

		if (targetNode["itemIndex"]) {
			var targetIndex = targetNode["itemIndex"].split(",");
			targetIndex.forEach((element, index) => {
				if (index != 0) {
					tempTarget.push(this.splitTextNumber(element));
				}
			});

			tempTarget.forEach(element => {
				if (element[0][0].indexOf("s") > -1) {
					buildTargetContainer += '["fields"][' + parseInt(element[0][1]) + ']';
				}
				else if (element[0][0].indexOf("tg") > -1) {
					buildTargetContainer += '["fields"][' + parseInt(element[0][1]) + ']';
				}
				else if (element[0][0].indexOf("ti") > -1) {
					buildTargetContainer += '["tabs"][' + parseInt(element[0][1]) + ']';
				}
				else {
					buildTargetIndex = parseInt(element[0][0]);
				}
			});
		}

		var s, si, sc, ti, tc;
		if (buildSource) {
			s = eval("this.tree" + buildSource);
		}
		else {
			s = this.selectedTreeNode;
		}

		if (buildSourceIndex >= 0) {
			si = buildSourceIndex;
		}

		//console.log('buildSourceContainer ', buildSourceContainer);

		if (buildSourceContainer) {
			sc = eval("this.tree" + buildSourceContainer + '["fields"]');
		}
		else
		{
			sc = eval("this.tree" + '["fields"]');
		}

		if (buildTargetIndex >= 0) {
			ti = buildTargetIndex;
		}

		tc = eval("this.tree" + buildTargetContainer + '["fields"]');

		// console.log('\n\n\ns ', s);
		// console.log('si ', si);
		// console.log('sc ', sc);
		// console.log('\n\nti ', ti);
		// console.log('tc ', tc);
		// console.log('buildSourceContainer ', buildSourceContainer);
		// console.log('buildTargetContainer ', buildTargetContainer);


		if (si >= 0 && ti >= 0) {
			if (buildSourceContainer === buildTargetContainer) {
				if (ti < si) {
					tc.splice(si, 1);
					tc.splice(ti, 0, s);
					//console.log('tc 1 ', tc);
				}
				else if (si < ti) {
					tc.splice(si, 1);
					tc.splice(ti, 0, s);
					//console.log('tc 2 ', tc);
				}
			}
			else {
				sc.splice(si, 1);
				tc.splice(ti, 0, s);
			}
		}
		else if (si >= 0 && !ti) {
			sc.splice(si, 1);//removing dragged source element
			tc.push(s);//adding dragged source element in the section of target
		}
		else if (!si && !ti) {
			tc.push(s);//adding dragged source element in the section of target
			this.entityDeatils.fields.forEach(obj => {

				if (this.selectedTreeNode.name === obj.name && this.entityDeatils.name === this.selectedTreeNode.entityName) {
					obj.draggedItem = true;
				}
			});

			if (this.entityDeatils.activityEntity != null && this.entityDeatils.activityEntity != undefined) {
				this.entityDeatils.activityEntity.fields.forEach(obj => {
					if (this.selectedTreeNode.name === obj.name && this.entityDeatils.activityEntity.name === this.selectedTreeNode.entityName) {
						obj.draggedItem = true;
					}
				});
			}
		}
		else if (!si && ti >= 0) {
			tc.splice(ti, 0, s);//placing dragged source element in the index of target
		}
		this.setItemIndex();
		//console.log('new tree ', this.tree);
	}


	private setItemIndex() {
		let treeNode = this.tree;
		treeNode["itemIndex"] = "s0";
		if (treeNode.fields.length > 0) {
			this.findAndSetIndex(treeNode.fields, treeNode["itemIndex"]);
		}
		//this.newTreeEvent.emit(this.rootNode);
		//console.log('this.rootNode final ', this.rootNode);
	}

	private findAndSetIndex(argTreeNode, parentIndex) {
		argTreeNode.forEach((element, index) => {
			//console.log('element ', element);

			//console.log('index ', index);
			if (element.controlType.toLowerCase() === "section") {
				element["itemIndex"] = parentIndex + ",s" + index;
				if (element.fields.length > 0) {
					this.findAndSetIndex(element.fields, element["itemIndex"]);
				}
			}
			else if (element.controlType.toLowerCase() === "tabs") {
				element["itemIndex"] = parentIndex + ",tg" + index;
				this.findAndSetIndex(element.tabs, element["itemIndex"]);
			}
			else if (element.controlType.toLowerCase() === "tab") {
				element["itemIndex"] = parentIndex + ",ti" + index;
				this.findAndSetIndex(element.fields, element["itemIndex"]);
			}
			else {
				element["itemIndex"] = parentIndex + "," + index;
			}
		});
	}

	private setField(tree, node) {
		//
		var sectionindex = tree.findIndex(t => t.name == 'Section');
		var tabgrpindex = tree.findIndex(t => t.name == 'Tabs');
		var nodeIndex = tree.findIndex(t => t.name == node.name);
		//
		//console.log('\n\n\nsetField :: sectionindex ', sectionindex);
		//console.log('nodeIndex ', nodeIndex);		
		//console.log('tree ', tree);

		if (sectionindex >= 0) {
			//console.log('this.tree.fields[sectionindex].fields ', this.tree.fields[sectionindex].fields);
			//console.log('node ', node);
			if (nodeIndex === -1) {
				this.setField(tree[sectionindex].fields, node);
			}
			else {
				tree.splice(nodeIndex, 0, this.selectedTreeNode);
				this.showLoader = false;
				return;
			}
		}
		else if (tabgrpindex >= 0) {
			//console.log('tabgrpindex ', tabgrpindex);
			//console.log('tree[tabgrpindex] ', tree[tabgrpindex]);
			if (nodeIndex === -1) {
				//this.setField(tree[tabindex].fields, node);
				tree[tabgrpindex].tabs.forEach(element => {
					this.setField(element.fields[0].fields, node);
				});
			}
			else {
				tree.splice(nodeIndex, 0, this.selectedTreeNode);
				return;
			}
		}
		else {
			tree.splice(nodeIndex, 0, this.selectedTreeNode);
			return;
		}


		this.entityDeatils.fields.forEach(obj => {

			if (this.selectedTreeNode.name === obj.name && this.entityDeatils.name === this.selectedTreeNode.entityName) {
				obj.draggedItem = true;
			}
		});

		if (this.entityDeatils.activityEntity != null && this.entityDeatils.activityEntity != undefined) {
			this.entityDeatils.activityEntity.fields.forEach(obj => {
				if (this.selectedTreeNode.name === obj.name && this.entityDeatils.activityEntity.name === this.selectedTreeNode.entityName) {
					obj.draggedItem = true;
				}
			});
		}
	}

	public handleCloseEvent(node: ITreeNode): void {
		//console.log('handleCloseEvent called');
		this.deleteComponent(node);
	}
	public handleEditEvent(node: ITreeNode): void {

		//console.log('handleEditEvent called');
		this.configureSettings(node);
	}
	public handleAddEvent(node: ITreeNode): void {
		//console.log('handleAddEvent called');
		this.configureAdd(node);
	}


	private deleteComponent(node: ITreeNode) {
		swal({
			title: this.getResourceValue("Areyousure"),
            text: this.getResourceValue("Youwntbeabletorevertthis"),
            type: this.getResourceValue('warning'),
			showCancelButton: true,
			confirmButtonColor: '#3085d6',
			cancelButtonColor: '#d33',
			confirmButtonText: this.getResourceValue('Yesdeleteit'),
			showLoaderOnConfirm: true,
		}).then((willDelete) => {
			if (willDelete.value) {
				this.removeData(this.tree.fields, node);
				this.removeCommon(this.tree.fields, node)
			} else {
				//write the code for cancel click
			}
		});
	}

	private configureSettings(node: ITreeNode) {
		//console.log(' configureSettings  ', node);
		let ngbModalOptions: NgbModalOptions = {
			backdrop: 'static',
			keyboard: false
		};
		const modalRef = this.modalService.open(MODALS[node.controlType.toLowerCase()], ngbModalOptions);
		let nodeObj = JSON.parse(JSON.stringify(node))
		//console.log("test"+JSON.stringify(node));

		if (modalRef.componentInstance !== undefined) {
			modalRef.componentInstance.node = nodeObj;
			modalRef.componentInstance.saveEvent.subscribe((receivedEntry) => {
				//console.log("saveEvent.subscribe", receivedEntry)
				node.name = receivedEntry.name;
				node.setting.showHeader = receivedEntry.setting.showHeader;
				node.setting.columnWidth = receivedEntry.setting.columnWidth;
				node.selectedView = receivedEntry.selectedView;
				node.validators = receivedEntry.validators;
				node.tabs = receivedEntry.tabs;

				modalRef.close();
			});
		}
		else {
			modalRef.close();
			this.toster.showWarning(this.getResourceValue("SettingsUnavailable"));
		}
	}

	private configureAdd(node: ITreeNode) {
		let ngbModalOptions: NgbModalOptions = {
			backdrop: 'static',
			keyboard: false
		};
		const modalRef = this.modalService.open(MODALS['add'], ngbModalOptions);
		let nodeObj = JSON.parse(JSON.stringify(node));
		modalRef.componentInstance.node = nodeObj;
		modalRef.componentInstance.saveEvent.subscribe((receivedEntry) => {
			// node.name = receivedEntry.name;
			// node.settings.showHeader = receivedEntry.settings.showHeader;
			// node.settings.columnWidth = receivedEntry.settings.columnWidth;
			var section =getTreenodeinstanceWithObject({
				name: receivedEntry.name,
				required: false,
				controlType: "section",
				readOnly: false,
				setting:
				{
					columnWidth: receivedEntry.setting.columnWidth,
					showHeader: receivedEntry.setting.showHeader
				},
				validators: receivedEntry.validators,
				accessibleLayoutTypes: receivedEntry.accessibleLayoutTypes,
				toolbar: [],
				entityName: ""
			});
			node.fields.push(section);
			modalRef.close();
		});
	}

	private configure() {

	}

	private setdeactivefields(node: ITreeNode) {
		this.entityDeatils.fields.forEach(obj => {
			if (node.name === obj.name && node.entityName === this.entityDeatils.name) {
				obj.draggedItem = true;
			}
		});

		if (this.entityDeatils.activityEntity != null && this.entityDeatils.activityEntity != undefined) {
			this.entityDeatils.activityEntity.fields.forEach(obj => {
				if (node.name === obj.name && this.entityDeatils.activityEntity.name === node.entityName) {
					obj.draggedItem = true;
				}
			});
		}


		if (node.tabs != null && node.tabs.length > 0) {
			node.tabs.forEach(tab => {

				this.entityDeatils.fields.forEach(obj => {
					if (tab.name === obj.name && tab.entityName === this.entityDeatils.name) {
						obj.draggedItem = true;
					}
				});

				if (this.entityDeatils.activityEntity !== null && this.entityDeatils.activityEntity !== undefined) {
					this.entityDeatils.activityEntity.fields.forEach(obj => {
						if (tab.name === obj.name && tab.entityName === this.entityDeatils.activityEntity.name) {
							obj.draggedItem = true;
						}
					});

				}


				tab.fields.forEach(innerelement => {
					this.entityDeatils.fields.forEach(obj => {
						if (innerelement.name === obj.name && innerelement.entityName === this.entityDeatils.name) {
							obj.draggedItem = true;
						}
					});

					if (this.entityDeatils.activityEntity !== null && this.entityDeatils.activityEntity !== undefined) {
						this.entityDeatils.activityEntity.fields.forEach(obj => {
							if (innerelement.name === obj.name && innerelement.entityName === this.entityDeatils.activityEntity.name) {
								obj.draggedItem = true;
							}
						});
					}


					innerelement.fields.forEach(nextf => {
						this.entityDeatils.fields.forEach(obj => {
							if (nextf.name === obj.name && nextf.entityName === this.entityDeatils.name) {
								obj.draggedItem = true;
							}
						});

						if (this.entityDeatils.activityEntity !== null && this.entityDeatils.activityEntity !== undefined) {
							this.entityDeatils.activityEntity.fields.forEach(obj => {
								if (nextf.name === obj.name && nextf.entityName === this.entityDeatils.activityEntity.name) {
									obj.draggedItem = true;
								}
							});

						}

						if (nextf.controlType === "Section") {
							this.setdeactivefields(nextf);
						}

						if (nextf.tabs.length > 0) {
							this.setdeactivefields(nextf);
						}

					});

					if (innerelement.tabs.length > 0) {
						this.setdeactivefields(innerelement);

					}
				});
				if (tab.tabs.length > 0) {
					this.setdeactivefields(tab);

				}
			});
		} else {
			node.fields.forEach(innerelement => {
				this.entityDeatils.fields.forEach(obj => {
					if (innerelement.name === obj.name && innerelement.entityName === this.entityDeatils.name) {
						obj.draggedItem = true;
					}
				});

				if (this.entityDeatils.activityEntity !== null && this.entityDeatils.activityEntity !== undefined) {
					this.entityDeatils.activityEntity.fields.forEach(obj => {
						if (innerelement.name === obj.name && innerelement.entityName === this.entityDeatils.activityEntity.name) {
							obj.draggedItem = true;
						}
					});

				}

				innerelement.fields.forEach(nextf => {
					this.entityDeatils.fields.forEach(obj => {
						if (nextf.name === obj.name && nextf.entityName === this.entityDeatils.name) {
							obj.draggedItem = true;
						}
					});

					if (this.entityDeatils.activityEntity !== null && this.entityDeatils.activityEntity !== undefined) {
						this.entityDeatils.activityEntity.fields.forEach(obj => {
							if (nextf.name === obj.name && nextf.entityName === this.entityDeatils.activityEntity.name) {
								obj.draggedItem = true;
							}
						});
					}

					if (nextf.tabs.length > 0) {
						this.setdeactivefields(nextf);
					}
				});

				if (innerelement.fields.length > 0) {
					this.setdeactivefields(innerelement);
				}
				if (innerelement.tabs.length > 0) {
					this.setdeactivefields(innerelement);
				}
			});
		}



	}

	private getDefaultLayout(name: string, type: string, subtype: string, context: string, node: ITreeNode) {
		//console.log('node : ', node);
		this.layoutService.getDefaultLayout(name, type, subtype, context)
			.pipe(first())
			.subscribe(
				data => {
					if (data) {
						this.defaultView = data;

						if (this.defaultView) {
							this.selectedTreeNode.selectedView = this.defaultView.id;
							node.fields.push(this.selectedTreeNode);
						}
					}
				},
				error => {
					console.log(error);
				});
	}

	private setidExistingfields(node: ITreeNode) {
		node.refId = node.refId || generateRandomNo();

		node.fields.forEach(item => {


			this.setidExistingfields(item);

			if (item.fields) {
				item.fields.forEach(item3 => {


					this.setidExistingfields(item3);

				});
			}
		});
		if (node.tabs) {
			node.tabs.forEach(item2 => {


				this.setidExistingfields(item2);

				if (item2.fields) {
					item2.fields.forEach(item3 => {


						this.setidExistingfields(item3);

					});
				}

			});
		}
	}

	public addItem(): void {
		this.isdatamainlist = false;
		if (this.selectedItemFromMainList != null && (typeof this.selectedItemFromMainList.isAdded === "undefined" || !this.selectedItemFromMainList.isAdded)) {
			for (var k = 0; k < this.addedItemToMainList.length; k++) {
				if (this.addedItemToMainList[k].name === this.selectedItemFromMainList.name) {
					this.isdatamainlist = true;
				}
			}

			if (this.isdatamainlist === false) {
				this.selectedItemFromMainList.sequence = (this.addedItemToMainList != null) ? this.addedItemToMainList.length + 1 : 1;
				this.addedItemToMainList.push(this.selectedItemFromMainList);
				this.selectedItemFromMainList.isAdded = true;
				this.processSelectedField(this.selectedItemFromMainList);
			}
		}
	}

	public removeItem(): void {
		this.displayToolbarItem = false;
		var index = this.addedItemToMainList.indexOf(this.selectedItemFromAddedList);
		if (index != -1) {
			for (var i = 0; i < this.addedItemToMainList.length; i++) {
				if (this.addedItemToMainList[i].name != this.selectedItemFromAddedList.name) continue;
				for (var j = 0; j < this.addedItemToMainList.length; j++) {
					if (this.addedItemToMainList[j].sequence > this.addedItemToMainList[i].sequence) {
						this.addedItemToMainList[j].sequence -= 1;
						break;
					}
				}
			}
			this.addedItemToMainList.splice(index, 1);
			for (var k = 0; k < this.toolbarSource.length; k++) {
				if (this.toolbarSource[k].name == this.selectedItemFromAddedList.name) {
					this.toolbarSource[k].isAdded = false;
				}
			}
		}

	}

	public mainItemClickEvent(item): void {
		if (item != null && (typeof item.isAdded === "undefined" || !item.isAdded)) {
			this.selectedItemFromMainList = item;
			this.resetOthers(this.toolbarSource);
			this.selectedItemFromMainList.isRowSelected = true;
		}
	}

	public slectedItemClickEvent(item): void {
		this.processSelectedField(item);
	}

	public upItem(): void {
		if (this.selectedItemFromAddedList) {
			for (var i = 0; i < this.addedItemToMainList.length; i++) {
				if (this.addedItemToMainList[i].name === this.selectedItemFromAddedList.name) {
					for (var j = 0; j < this.addedItemToMainList.length; j++) {
						if (this.addedItemToMainList[j].sequence === (this.addedItemToMainList[i].sequence - 1)) {
							this.addedItemToMainList[j].sequence += 1;
							break;
						}
					}
					this.addedItemToMainList[i].sequence -= 1;
					break;
				}
			}

			this.addedItemToMainList = this.addedItemToMainList.sort().sort(function (a, b) {
				return a.sequence - b.sequence;
			});
		}
	}

	public downItem(): void {
		if (this.selectedItemFromAddedList) {
			for (var i = 0; i < this.addedItemToMainList.length; i++) {
				if (this.addedItemToMainList[i].name === this.selectedItemFromAddedList.name) {
					for (var j = 0; j < this.addedItemToMainList.length; j++) {
						if (this.addedItemToMainList[j].sequence === (this.addedItemToMainList[i].sequence + 1)) {
							this.addedItemToMainList[j].sequence -= 1;
							break;
						}
					}
					this.addedItemToMainList[i].sequence += 1;
					break;
				}
			}

			this.addedItemToMainList = this.addedItemToMainList.sort().sort(function (a, b) {
				return a.sequence - b.sequence;
			});
		}
	}

	private processSelectedField(item): void {
		if (item != null) {
			this.selectedItemFromAddedList = item;
			this.selectedTask = item.name;
			this.resetOthers(this.addedItemToMainList);
			this.selectedItemFromAddedList.isRowSelected = true;
			this.displayToolbarItem = false;

			if (item.type === 'task') {
				this.displayToolbarItem = true;
				if (this.tree.toolbar !== null && this.tree.toolbar !== undefined && this.tree.toolbar.length > 0) {
					for (var i = 0; i < this.tree.toolbar.length; i++) {
						if (this.tree.toolbar[i].type === 'task') {
							if (this.selectedTask === this.tree.toolbar[i].name) {
								this.toolbarGroup = this.tree.toolbar[i].group;
							}
						}
					}
				}
			}
		}
	}

	private resetOthers(array): void {
		if (array != null) {
			for (var k = 0; k < array.length; k++) {
				array[k].isRowSelected = false;
			}
		}
	}

	// public onChangeEvent(ev): void {
	// 	for (var i = 0; i < this.tree.toolbar.length; i++) {
	// 		if (this.tree.toolbar[i].type === 'task') {
	// 			if (this.selectedTask === this.tree.toolbar[i].name) {
	// 				this.tree.toolbar[i].group = ev.target.value;
	// 			}
	// 		}
	// 	}
	// }

	public onChangeEvent(ev): void {
		for (var i = 0; i < this.addedItemToMainList.length; i++) {
			if (this.addedItemToMainList[i].type === 'task') {
				if (this.selectedTask === this.addedItemToMainList[i].name) {
					this.addedItemToMainList[i].group = ev.target.value;
				}
			}
		}
	}
}
