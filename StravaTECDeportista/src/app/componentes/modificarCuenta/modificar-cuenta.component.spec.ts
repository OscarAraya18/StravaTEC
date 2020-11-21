import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ModificarCuentaComponent } from './modificar-cuenta.component';

describe('ModificarCuentaComponent', () => {
  let component: ModificarCuentaComponent;
  let fixture: ComponentFixture<ModificarCuentaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ModificarCuentaComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ModificarCuentaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
