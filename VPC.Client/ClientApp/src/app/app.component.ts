import { Component, OnInit, ViewChild } from '@angular/core';
import { Router, NavigationEnd, NavigationStart, NavigationError,Event } from '@angular/router';
import { Spinkit } from 'ng-http-loader';
import { MenuItem } from './model/menuItem';
import { NewMenuItem } from './model/menuItem';
import { first } from 'rxjs/operators';
import { TosterService } from './services/toster.service';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { MenuService } from './services/menu.service';
import { DeletepopupComponent } from './deletepopup/deletepopup.component';
import { GlobalResourceService } from './global-resource/global-resource.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {

  title = 'app';
  @ViewChild(DeletepopupComponent) deletepopupComponent:DeletepopupComponent;
  public spinkit = Spinkit;
  public leftmenuItems: Array<MenuItem>;
  public leftmenuItemsNew: any;
  group: string;
  isMenuopen: boolean = false;
  

  public menus:Array<NewMenuItem>;
  public currentMenu:NewMenuItem;

  constructor( 
    private router: Router,
    private toster:TosterService,
    private modalService: NgbModal,
    public menuService: MenuService,
    private globalResourceService: GlobalResourceService,) {   
    
       
    router.events.subscribe( (event: Event) => {

      if (event instanceof NavigationStart) {
          // Show loading indicator
      
      }

      if (event instanceof NavigationEnd) {
         this.toster.dismissAllToastr();
         this.modalService.dismissAll();
      }

      if (event instanceof NavigationError) {
          // Hide loading indicator

          // Present error to user
          console.log(event.error);
      }
  });
}
  ngOnInit(): void {
    this.globalResourceService.openDeleteModal.subscribe(x=>{
      this.deletepopupComponent.openModal();
    })

  }

    // this.menuService.getAllMenu().subscribe(result => {
    //   if (result) {
    //     this.menus = result;
    //     this.currentMenu = this.menus[0];
    //     console.log("menu get called from app component...", this.menus);
    //   }
    // });
  


  // topMenu(value): void {
  //   console.log('top menu called');
  //   if (value) {
  //     this.leftmenuItems = this.menuService.getMenuItems(value.groupName);  

  //     this.group = value.groupName;
  //     if (this.leftmenuItems && this.leftmenuItems.length > 0) {
  //       this.router.navigate([this.leftmenuItems[0].path]);
  //       this.isMenuopen = true;
  //     } else {
  //       this.router.navigate(['notfound']);
        
  //     }
  //   }

  // }

  // topMenuNew(value): void {
  //   if (value) {
  //     this.getMenuList(value.groupName);
  //   }
  // }

  // private getMenuList(groupName:string): void {
  //   this.menuService.getMenuItemsNew(groupName)
  //     .pipe(first())
  //     .subscribe(
  //       data => {
  //         if (data) {
  //           this.leftmenuItemsNew = data; 
  //           this.group = groupName;

  //           if (this.leftmenuItemsNew && this.leftmenuItemsNew.length > 0) {
  //             //this.router.navigate([this.leftmenuItems[0].path]);
  //             this.isMenuopen = true;
  //           } else {
  //             this.router.navigate(['notfound']);
              
  //           }

  //         }
  //       },
  //       error => {
  //         console.log(error)
  //       }
  //     );
  // }


}
