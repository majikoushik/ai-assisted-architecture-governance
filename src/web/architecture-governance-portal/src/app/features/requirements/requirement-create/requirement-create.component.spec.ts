import { ComponentFixture, TestBed } from '@angular/core/testing';
import { provideHttpClient } from '@angular/common/http';
import { provideHttpClientTesting } from '@angular/common/http/testing';
import { provideRouter } from '@angular/router';

import { RequirementCreateComponent } from './requirement-create.component';

describe('RequirementCreateComponent', () => {
  let component: RequirementCreateComponent;
  let fixture: ComponentFixture<RequirementCreateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RequirementCreateComponent],
      providers: [
        provideHttpClient(),
        provideHttpClientTesting(),
        provideRouter([])
      ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RequirementCreateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
