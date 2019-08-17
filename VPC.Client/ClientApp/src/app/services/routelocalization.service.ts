import { Injectable } from '@angular/core';
import { defaultURL } from '../bread-crumb/BreadcrumbsService';
import { ActivatedRoute, Params, Router, ActivatedRouteSnapshot } from '@angular/router';


@Injectable({
  providedIn: 'root'
})
export class RoutelocalizationService {
  pathArray: any;

  constructor(private activatedRoute: ActivatedRoute, private router: Router) { }


  //////////////////////////////////////////Public methods//////////////////////////////////////////////////////////
   getDefaultUrl(): string {
    return this.getlocalizeUrl(defaultURL);
  }
  getDefaultlanguageKey(): string {
    let languageKey: string;
    var lang = JSON.parse(localStorage.getItem('langInfo'));
    //console.log('language ',JSON.stringify(localStorage.getItem('langInfo')));
    languageKey = (lang != undefined && lang != null) ? lang.key : 'en-us';
    
    return languageKey;
  }
  
  getlocalizeUrl(url: string): string {
    
    //this.getPathArray(this.router.routerState.snapshot.root);
    //routeUrl = ;
    return  this.getDefaultlanguageKey().toLocaleLowerCase()+"/"+url;//this.pathArray;
  }

  setdefaultLanguage(language: any) {
    localStorage.setItem('langInfo', JSON.stringify(language));;
  }

//////////////////////////////////////////////////////END////////////////////////////////////////////////////////////////////////////
//////////////////////////////////////////Private Methods///////////////////////////////////////////////////////////////////////////
  // private getPathArray(route: ActivatedRouteSnapshot) {
    
  //   this.pathArray = [];

  //   if (route.routeConfig && route.routeConfig.path !== '') {
  //     this.pathArray.push(route.routeConfig.path);
  //   }

  //   if (route.firstChild) {
  //      this.pathArray =  this.pathArray.concat(this.getPathArray(route.firstChild));
  //   }
        
  // }
  
  // private replacePathArrayId() {
    
  //   return this.pathArray.map(path => {
  //     return path.replace(':language', this.getDefaultlanguage());
  //   })

  // }
//////////////////////////////////////////////////////END//////////////////////////////////////////////////////////////////////////
}
