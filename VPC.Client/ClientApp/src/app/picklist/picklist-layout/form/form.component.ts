import { Component, OnInit, NgZone, ChangeDetectorRef } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { ITreeNode } from "../../../dynamic-form-builder/tree.module";
import { fromEvent } from 'rxjs';
import { findIndex } from 'rxjs/operators';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { first } from 'rxjs/operators';
import { Observable } from 'rxjs/Observable';
import swal from 'sweetalert2';
import { NgbModal, NgbModalOptions } from '@ng-bootstrap/ng-bootstrap';
import { FormService } from "./form.service";
import { MetadataService } from "../../../meta-data/metadata.service";
import { Entities } from '../../../model/entities';
import { FieldModel } from '../../../model/fieldmodel';
import { getTreenodeinstanceWithObject, generateRandomNo } from '../../../model/treeNode';

import { predifinedType } from 'src/app/dynamic-form-builder/helper/utils';
import { MODALS } from 'src/app/dynamic-form-builder/tree.config'
import { PicklistLayoutService } from "../../picklist-layout/picklist-layout.service";
import { LayoutModel } from '../../../model/layoutmodel';
import { TosterService } from '../../../services/toster.service';
import { ResourceService } from '../../../services/resource.service';
import { HostListener } from "@angular/core";
import { CommonService } from 'src/app/services/common.service';

import { Target } from "../../../model/target";
import { GlobalResourceService } from '../../../global-resource/global-resource.service';
import { Resource } from '../../../model/resource';




@Component({
	selector: 'app-form',
	templateUrl: './form.component.html',
	styleUrls: ['./form.component.css']
})
export class FormComponent implements OnInit {


	public tree: ITreeNode;
	public selectedTreeNode: ITreeNode | null;
	private dropType: string = "section";
	public layoutInfo: LayoutModel = new LayoutModel();
	public layoutId: string;
	//private layoutName: string = '';
	public entityDeatils: Entities;
	private selectedField: FieldModel;
	public predefinedData: any[] = predifinedType();
	private dragStart: boolean = false;
	public isConfigToggle: boolean = false;
	public screenHeight: number;
	modalReference: any;
	resource: Resource;
	public displayRule: any;

	private metadatafield: any;
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
	config: { allowConfiguration: boolean; displaySortColumn: boolean; direction: boolean; maxResult: boolean; groupBy: boolean; clickableColumn: boolean; };
	selectedLayout: any = {}
	pageInfo =
		{
			config:
			{
				allowConfiguration: false, displaySortColumn: false, direction: false, maxResult: false,
				groupBy: false, clickableColumn: false
			}
			, selectedLayout: {},
			addedItemToMainList: [],
			fieldSource: [],
			type: ""
		}
	fieldSource: any=[];
	// public resourceData: Resource;
	constructor(
		private router: Router,
		private activatedRoute: ActivatedRoute,
		private metadataService: MetadataService,
		private toster: TosterService,
		private layoutService: PicklistLayoutService,
		private zone: NgZone,
		private cdr: ChangeDetectorRef,
		private modalService: NgbModal,
		private resourceService: ResourceService,
		private commonService: CommonService,
		private globalResourceService: GlobalResourceService,
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
		this.resource = this.globalResourceService.getGlobalResources();
		this.selectedTreeNode = null;
		this.addedItemToMainList = [];
		this.toolbarSource = [];

		// this.getResource();

		this.layoutInfo = this.activatedRoute.snapshot.data['layoutDetails'];
		//console.log(this.layoutInfo);

		// if (this.layoutInfo != null && this.layoutInfo.formLayoutDetails != null) {
		// 	this.tree = this.layoutInfo.formLayoutDetails;
		// } else {
		// 	this.tree = getTreenodeinstanceWithObject({
		// 		name: " ",
		// 		value: "",
		// 		required: true,
		// 		dataType: "",
		// 		fields: [],
		// 		controlType: "section",
		// 		readOnly: false,
		// 		decimalPrecision: null,
		// 		defaultValue: "",
		// 		properties: "",
		// 		tabs: [],
		// 		setting: { columnWidth: 12, showHeader: true },
		// 		validators: [],
		// 		selectedView: "",
		// 		typeOf: "",
		// 		receivingTypes: [],
		// 		receiverDataTypes: [],
		// 		broadcastingTypes: [],
		// 		refId: '',
		// 		accessibleLayoutTypes: [],
		// 		toolbar: [],
		// 		entityName: ""
		// 	});
		// }
		// this.activatedRoute.params.subscribe((params: Params) => {
		// 	this.layoutId = params['id'];
		// 	this.entityname = params['name'];
		// });

		this.activatedRoute.params.subscribe((params: Params) => {
			this.layoutId = params['id'];
		});

		this.activatedRoute.parent.parent.params.subscribe((params: Params) => {
			this.entityname = params['picklistName'];
		});


		this.getMetadataFieldsByName(this.entityname);
		this.selectedTreeNode = null;
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


	}




