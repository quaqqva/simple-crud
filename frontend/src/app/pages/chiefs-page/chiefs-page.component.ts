import { Component } from '@angular/core';
import {
  TuiAlertService,
  TuiButtonModule,
  TuiDialogService,
} from '@taiga-ui/core';
import { CommonModule } from '@angular/common';
import {
  TuiTableModule,
  TuiTablePaginationModule,
} from '@taiga-ui/addon-table';
import { TuiAccordionModule } from '@taiga-ui/kit';

import { ChiefsService } from '../../services/chiefs.service';
import { Chief } from '../../models/chief.model';
import BasePageComponent from '../base-page.component';

@Component({
  selector: 'app-chiefs-page',
  standalone: true,
  imports: [
    CommonModule,
    TuiButtonModule,
    TuiTableModule,
    TuiTablePaginationModule,
    TuiAccordionModule,
  ],
  templateUrl: './chiefs-page.component.html',
  styleUrl: './chiefs-page.component.scss',
})
export class ChiefsPageComponent extends BasePageComponent<Chief> {
  protected override itemFieldNames = ['ID', 'Фамилия', 'Имя', 'Отчество'];

  public constructor(
    dbService: ChiefsService,
    alertService: TuiAlertService,
    dialogService: TuiDialogService,
  ) {
    super(dbService, alertService, dialogService);
  }
}
