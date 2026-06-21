import { ComponentFixture, TestBed } from '@angular/core/testing';
import { provideHttpClient } from '@angular/common/http';
import { provideHttpClientTesting } from '@angular/common/http/testing';

import { SystemHealthComponent } from './system-health.component';

describe('SystemHealthComponent', () => {
  let component: SystemHealthComponent;
  let fixture: ComponentFixture<SystemHealthComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SystemHealthComponent],
      providers: [
        provideHttpClient(),
        provideHttpClientTesting()
      ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SystemHealthComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
