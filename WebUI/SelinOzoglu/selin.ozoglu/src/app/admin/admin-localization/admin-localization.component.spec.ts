import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminLocalizationComponent } from './admin-localization.component';

describe('AdminLocalizationComponent', () => {
  let component: AdminLocalizationComponent;
  let fixture: ComponentFixture<AdminLocalizationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AdminLocalizationComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AdminLocalizationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
