import { TestBed } from '@angular/core/testing';

import { PhotostockService } from './photostock.service';

describe('PhotostockService', () => {
  let service: PhotostockService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PhotostockService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
