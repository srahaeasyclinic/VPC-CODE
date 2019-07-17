import { Subject, Observable } from "rxjs";
import { Payload } from "./payload";

interface BroadcastEvent {
    key: any;
    data?: any;
  }
export class Broadcaster {
    private _eventBus: Subject<BroadcastEvent>;
  
    constructor() {
      this._eventBus = new Subject<BroadcastEvent>();
    }
  
    broadcast(key: any, data?: any) {
      this._eventBus.next({key, data});
    }
  
    on<T>(key: any): Observable<T> {
      return this._eventBus.asObservable()
        .filter(event => event.key === key)
        .map(event => <T>event.data);
    }


    //declare observable as dependency rules....
    private dependencyRules = new Subject<Payload>();
    dependencyRules$ = this.dependencyRules.asObservable();

    setDependency(value:Payload) {
      this.dependencyRules.next(value);
      console.log("dependencyRules", this.dependencyRules);
    }
  }