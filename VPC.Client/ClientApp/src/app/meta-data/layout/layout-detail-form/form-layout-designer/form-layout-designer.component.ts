import { Component, OnInit, NgZone, ChangeDetectorRef, ChangeDetectionStrategy, ViewChildren, QueryList } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { fromEvent, Observable } from 'rxjs';
import { findIndex, ignoreElements } from 'rxjs/operators';
import { Router, ActivatedRoute, Params } from '@angular/router';

import { first } from 'rxjs/operators';

import { predifinedType } from 'src/app/dynamic-form-builder/helper/utils';
import { TosterService } from 'src/app/services/toster.service';

import { MODALS } from 'src/app/dynamic-form-builder/tree.config'
import swal from 'sweetalert2';
import { NgbModal, NgbModalOptions } from '@ng-bootstrap/ng-bootstrap';

import { HostListener } from "@angular/core";
import { Tasks } from 'src/app/model/tasks';
import { jsonpCallbackContext } from '@angular/common/http/src/module';
import { ConvertActionBindingResult } from '@angular/compiler/src/compiler_util/expression_converter';
import { SharedTreeService } from 'src/app/dynamic-form-builder/service/sharedtree.service';
import { ITreeNode, getTreenodeinstanceWithObject } from 'src/app/model/treeNode';
import { LayoutModel } from 'src/app/model/layoutmodel';
import { Entities } from 'src/app/model/entities';
import { FieldModel } from 'src/app/model/fieldmodel';
import { Resource } from 'src/app/model/resource';
import { TreeNodeComponent } from 'src/app/dynamic-form-builder/tree-node.component';
import { LayoutService } from '../../layout.service';
import { CommonService } from 'src/app/services/common.service';
import { MetadataService } from 'src/app/meta-data/metadata.service';
import { GlobalResourceService } from 'src/app/global-resource/global-resource.service';
import { PicklistLayoutService } from 'src/app/picklist/picklist-layout/picklist-layout.service';




@Component({
	selector: 'app-form-layout-designer',
	templateUrl: './form-layout-designer.component.html',
	styleUrls: ['./form-layout-designer.component.css'],
	providers: [SharedTreeService],
	//changeDetection: ChangeDetectionStrategy.OnPush
})


export class FormLayoutDesignerComponent implements OnInit {
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
	addedItemToMainList: any = [];
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
	selectedRelatedEntityIndex = 0;
	@ViewChildren(TreeNodeComponent) treeNodeComponent: QueryList<TreeNodeComponent>;
	public entityInfo: any[];
	selectedLayout: any;
	config: { allowConfiguration: boolean; displaySortColumn: boolean; direction: boolean; maxResult: boolean; groupBy: boolean; clickableColumn: boolean; };
	pageInfo:
		{
			config:
			{
				allowConfiguration: boolean, displaySortColumn: boolean, direction: boolean, maxResult: boolean,
				groupBy: boolean, clickableColumn: boolean
			}
			, selectedLayout: {},
			addedItemToMainList: [],
			fieldSource: [],
			type: string

		}
	fieldSource: any = [];
	type: string = "";
	//
	// public layoutContext: string = "";
	// public subTypeName: string = "";

	// I initialize the app component.
	constructor(
		private router: Router,
		private activatedRoute: ActivatedRoute,
		private layoutService: LayoutService,
		private pllayoutService: PicklistLayoutService,
		private commonService: CommonService,
		private metadataService: MetadataService,
		private zone: NgZone,
		private changeRef: ChangeDetectorRef,
		private modalService: NgbModal,
		private toster: TosterService,
		public globalResourceService: GlobalResourceService,
		private sharedTreeService: SharedTreeService,
	) {
		sharedTreeService.forminstance = this;
		this.getScreenSize();
	}

	@HostListener('window:resize', ['$event'])
	getScreenSize(event?) {
		this.screenHeight = window.innerHeight;
	}



