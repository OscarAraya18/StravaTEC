import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SeguirDeportistasComponent } from './seguir-deportistas.component';

describe('SeguirDeportistasComponent', () => {
  let component: SeguirDeportistasComponent;
  let fixture: ComponentFixture<SeguirDeportistasComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SeguirDeportistasComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SeguirDeportistasComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
