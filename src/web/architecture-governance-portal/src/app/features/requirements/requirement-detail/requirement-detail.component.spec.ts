import { ComponentFixture, TestBed } from '@angular/core/testing';
import { provideHttpClient } from '@angular/common/http';
import { provideHttpClientTesting } from '@angular/common/http/testing';
import { provideRouter } from '@angular/router';

import { RequirementDetailComponent } from './requirement-detail.component';

describe('RequirementDetailComponent', () => {
  let component: RequirementDetailComponent;
  let fixture: ComponentFixture<RequirementDetailComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RequirementDetailComponent],
      providers: [
        provideHttpClient(),
        provideHttpClientTesting(),
        provideRouter([])
      ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RequirementDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
