// Import the core angular services.
import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { MatTabsModule, MatInputModule, MatDatepickerModule } from '@angular/material';
import {MatIconModule} from '@angular/material/icon';
import {MatMenuModule} from '@angular/material/menu';
import {MatButtonModule} from '@angular/material/button';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { NgxSmoothDnDModule } from 'ngx-smooth-dnd';

//RichTextbox module
//import { AngularEditorModule } from '@kolkov/angular-editor';
import { CKEditorModule } from 'ng2-ckeditor';
//end
// import {DragAndDropModule } from 'angular-draggable-droppable';
// Import the application components and services.
import { TreeComponent } from "./tree.component";
import { TreeNodeComponent } from "./tree-node.component";
// Export the module data structures.
//export { TreeNode } from "../dynamic-form-builder/tree.component"; 
export {ITreeNode} from '../model/treeNode'
// ----------------------------------------------------------------------------------- //

import { TextBoxComponent } from './atoms/textbox';
import { TextAreaComponent } from './atoms/textarea';
import { CalanderComponent } from './atoms/calander';
import { DropDownComponent } from './atoms/dropdown';
import {LinkComponent} from './atoms/link';
import { FileComponent } from './atoms/file';
import { CheckBoxComponent } from './atoms/checkbox';
import { RadioComponent } from './atoms/radio';
import { TreeService } from "./service/tree.service";
import { CustomComponent } from './atoms/custom';
import { GridModule, ExcelModule } from '@progress/kendo-angular-grid';

// import { RichtextBoxComponent } from './atoms/richtextbox';

import { GeneralUiListModule } from '../general/list/general-ui-list.module';
import { UtilityTopBarModule } from '../utility-top-bar/utility-top-bar.module';


//multiselect dropdown
import { NgMultiSelectDropDownModule } from 'ng-multiselect-dropdown';
import { MultiSelectDropDownComponent } from './atoms/multiselect-dropdown';
import { DateFormatPipe } from "./filter/customdate-format.pipe";
import { Broadcaster } from "./messaging/broadcaster";
import { MessageEvent } from "./messaging/message.event";
import { RichtextboxComponent } from './atoms/richtextbox//richtextbox.component';
import { HierarchyDropDownComponent} from './atoms/hierarchy-dropdown';
import { OrderModule } from 'ngx-order-pipe';
import { SharedTreeService } from "./service/sharedtree.service";

@NgModule({
	imports: [
		BrowserModule,
		FormsModule,
		CommonModule,
		NgxSmoothDnDModule,
		MatTabsModule,
		MatIconModule,
		MatMenuModule,
		   //richtextbox module
	   // AngularEditorModule,
		CKEditorModule,
		MatButtonModule,
		GridModule,
		NgMultiSelectDropDownModule,
		
		UtilityTopBarModule,
		GeneralUiListModule,
		MatInputModule,
		MatDatepickerModule,
		OrderModule,
	],
	exports: [
		TreeComponent,
		MatInputModule,
		MatDatepickerModule
	],
	declarations: [
		TreeComponent,
		TreeNodeComponent,
		TextBoxComponent,
		TextAreaComponent,
		CalanderComponent,
		DropDownComponent,
		LinkComponent,
		MultiSelectDropDownComponent,
		CheckBoxComponent,
		FileComponent,
		RadioComponent,
		CustomComponent,
		// RichtextBoxComponent,
		DateFormatPipe,
		RichtextboxComponent,
		HierarchyDropDownComponent
	],
	providers: [
		TreeService,
		SharedTreeService,
		Broadcaster,
		MessageEvent
	]
})
export class TreeModule {
	// ...
}
