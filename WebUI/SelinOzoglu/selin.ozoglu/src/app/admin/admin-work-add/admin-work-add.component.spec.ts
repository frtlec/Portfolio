import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminWorkAddComponent } from './admin-work-add.component';

describe('AdminWorkAddComponent', () => {
  let component: AdminWorkAddComponent;
  let fixture: ComponentFixture<AdminWorkAddComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AdminWorkAddComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AdminWorkAddComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
