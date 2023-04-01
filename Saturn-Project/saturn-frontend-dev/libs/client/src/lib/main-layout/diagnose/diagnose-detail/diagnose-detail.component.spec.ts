import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DiagnoseDetailComponent } from './diagnose-detail.component';

describe('DiagnoseDetailComponent', () => {
  let component: DiagnoseDetailComponent;
  let fixture: ComponentFixture<DiagnoseDetailComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [DiagnoseDetailComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(DiagnoseDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
