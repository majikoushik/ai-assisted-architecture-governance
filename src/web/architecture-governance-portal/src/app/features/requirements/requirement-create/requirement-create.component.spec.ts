import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RequirementCreateComponent } from './requirement-create.component';

describe('RequirementCreateComponent', () => {
  let component: RequirementCreateComponent;
  let fixture: ComponentFixture<RequirementCreateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RequirementCreateComponent]
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
