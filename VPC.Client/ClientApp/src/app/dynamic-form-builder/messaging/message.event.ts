import { Injectable } from "@angular/core";
import { Broadcaster } from "./broadcaster";
import { Observable } from "rxjs";
import { Payload } from "./payload";

@Injectable()
export class MessageEvent {
  constructor(private broadcaster: Broadcaster) {}

  fire(data: Payload): void {
    this.broadcaster.broadcast(MessageEvent, data);
  }

  on(): Observable<Payload> {
    return this.broadcaster.on<Payload>(MessageEvent);
  }
}