import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AlbumsFormComponent } from './albums-form.component';

describe('AlbumsFormComponent', () => {
  let component: AlbumsFormComponent;
  let fixture: ComponentFixture<AlbumsFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AlbumsFormComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AlbumsFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