	public updateLayout() {
		if (this.addedItemToMainList && this.addedItemToMainList.length > 0) {
			this.tree.toolbar = this.addedItemToMainList;
		}

		this.layoutInfo.formLayoutDetails = this.tree;

		this.layoutService.updateLayout(this.layoutInfo, this.entityname, this.layoutId).subscribe(result => {
			//console.log(result);
			this.toster.showSuccess(this.getResourceValue("metadata_operation_save_success_message"));
		});
	}

	private getMetadataFieldsByName(name) {
		this.metadataService.getMetadataByName(name)
			.pipe(first())
			.subscribe(
				data => {
					if (data) {
						this.entityDeatils = data;
						//Filter by accessblity attributes 
						//this.entityDeatils.fields = data.fields.filter(f => (f.accessibleLayoutTypes === undefined||f.accessibleLayoutTypes ===null ) || f.accessibleLayoutTypes.find(e => e === 2));

						//this.entityDeatils.fields = data.fields.filter(f => (f.accessibleLayoutTypes && f.accessibleLayoutTypes.find(e => e === 2)));

						this.entityDeatils.fields = data.fields.filter(f => (f.accessibleLayoutTypes && f.accessibleLayoutTypes.find(e =>
							(e.toString().split('').length == 1 && e.toString().split('')[0] === this.layoutInfo.layoutType.toString())
							||
							(
								e.toString().split('').length > 1 && e.toString().split('')[0] === this.layoutInfo.layoutType.toString() && e.toString().split('')[1] === this.layoutInfo.context.toString()
							)
						)));

						//end

						this.metadatafield = data;

						if (this.layoutInfo != null && this.layoutInfo.formLayoutDetails != null) {
							this.tree = this.layoutInfo.formLayoutDetails;
						} else {
							this.tree = getTreenodeinstanceWithObject({
								name: " ",
								value: "",
								required: true,
								dataType: "",
								fields: [],
								controlType: "section",
								readOnly: false,
								decimalPrecision: null,
								defaultValue: "",
								properties: "",
								tabs: [],
								setting: { columnWidth: 12, showHeader: true },
								validators: [],
								selectedView: "",
								typeOf: "",
								receivingTypes: [],
								receiverDataTypes: [],
								broadcastingTypes: [],
								refId: '',
								accessibleLayoutTypes: [],
								toolbar: [],
								entityName: ""
							});

							// if (this.metadatafield != null) {
							// 	if (this.metadatafield.fields != undefined) {
							// 		this.metadatafield.fields.forEach(item => {
							// 			if (item.controlType.toLocaleLowerCase() !== 'label' && item.required == true && (item.accessibleLayoutTypes === undefined || item.accessibleLayoutTypes.find(x => x === 2))) {
							// 				this.addMandatoryFieldsAndActivityEntity(item, this.metadatafield.name);
							// 			}
							// 		});
							// 	}
							// }

							if (this.entityDeatils != null) {
								if (this.entityDeatils.fields != undefined) {
									this.entityDeatils.fields.forEach(item => {
										//if (item.controlType.toLocaleLowerCase() !== 'label' && item.required == true && (item.accessibleLayoutTypes === undefined || item.accessibleLayoutTypes.find(x => x === 2))) {
										if (item.controlType.toLocaleLowerCase() !== 'label' && item.required == true && (item.accessibleLayoutTypes === undefined || (item.accessibleLayoutTypes && item.accessibleLayoutTypes.find(e =>
											(e.toString().split('').length == 1 && e.toString().split('')[0] === this.layoutInfo.layoutType.toString())
											||
											(
												e.toString().split('').length > 1 && e.toString().split('')[0] === this.layoutInfo.layoutType.toString() && e.toString().split('')[1] === this.layoutInfo.context.toString()
											)
										)))) {
											this.addMandatoryFieldsAndActivityEntity(item, this.entityDeatils.name);
										}
									});
								}
							}

							this.updateLayout();
						}

						//validate whether field is already added
						this.tree.fields.forEach(item => {
							// this.entityDeatils.fields.forEach(obj => {
							// 	if (item.name === obj.name) {
							// 		obj.draggedItem = true;
							// 	}
							// });
							this.setdeactivefields(item);

						});
						//console.log(this.entityDeatils);
						this.setactivityOnMetadataFields();

						// if (this.metadatafield != null) {
						// 	if (this.metadatafield.fields != undefined) {
						// 		this.metadatafield.fields.forEach(item => {
						// 			if (item.controlType.toLocaleLowerCase() !== 'label' && item.required == true && (item.accessibleLayoutTypes === undefined || item.accessibleLayoutTypes.find(x => x === 2))) {
						// 				this.addMandatoryFieldsAndActivityEntity(item, this.metadatafield.name);
						// 			}
						// 		});
						// 	}
						// }

						this.setData(data)
					}
				},
				error => {
					console.log(error);
				});
	}

