import { TestBed } from '@angular/core/testing';

import { EventqueueService } from './eventqueue.service';

describe('EventqueueService', () => {
  let service: EventqueueService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(EventqueueService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
