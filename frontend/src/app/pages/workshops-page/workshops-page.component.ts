import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import {
  TuiTableModule,
  TuiTablePaginationModule,
} from '@taiga-ui/addon-table';
import { TuiAccordionModule } from '@taiga-ui/kit';
import {
  TuiAlertService,
  TuiButtonModule,
  TuiDialogService,
} from '@taiga-ui/core';
import { Workshop } from '../../models/workshop.model';
import { WorkshopsService } from '../../services/workshops.service';
import BasePageComponent from '../base-page.component';

@Component({
  selector: 'app-workshops-page',
  standalone: true,
  imports: [
    CommonModule,
    TuiButtonModule,
    TuiTableModule,
    TuiTablePaginationModule,
    TuiAccordionModule,
  ],
  templateUrl: './workshops-page.component.html',
  styleUrl: './workshops-page.component.scss',
})
export class WorkshopsPageComponent extends BasePageComponent<Workshop> {
  protected override itemFieldNames: string[] = [
    'ID',
    'Название',
    'Номер телефона',
  ];

  public constructor(
    dbService: WorkshopsService,
    alertService: TuiAlertService,
    dialogService: TuiDialogService,
  ) {
    super(dbService, alertService, dialogService);
  }
}
