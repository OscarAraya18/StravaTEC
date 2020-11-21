import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MiMuroComponent } from './mi-muro.component';

describe('MiMuroComponent', () => {
  let component: MiMuroComponent;
  let fixture: ComponentFixture<MiMuroComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MiMuroComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MiMuroComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
