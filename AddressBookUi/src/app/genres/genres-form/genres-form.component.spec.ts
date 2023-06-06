import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GenresFormComponent } from './genres-form.component';

describe('GenresFormComponent', () => {
  let component: GenresFormComponent;
  let fixture: ComponentFixture<GenresFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ GenresFormComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(GenresFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
