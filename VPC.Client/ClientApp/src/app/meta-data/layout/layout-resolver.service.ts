import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Resolve, ActivatedRoute, Params, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { Observable } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { environment } from '../../../environments/environment';
@Injectable()
export class LayoutResolver implements Resolve<any> {

  constructor(
    private http: HttpClient,
    private activatedRoute: ActivatedRoute
  ) { }

  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<any> {
   
    let id: string;
    let name: string;

    let layout: string = '/api/meta-data';
    id = route.params['id'];
    name = route.params['name'];

    let query: string = '?&pagingParameter.pageNumber=1' + '&pagingParameter.pageSize=10';
    var url = `${environment.apiUrl}` + layout + '/' + name + '/layouts/' + id;
    return this.http.get(url).pipe(
      map( (dataFromApi) => dataFromApi ),
      catchError( (err) => Observable.throw(err.json().error) )
    )
  }
}


