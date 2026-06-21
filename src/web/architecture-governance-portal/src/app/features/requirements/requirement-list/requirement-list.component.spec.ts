import { ComponentFixture, TestBed } from '@angular/core/testing';
import { provideHttpClient } from '@angular/common/http';
import { provideHttpClientTesting } from '@angular/common/http/testing';
import { provideRouter } from '@angular/router';

import { RequirementListComponent } from './requirement-list.component';

describe('RequirementListComponent', () => {
  let component: RequirementListComponent;
  let fixture: ComponentFixture<RequirementListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RequirementListComponent],
      providers: [
        provideHttpClient(),
        provideHttpClientTesting(),
        provideRouter([])
      ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RequirementListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
