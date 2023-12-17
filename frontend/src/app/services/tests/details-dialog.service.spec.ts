import { TestBed } from '@angular/core/testing';

import { DetailsDialogService } from '../details-dialog.service';

describe('DetailsDialogService', () => {
  let service: DetailsDialogService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(DetailsDialogService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
