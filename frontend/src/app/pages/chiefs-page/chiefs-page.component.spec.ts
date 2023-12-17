import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ChiefsPageComponent } from './chiefs-page.component';

describe('ChiefsPageComponent', () => {
  let component: ChiefsPageComponent;
  let fixture: ComponentFixture<ChiefsPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ChiefsPageComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(ChiefsPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
