import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PortEditComponent } from './port-edit.component';

describe('PortEditComponent', () => {
  let component: PortEditComponent;
  let fixture: ComponentFixture<PortEditComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PortEditComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PortEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
