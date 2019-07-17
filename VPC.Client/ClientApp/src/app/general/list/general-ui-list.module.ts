import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { GeneralUiListComponent } from './general-ui-list.component';

//Kendo grid ---- Kendo UI for Angular provides themes that you can use to style your application
import { GridModule, ExcelModule } from '@progress/kendo-angular-grid';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { DropDownsModule } from '@progress/kendo-angular-dropdowns';

//

//Material module -------- Angular Material is a UI component library for Angular developers
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatMenuModule } from '@angular/material/menu';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatExpansionModule } from '@angular/material/expansion';
import { BrowserModule } from '@angular/platform-browser';

import { CommonGridDataComponent } from 'src/app/common-grid-data/common-grid-data.component';




@NgModule({
	imports: [
	BrowserModule,

        
       // CommonModule,
        //kendo-grid 
        GridModule,
        ExcelModule,
        BrowserAnimationsModule,
        DropDownsModule,
        //Material module
        MatToolbarModule,
        MatCardModule,
        MatButtonModule,
        MatIconModule,
        MatMenuModule,
        MatInputModule,
        MatFormFieldModule,
        MatExpansionModule,

	],
	exports: [
		GeneralUiListComponent
	],
	declarations: [
                GeneralUiListComponent,
                CommonGridDataComponent
	],
	providers: [
		
	]
})
export class GeneralUiListModule {
	// ...
}