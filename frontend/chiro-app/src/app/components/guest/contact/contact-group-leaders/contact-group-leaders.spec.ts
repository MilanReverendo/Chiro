import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ContactGroupLeaders } from './contact-group-leaders';

describe('ContactGroupLeaders', () => {
  let component: ContactGroupLeaders;
  let fixture: ComponentFixture<ContactGroupLeaders>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ContactGroupLeaders],
    }).compileComponents();

    fixture = TestBed.createComponent(ContactGroupLeaders);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