	ngDoCheck() {
		this.sharedTreeService.stickFieldToolBar() 
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
			this.entityname = params['entityName'] ? params['entityName'] : params['picklistName'];
			this.type = params['entityName'] ? 'entity' : 'picklist'
			this.getMetadataFieldsByName(this.entityname);
		});
	}

	private addMandatoryFieldsAndActivityEntity(item, entityName) {
		var myObj = getTreenodeinstanceWithObject({
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
			selectedFormOrList: "",
			typeOf: item.typeOf,
			receivingTypes: item.receivingTypes,
			receiverDataTypes: item.receiverDataTypes,
			broadcastingTypes: item.broadcastingTypes,
			refId: '',
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
		} else {
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
				if (this.metadatafield.versionControl != undefined) {
					this.metadatafield.versionControl.fields.forEach(item => {
						if (item.controlType.toLocaleLowerCase() !== 'label' && item.required == true && (item.accessibleLayoutTypes === undefined || item.accessibleLayoutTypes.find(x => x === 2))) {
							this.addMandatoryFieldsAndActivityEntity(item, this.metadatafield.versionControl.name)
						}
					});
				}
			}

			this.updateLayout();
		}
		this.setactivityOnMetadataFields();
		//console.log('beforeSetId '+JSON.stringify(this.tree));
		this.sharedTreeService.setidExistingfields(this.tree);

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

		this.sharedTreeService.setItemIndex();
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

		this.pllayoutService.updateFormLayout(this.layoutInfo, this.entityname, this.layoutId, this.type).subscribe(result => {
			//console.log(result);
			this.toster.showSuccess(this.getResourceValue("metadata_operation_save_success_message"));
		});

		//console.log(' this.tree ', this.tree);
	}

	private getMetadataFieldsByName(name) {
		this.metadataService.getMetadataByName(name)
			.pipe(first())
			.subscribe(
				data => {
					this.metadatafield = data;
					this.initAfterDataFieldByName();
					this.setData(data)
					//console.log('getMetadataFieldsByName metadatafield '+ JSON.stringify(this.metadatafield));
				},
				error => {
					console.log(error);
				});
		//console.log('treenode'+JSON.stringify(this.tree.fields));
	}
	setData(data) {

		this.selectedLayout = this.layoutInfo.formLayoutDetails;
		this.addedItemToMainList = this.selectedLayout.toolbar ? this.selectedLayout.toolbar : [];
		this.manipulateToolBar(data)
		this.config = { allowConfiguration: true, displaySortColumn: false, direction: false, maxResult: false, groupBy: false, clickableColumn: false }
		//for left side gray
		this.setFieldSourceProperties()
		this.setPageInfo()
	}

	setFieldSourceProperties() {
		for (var i = 0; i < this.addedItemToMainList.length; i++) {
			for (var j = 0; j < this.fieldSource.length; j++) {
				if (this.addedItemToMainList[i].name === this.fieldSource[j].name) {
					this.fieldSource[j].isRowSelected = true;
					this.fieldSource[j].isAdded = true;
				}
			}
		}
	}
	setPageInfo() {
		this.pageInfo = {
			config: this.config,
			selectedLayout: this.selectedLayout,
			addedItemToMainList: this.addedItemToMainList,
			fieldSource: this.fieldSource,
			type: 'toolbar'
		}
	}
	manipulateToolBar(data) {
		if (data.operations) {
			for (var k = 0; k < data.operations.length; k++) {
				this.fieldSource.push(data.operations[k]);
			}
		}
		if (data.tasks) {
			for (var k = 0; k < data.tasks.length; k++) {
				this.fieldSource.push(data.tasks[k]);
			}
		}
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

			//fields mapper...
			this.entityDeatils.fields = this.mapAccessibilityRule(this.metadatafield.fields);
			//----- logic for qucik add
			this.mapQuickAddMapper(this.entityDeatils.fields);
			//----------

			if (this.entityDeatils.versionControl && this.entityDeatils.versionControl.fields) {
				this.entityDeatils.versionControl.fields = this.mapAccessibilityRule(this.metadatafield.versionControl.fields);
				this.mapQuickAddMapper(this.entityDeatils.versionControl.fields);
			}

			if (this.metadatafield.activityEntity && this.metadatafield.activityEntity.fields) {
				this.entityDeatils.activityEntity.fields = this.mapAccessibilityRule(this.entityDeatils.activityEntity.fields);
				//this.entityDeatils.activityEntity.fields = this.metadatafield.activityEntity.fields.filter(f => (f.accessibleLayoutTypes && f.accessibleLayoutTypes.find(e => e === 2)));
			}

			this.tree.fields.forEach(item => {
				this.setdeactivefields(item);
			});
		}
	}
	mapQuickAddMapper(fields: FieldModel[]) {
		fields.forEach(element => {
			if (element.supportedQuickAddModes) {
				element.supportedQuickAddModes.forEach(layoutType => {
					var types = layoutType.toString().split('');
					if (types.length > 1) {
						element.isQuickAddSupported = false;
						if (types[0] == this.layoutInfo.layoutType.toString()) {
							if (types[1] == this.layoutInfo.context.toString()) {
								element.isQuickAddSupported = true;
								return;
							}
						}
					}
				});
			}
		});
	}


	mapAccessibilityRule(fields: any): FieldModel[] {
		return fields.filter(f => (f.accessibleLayoutTypes && f.accessibleLayoutTypes.find(e =>
			(e.toString().split('').length == 1 && e.toString().split('')[0] === this.layoutInfo.layoutType.toString())
			||
			(
				e.toString().split('').length > 1 && e.toString().split('')[0] === this.layoutInfo.layoutType.toString() && e.toString().split('')[1] === this.layoutInfo.context.toString()
			)
		)));
	}

	public fieldDragStartEvent(field: any, entity) {

		//event.preventDefault();

		(<any>event).stopPropagation();
		//console.log("field", field);
		field.entityName = entity
		if (field.type === "DetailEntity") {
			field.dataType = "custom";
			field.controlType = "custom";
			//field.entityName = field.name
		}
		if (field.type && field.type.toLowerCase() === "intersectentity") {
			field.dataType = field.relatedEntity;
			//field.entityName = field.name
			field.controlType = "multiselectdropdown";
		}

		//console.log("setting checking:::", field);
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
			setting: null,
			validators: field.validators,
			selectedView: "",
			selectedFormOrList:"",
			typeOf: field.typeOf,
			receivingTypes: field.receivingTypes,
			receiverDataTypes: field.receiverDataTypes,
			broadcastingTypes: field.broadcastingTypes,
			refId: '',
			accessibleLayoutTypes: field.accessibleLayoutTypes,
			toolbar: [],
			entityName: entity,
			supportedQuickAddModes: field.supportedQuickAddModes,
			isQuickAddSupported: field.isQuickAddSupported
		});
		if (field.controlType == "Tabs") {

			var tab = getTreenodeinstanceWithObject({
				name: "Tab 1",
				value: "",
				required: false, //need to change
				dataType: "tab",
				controlType: "tab",
				readOnly: false,
				setting: null,
				// entityName: field.entityName
			});
			var tabSection = getTreenodeinstanceWithObject({
				required: true,
				controlType: "section",
				readOnly: false,
				tabs: [],
				setting: null,
				// entityName: field.entityName
			});

			tab.fields.push(tabSection);
			myObj.tabs.push(tab);
		}

		this.dragStart = true;
		this.selectedTreeNode = myObj;
		//(<any>event).stopPropagation();
		(<any>event).dataTransfer.setData('text/plain', '');
	}



	allowDrop(ev) {
		//console.log('ev ', ev);
		//ev['dragstart'] = false ;
		//ev['dropstart'] = false;
		// if(this.selectedField)
		// {
		// 	this.selectedField['dragstart'] = false;
		// 	this.selectedField['dropstart'] = false;
		// }
		(<any>event).stopPropagation();
		(<any>event).preventDefault();
	}


	private removeCommon(data: ITreeNode[], targetData: ITreeNode) {
		if (targetData.fields.length > 0) {
			targetData.fields.forEach(field => {
				this.entityDeatils.fields.forEach(obj => {
					if (obj.name === field.name) {
						obj.draggedItem = false;
					}
				});

				if (this.entityDeatils.activityEntity != null && this.entityDeatils.activityEntity != undefined) {
					this.entityDeatils.activityEntity.fields.forEach(obj => {
						if (field.name === obj.name) {
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
			this.entityDeatils.fields.forEach(obj => {
				if (obj.name === targetData.name) {
					obj.draggedItem = false;
				}
			});

			if (this.entityDeatils.activityEntity != null && this.entityDeatils.activityEntity != undefined) {
				this.entityDeatils.activityEntity.fields.forEach(obj => {
					if (targetData.name === obj.name) {
						obj.draggedItem = false;
					}
				});
			}

			data.splice(index, 1);
			//this.selectedTreeNode = null;
			return;
		} else {
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
									this.removeData(tab.fields, targetData);
								}
								else {
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
		}
	}

	// I handle the events from the tree component.
	public handleSelection(node: ITreeNode): void {
		this.selectedTreeNode = node;
		//console.log("this.selectedTreeNode", this.selectedTreeNode);
		(<any>event).dataTransfer.setData('text/plain', '');
		//console.groupEnd();
	}

	public handleDropEvent(node: ITreeNode): void {
		this.dragStart = false;
		node['dragover'] = false;
		event.stopPropagation();
		//console.log('this.selectedTreeNode ', this.selectedTreeNode);
		//console.log('node ', node);
		//console.log('this.selectedField ', this.selectedField)
		this.sharedTreeService.addFieldToLayout(this.selectedTreeNode, node);
	}

	public handleCloseEvent(node: ITreeNode): void {
		//console.log('handleCloseEvent called');	
		this.deleteComponent(node);
	}
	public handleEditEvent(node: ITreeNode): void {

		//console.log('handleEditEvent called');		

		if(node.entityName === null || node.entityName === undefined)
		{
			node.entityName = this.entityname;
		}
		this.configureSettings(node);
	}
	public handleAddEvent(node: ITreeNode): void {
		//console.log('handleAddEvent called');
		this.configureAdd(node);
	}

	private deleteComponent(node: ITreeNode) {

		this.globalResourceService.openDeleteModal.emit()
		this.globalResourceService.notifyConfirmationDelete.subscribe(x => {
			this.removeData(this.tree.fields, node);
				this.removeCommon(this.tree.fields, node)
		   
		  })








		// swal({
		// 	title: this.getResourceValue("common_message_areyousure"),
		// 	text: this.getResourceValue("common_message_youwontbeabletorevertthis"),
		// 	type: 'warning',
		// 	showCancelButton: true,
		// 	confirmButtonColor: '#3085d6',
		// 	cancelButtonColor: '#d33',
		// 	confirmButtonText: this.getResourceValue('common_message_yesdeleteit'),
		// 	showLoaderOnConfirm: true,
		// }).then((willDelete) => {
		// 	if (willDelete.value) {
		// 		this.removeData(this.tree.fields, node);
		// 		this.removeCommon(this.tree.fields, node)
		// 	} else {
		// 		//write the code for cancel click
		// 	}
		// });
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
				node.name = receivedEntry.name;
				node.setting = receivedEntry.setting;
				//node.changeRef.detectChanges();
				// node.setting.showHeader = receivedEntry.setting.showHeader;
				// node.setting.columnWidth = receivedEntry.setting.columnWidth;
				node.selectedView = receivedEntry.selectedView;
				node.selectedFormOrList= receivedEntry.selectedFormOrList;
				node.validators = receivedEntry.validators;
				node.tabs = receivedEntry.tabs;
				modalRef.close();
			});
		}
		else {
			modalRef.close();
			this.toster.showWarning(this.getResourceValue("metadata_task_settingsunavailable_warning_message"));
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
			var section = getTreenodeinstanceWithObject({
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

	private setdeactivefields(node: ITreeNode) {
		//console.log('\n\n\nthis.entityDeatils ', this.entityDeatils);
		//console.log('node ', node);
		// console.log('this.entityDeatils.detailEntities ', this.entityDeatils.detailEntities);
		this.entityDeatils.fields.forEach(obj => {
			if (node.name === obj.name) {
				obj.draggedItem = true;
			}
		});

		if (this.entityDeatils.detailEntities.length > 0) {
			this.entityDeatils.detailEntities.forEach(obj => {
				if (node.name === obj.name) {
					obj.draggedItem = true;
				}
			});
		}

		if (this.entityDeatils.activityEntity != null && this.entityDeatils.activityEntity != undefined) {
			this.entityDeatils.activityEntity.fields.forEach(obj => {
				if (node.name === obj.name) {
					obj.draggedItem = true;
				}
			});
		}

		if (node.tabs != null && node.tabs.length > 0) {
			node.tabs.forEach(tab => {

				this.entityDeatils.fields.forEach(obj => {
					if (tab.name === obj.name) {
						obj.draggedItem = true;
					}
				});
				//
				if (this.entityDeatils.detailEntities.length > 0) {
					this.entityDeatils.detailEntities.forEach(obj => {
						if (tab.name === obj.name) {
							obj.draggedItem = true;
						}
					});
				}

				if (this.entityDeatils.activityEntity !== null && this.entityDeatils.activityEntity !== undefined) {
					this.entityDeatils.activityEntity.fields.forEach(obj => {
						if (tab.name === obj.name) {
							obj.draggedItem = true;
						}
					});

				}

				tab.fields.forEach(innerelement => {
					this.entityDeatils.fields.forEach(obj => {
						if (innerelement.name === obj.name) {
							obj.draggedItem = true;
						}
					});

					if (this.entityDeatils.detailEntities.length > 0) {
						this.entityDeatils.detailEntities.forEach(obj => {
							if (innerelement.name === obj.name) {
								obj.draggedItem = true;
							}
						});
					}

					if (this.entityDeatils.activityEntity !== null && this.entityDeatils.activityEntity !== undefined) {
						this.entityDeatils.activityEntity.fields.forEach(obj => {
							if (innerelement.name === obj.name) {
								obj.draggedItem = true;
							}
						});
					}

					innerelement.fields.forEach(nextf => {
						this.entityDeatils.fields.forEach(obj => {
							if (nextf.name === obj.name) {
								obj.draggedItem = true;
							}
						});

						if (this.entityDeatils.detailEntities.length > 0) {
							this.entityDeatils.detailEntities.forEach(obj => {
								if (nextf.name === obj.name) {
									obj.draggedItem = true;
								}
							});
						}

						if (this.entityDeatils.activityEntity !== null && this.entityDeatils.activityEntity !== undefined) {
							this.entityDeatils.activityEntity.fields.forEach(obj => {
								if (nextf.name === obj.name) {
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

					if (innerelement.tabs && innerelement.tabs.length > 0) {
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
					if (innerelement.name === obj.name) {
						obj.draggedItem = true;
					}
				});

				if (this.entityDeatils.detailEntities.length > 0) {
					this.entityDeatils.detailEntities.forEach(obj => {
						if (innerelement.name === obj.name) {
							obj.draggedItem = true;
						}
					});
				}

				if (this.entityDeatils.activityEntity !== null && this.entityDeatils.activityEntity !== undefined) {
					this.entityDeatils.activityEntity.fields.forEach(obj => {
						if (innerelement.name === obj.name) {
							obj.draggedItem = true;
						}
					});
				}

				innerelement.fields.forEach(nextf => {
					this.entityDeatils.fields.forEach(obj => {
						if (nextf.name === obj.name) {
							obj.draggedItem = true;
						}
					});

					if (this.entityDeatils.detailEntities.length > 0) {
						this.entityDeatils.detailEntities.forEach(obj => {
							if (innerelement.name === obj.name) {
								obj.draggedItem = true;
							}
						});
					}

					if (this.entityDeatils.activityEntity !== null && this.entityDeatils.activityEntity !== undefined) {
						this.entityDeatils.activityEntity.fields.forEach(obj => {
							if (nextf.name === obj.name) {
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


	public onChangeEvent(ev): void {
		for (var i = 0; i < this.addedItemToMainList.length; i++) {
			if (this.addedItemToMainList[i].type === 'task') {
				if (this.selectedTask === this.addedItemToMainList[i].name) {
					this.addedItemToMainList[i].group = ev.target.value;
				}
			}
		}
	}
	onclick(index) {
		this.selectedRelatedEntityIndex = index
	}
}
