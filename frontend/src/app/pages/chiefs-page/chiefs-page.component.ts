import { Component } from '@angular/core';
import { TuiAlertService } from '@taiga-ui/core';

import { ChiefsService } from '../../services/chiefs.service';
import { Chief } from '../../models/chief.model';
import BasePageComponent from '../base-page.component';

@Component({
  selector: 'app-chiefs-page',
  standalone: true,
  imports: [],
  templateUrl: './chiefs-page.component.html',
  styleUrl: './chiefs-page.component.scss',
})
export class ChiefsPageComponent extends BasePageComponent<Chief> {
  public constructor(dbService: ChiefsService, alertService: TuiAlertService) {
    super(dbService, alertService);
  }
}