	setData(data) {

		this.selectedLayout = this.layoutInfo.formLayoutDetails;
		this.addedItemToMainList = this.selectedLayout.toolbar ? this.selectedLayout.toolbar : [];
		this.manipulateToolBar(data)
		this.config = { allowConfiguration: true, displaySortColumn: false, direction: false, maxResult: false, groupBy: false, clickableColumn: false }
		this.setFieldSourceProperties()
		this.setPageInfo()
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
	public fieldDragStartEvent(field: any) {

		//console.log('ss' + JSON.stringify(field));

		this.tree.fields.forEach(item => {
			this.entityDeatils.fields.forEach(obj => {
				if (item.name === obj.name) {
					obj.draggedItem = true;
				}
			});

		});
		event.stopPropagation();
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
			refId: '',
			accessibleLayoutTypes: field.accessibleLayoutTypes,
			toolbar: [],
			entityName: ""

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
				entityName: ""
			});
			var tabSection = getTreenodeinstanceWithObject({
				required: true,
				controlType: "section",
				readOnly: false,
				tabs: [],
				setting: { columnWidth: 12, showHeader: true },
				entityName: ""
			});

			tab.fields.push(tabSection);
			myObj.tabs.push(tab);
		}

		this.dragStart = true;
		this.selectedTreeNode = myObj;
	}
	private swapfield(data: ITreeNode[], sourceData: ITreeNode, targetdata: ITreeNode) {

		var sourceindex = data.findIndex(t => t.refId == sourceData.refId);
		var tragetindex = data.findIndex(t => t.refId == targetdata.refId);

		if (sourceindex >= 0 && tragetindex >= 0) {

			let sourcedata = data.find(t => t.refId == sourceData.refId);

			data[sourceindex] = data[tragetindex];
			data[tragetindex] = sourcedata;

			//console.log('treenodeswap'+JSON.stringify(data));

			return;
		} else {

			data.forEach(element => {

				if (sourceData.controlType === "custom") {
					element.tabs.forEach(tab => {
						tab.fields.forEach(innerelement => {
							innerelement.fields.forEach(customelement => {
								if (customelement.name === sourceData.name) {
									this.swapfield(innerelement.fields, sourceData, targetdata);
								}
							});
						});
					});
				}
				if (element.controlType.toLowerCase() === "tabs" || element.controlType.toLowerCase() === "tab") {

					element.tabs.forEach(tab => {
						tab.fields.forEach(innerelement => {
							innerelement.fields.forEach(customelement => {
								if (customelement.name === sourceData.name) {
									this.swapfield(innerelement.fields, sourceData, targetdata);
								}
							});
						});
					});
				}
				else {

					if (element.fields) {
						this.swapfield(element.fields, sourceData, targetdata);
					}
				}

			});
		}
	}
	private removeData(data: ITreeNode[], targetData: ITreeNode) {
		// var index = data.findIndex(t => t.name == targetData.name);
		// if (index >= 0) {
		// 	this.entityDeatils.fields.forEach(obj => {
		// 		if (obj.name === targetData.name) {
		// 			obj.draggedItem = false;
		// 		}
		// 	});
		// 	data.splice(index, 1);
		// 	return;
		// } else {
		// 	data.forEach(element => {
		// 		if (element.fields) {
		// 			this.removeData(element.fields, targetData);
		// 		}
		// 	});
		// }
		var index = data.findIndex(t => t.refId == targetData.refId);
		// console.log('data- '+JSON.stringify(data));
		// console.log('targetData- '+JSON.stringify(targetData));
		if (index >= 0) {
			//console.log('if (index >= 0)');
			this.entityDeatils.fields.forEach(obj => {
				//console.log('if (index >= 0)'+obj.name === targetData.name);
				if (obj.name === targetData.name) {
					obj.draggedItem = false;
				}
			});
			data.splice(index, 1);
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
				if (element.controlType.toLowerCase() === "tabs" || element.controlType.toLowerCase() === "tab") {
					//console.log(JSON.stringify(element.tabs));
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


				else {
					if (element.fields) {
						this.removeData(element.fields, targetData);
					}
				}

			});
		}
	}

	// I handle the events from the tree component.
	public handleSelection(node: ITreeNode): void {
		this.selectedTreeNode = node;
		//console.groupEnd();
	}

	// public handleDropEvent(node: TreeNode): void {
	// 	event.stopPropagation();
	// 	if (node != null && node.controlType.toLocaleLowerCase() == this.dropType.toLocaleLowerCase()) {
	// 		node.fields.push(this.selectedTreeNode);
	// 		if (!this.dragStart) {
	// 			this.removeData(this.tree.fields, this.selectedTreeNode);
	// 		}			
	// 	}
	// 	this.dragStart = false;
	// }

	public handleDropEvent(node: ITreeNode): void {
		var errormessage = "";
		event.stopPropagation();
		if (node != null && node.controlType && node.controlType.toLocaleLowerCase() == this.dropType.toLocaleLowerCase()) {
			if (node.fields !== null && node.fields.length > 0) {

				node.fields.forEach(item => {
					if (item.name === this.selectedTreeNode.name && this.selectedTreeNode.controlType !== "Section") {
						errormessage += this.selectedTreeNode.name + this.getResourceValue("metadata_operation_alert_message");
					}
					this.entityDeatils.fields.forEach(obj => {
						if (item.name === obj.name) {
							obj.draggedItem = true;
						}
					});
				});
			}
			if (errormessage !== "") {
				this.toster.showError(errormessage);
			}
			if (!this.dragStart) {
				this.removeData(this.tree.fields, this.selectedTreeNode);
			}
			else {

				node.fields.push(this.selectedTreeNode);
			}

		}
		else if (node != null && node.controlType && node.controlType.toLocaleLowerCase() != this.dropType.toLocaleLowerCase()) {
			if (!this.dragStart) {
				this.swapfield(this.tree.fields, this.selectedTreeNode, node);
			}
		}

		this.dragStart = false;
	}





	public handleCloseEvent(node: ITreeNode): void {
		this.deleteComponent(node);
	}
	public handleEditEvent(node: ITreeNode): void {
		this.configureSettings(node);
	}
	public handleAddEvent(node: ITreeNode): void {
		this.configureAdd(node);
	}

	private deleteComponent(node: ITreeNode) {

		this.globalResourceService.openDeleteModal.emit()

		this.globalResourceService.notifyConfirmationDelete.subscribe(x => {
			this.removeData(this.tree.fields, node);
		   
		  })



		// swal({
		// 	title: this.getResourceValue("common_message_areyousure"),
		// 	text: this.getResourceValue("common_message_youwontbeabletorevertthis"),
		// 	type: "warning",
		// 	showCancelButton: true,
		// 	confirmButtonColor: '#3085d6',
		// 	cancelButtonColor: '#d33',
		// 	confirmButtonText: this.getResourceValue("common_message_yesdeleteit"),
		// 	showLoaderOnConfirm: true,
		// }).then((willDelete) => {
		// 	if (willDelete.value) {
		// 		this.removeData(this.tree.fields, node);
		// 		//this.cdr.detectChanges();
		// 		// this.zone.run(() => {
		// 		// 	this.removeData(this.tree.fields, node);
		// 		// });
		// 	} else {
		// 		//write the code for cancel click
		// 	}
		// });
	}

	private configureSettings(node: ITreeNode) {
		let ngbModalOptions: NgbModalOptions = {
			backdrop: 'static',
			keyboard: false
		};
		const modalRef = this.modalService.open(MODALS[node.controlType.toLowerCase()], ngbModalOptions);
		let nodeObj = JSON.parse(JSON.stringify(node))

		if (modalRef.componentInstance !== undefined) {
			modalRef.componentInstance.node = nodeObj;
			modalRef.componentInstance.saveEvent.subscribe((receivedEntry) => {
				node.name = receivedEntry.name;
				node.setting.showHeader = receivedEntry.setting.showHeader;
				node.setting.columnWidth = receivedEntry.setting.columnWidth;
				node.validators = receivedEntry.validators;
				node.tabs = receivedEntry.tabs;
				modalRef.close();
			});
		}
		else {
			modalRef.close();
			this.toster.showWarning(this.getResourceValue('metadata_operation_warning_message'));
		}
	}

	private configureAdd(node: ITreeNode) {
		let ngbModalOptions: NgbModalOptions = {
			backdrop: 'static',
			keyboard: false
		};
		const modalRef = this.modalService.open(MODALS['add'], ngbModalOptions);
		let nodeObj = JSON.parse(JSON.stringify(node))
		modalRef.componentInstance.node = nodeObj;
		modalRef.componentInstance.saveEvent.subscribe((receivedEntry) => {
			// node.name = receivedEntry.name;
			// node.settings.showHeader = receivedEntry.settings.showHeader;
			// node.settings.columnWidth = receivedEntry.settings.columnWidth;
			var section = getTreenodeinstanceWithObject({
				name: receivedEntry.name,
				value: "",
				required: false,
				dataType: "",
				fields: [],
				controlType: "section",
				readOnly: false,
				decimalPrecision: null,
				defaultValue: "",
				properties: "",
				tabs: [],
				setting:
				{
					columnWidth: receivedEntry.setting.columnWidth,
					showHeader: receivedEntry.setting.showHeader
				},
				validators: receivedEntry.validators,
				selectedView: "",
				typeOf: "",
				receivingTypes: [],
				receiverDataTypes: [],
				broadcastingTypes: [],
				refId: '',
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
			//console.log("entityDeatils by );
			//console.log('First field'+innerelement.name === obj.name);
			if (node.name === obj.name) {
				//console.log("entityDeatils by name", JSON.stringify(obj));
				obj.draggedItem = true;
			}
		});

		if (node.tabs != null && node.tabs.length > 0) {
			node.tabs.forEach(tab => {

				this.entityDeatils.fields.forEach(obj => {
					//console.log("entityDeatils by );
					//console.log('First field'+innerelement.name === obj.name);
					if (tab.name === obj.name) {
						//console.log("entityDeatils by name", JSON.stringify(obj));
						obj.draggedItem = true;
					}
				});
				//console.log("tab by ", JSON.stringify(tab));
				tab.fields.forEach(innerelement => {
					//console.log("innerelement by ", JSON.stringify(innerelement));
					this.entityDeatils.fields.forEach(obj => {
						//console.log("entityDeatils by );
						//console.log('First field'+innerelement.name === obj.name);
						if (innerelement.name === obj.name) {
							//console.log("entityDeatils by name", JSON.stringify(obj));
							obj.draggedItem = true;
						}
					});
					innerelement.fields.forEach(nextf => {
						this.entityDeatils.fields.forEach(obj => {
							//console.log("entityDeatils by );
							//console.log('Second field'+nextf.name === obj.name);
							if (nextf.name === obj.name) {
								//console.log("entityDeatils by name", JSON.stringify(obj));
								obj.draggedItem = true;
							}
						});
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
					//console.log("entityDeatils by );
					//console.log('Second field'+nextf.name === obj.name);
					if (innerelement.name === obj.name) {
						//console.log("entityDeatils by name", JSON.stringify(obj));
						obj.draggedItem = true;
					}
				});
				//console.log("innerelement by ", JSON.stringify(innerelement));

				innerelement.fields.forEach(nextf => {
					this.entityDeatils.fields.forEach(obj => {
						//console.log("entityDeatils by );
						//console.log('Second field'+nextf.name === obj.name);
						if (nextf.name === obj.name) {
							//console.log("entityDeatils by name", JSON.stringify(obj));
							obj.draggedItem = true;
						}
					});
				});

				if (innerelement.fields.length > 0) {
					this.setdeactivefields(innerelement);

				}
			});
		}



	}

	public configToggle(): void {
		this.isConfigToggle = !this.isConfigToggle;
	}


	private setidExistingfields(node: ITreeNode) {
		node.refId = node.refId || generateRandomNo();
		if (node.fields) {
			node.fields.forEach(item => {
				this.setidExistingfields(item);
				if (item.fields) {
					item.fields.forEach(item3 => {
						this.setidExistingfields(item3);
					});
				}
			});
		}
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

			// if (this.addedItemToMainList !== null && this.addedItemToMainList !== undefined) {
			// 	for (var i = 0; i < this.addedItemToMainList.length; i++) {
			// 		for (var j = 0; j < this.toolbarSource.length; j++) {
			// 			if (this.addedItemToMainList[i].name === this.toolbarSource[j].name) {
			// 				this.toolbarSource[j].isRowSelected = true;
			// 				this.toolbarSource[j].isAdded = true;
			// 			}
			// 		}
			// 	}
			// }



		}

	}

	public generateResourceName(word: string) {
		if (!word) return word;
		return word[0].toLowerCase() + word.substr(1);
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

	public onChangeEvent(ev): void {
		for (var i = 0; i < this.addedItemToMainList.length; i++) {
			if (this.addedItemToMainList[i].type === 'task') {
				if (this.selectedTask === this.addedItemToMainList[i].name) {
					this.addedItemToMainList[i].group = ev.target.value;
				}
			}
		}
	}

	getResourceValue(key) {
		return this.globalResourceService.getResourceValueByKey(key);
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

}


