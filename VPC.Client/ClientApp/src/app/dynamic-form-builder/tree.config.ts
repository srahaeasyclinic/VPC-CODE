import { Component } from '@angular/core';
import {CheckboxSettingComponent} from './settings/checkbox-setting.component'
import { TextboxSettingComponent } from './settings/textbox-setting.component';
import { TabSettingComponent } from './settings/tab-setting.component';
import { SectionSettingComponent } from './settings/section-setting.component';
import { RadioSettingComponent } from './settings/radio-setting.component';
import { FileSettingComponent } from './settings/file-setting.component';
import { DropdownSettingComponent } from './settings/dropdown-setting.component';
import { AddSettingComponent } from './settings/add-setting.component';
import { CustomSettingComponent } from './settings/custom-setting.component';
import { DetailEntityComponent } from './settings/custom/detailentity-setting.component';
import { MultiSelectDropdownSettingComponent } from './settings/multiselectdropdown-setting.component';
import { RichtextboxSettingComponent} from './settings/richtextbox-setting.component';

export const MODALS = {
    checkbox:CheckboxSettingComponent,
    dropdown:DropdownSettingComponent,
    file:FileSettingComponent,
    radio:RadioSettingComponent,
    section:SectionSettingComponent,
    tabs:TabSettingComponent,
    textbox:TextboxSettingComponent,
    text:TextboxSettingComponent,
    add: AddSettingComponent,
    richtext:RichtextboxSettingComponent,
    custom:CustomSettingComponent,
    custom_detailEntity:DetailEntityComponent,
    multiselectdropdown:MultiSelectDropdownSettingComponent
  };

