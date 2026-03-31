import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LeaderSettings } from './leader-settings';

describe('LeaderSettings', () => {
  let component: LeaderSettings;
  let fixture: ComponentFixture<LeaderSettings>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LeaderSettings],
    }).compileComponents();

    fixture = TestBed.createComponent(LeaderSettings);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
