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
import { Contract } from '../../models/contract.model';
import { ContractsService } from '../../services/contracts.service';
import BasePageComponent from '../base-page.component';

@Component({
  selector: 'app-contracts-page',
  standalone: true,
  imports: [
    CommonModule,
    TuiButtonModule,
    TuiTableModule,
    TuiTablePaginationModule,
    TuiAccordionModule,
  ],
  templateUrl: './contracts-page.component.html',
  styleUrl: './contracts-page.component.scss',
})
export class ContractsPageComponent extends BasePageComponent<Contract> {
  protected override itemFieldNames = [
    'ID',
    'Дата выполнения',
    'Дата регистрации',
  ];

  public constructor(
    dbService: ContractsService,
    alertService: TuiAlertService,
    dialogService: TuiDialogService,
  ) {
    super(dbService, alertService, dialogService);
  }
}
