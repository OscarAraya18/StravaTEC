import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MisCarrerasRetosComponent } from './mis-carreras-retos.component';

describe('MisCarrerasRetosComponent', () => {
  let component: MisCarrerasRetosComponent;
  let fixture: ComponentFixture<MisCarrerasRetosComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MisCarrerasRetosComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MisCarrerasRetosComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
