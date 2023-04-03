import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddUserFinishComponent } from './add-user-finish.component';

describe('AddUserFinishComponent', () => {
  let component: AddUserFinishComponent;
  let fixture: ComponentFixture<AddUserFinishComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddUserFinishComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AddUserFinishComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
