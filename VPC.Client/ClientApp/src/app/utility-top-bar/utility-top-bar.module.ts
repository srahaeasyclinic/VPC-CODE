import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { UtilityTopBarComponent } from './utility-top-bar.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';


@NgModule({
    imports: [
      CommonModule,
      FormsModule,
      NgbModule,
      //NgbModal
  
    //   //kendo-grid 
    //   GridModule,
    //   ExcelModule,
    //   BrowserAnimationsModule,
    //   DropDownsModule,
  
  
    //   //Material module
    //   MatToolbarModule,
    //   MatCardModule,
    //   MatButtonModule,
    //   MatIconModule,
    //       MatMenuModule,
    //   MatInputModule,
    //   MatFormFieldModule,
    //   MatExpansionModule,
    ],
    exports: [
        UtilityTopBarComponent
      ],
    declarations: [
        UtilityTopBarComponent
    ]
  })
  export class UtilityTopBarModule { }