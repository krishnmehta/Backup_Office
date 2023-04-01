import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UserOnboardComponent } from './user-onboard.component';

describe('UserOnboardComponent', () => {
  let component: UserOnboardComponent;
  let fixture: ComponentFixture<UserOnboardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [UserOnboardComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(UserOnboardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
