import { TestBed } from '@angular/core/testing';

import { ChiefsService } from '../chiefs.service';

describe('ChiefsService', () => {
  let service: ChiefsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ChiefsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
