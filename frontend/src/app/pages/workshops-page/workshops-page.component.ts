import { Component } from '@angular/core';
import { TuiAlertService } from '@taiga-ui/core';
import { Workshop } from '../../models/workshop.model';
import { WorkshopsService } from '../../services/workshops.service';
import BasePageComponent from '../base-page.component';

@Component({
  selector: 'app-workshops-page',
  standalone: true,
  imports: [],
  templateUrl: './workshops-page.component.html',
  styleUrl: './workshops-page.component.scss',
})
export class WorkshopsPageComponent extends BasePageComponent<Workshop> {
  public constructor(
    dbService: WorkshopsService,
    alertService: TuiAlertService,
  ) {
    super(dbService, alertService);
  }
}
