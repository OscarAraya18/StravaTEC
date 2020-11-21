import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BuscarCarrerasRetosComponent } from './buscar-carreras-retos.component';

describe('BuscarCarrerasRetosComponent', () => {
  let component: BuscarCarrerasRetosComponent;
  let fixture: ComponentFixture<BuscarCarrerasRetosComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ BuscarCarrerasRetosComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(BuscarCarrerasRetosComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
