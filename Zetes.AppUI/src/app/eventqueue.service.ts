import { Injectable } from '@angular/core';
import { Subject, filter } from 'rxjs';

@Injectable({
  providedIn: 'root'
})

class EventqueueService {
  private eventQueue = new Subject<EventData>();
  constructor() { }

  onEvent(type:EventType) {
    return this.eventQueue.pipe(filter(e => e.type === type));
  }

  emitEvent(data: EventData) {
    this.eventQueue.next(data);
  }
}

enum EventType {
  LOGIN = 'login',
  LOGOUT = 'logout',
  REGISTER = 'register'
}

class EventData {
  type?: EventType;
  data: any;
}

export{ 
  EventqueueService, EventType, EventData
}

