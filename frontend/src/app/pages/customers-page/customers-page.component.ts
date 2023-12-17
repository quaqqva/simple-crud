import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import {
  TuiAlertService,
  TuiButtonModule,
  TuiDialogService,
} from '@taiga-ui/core';
import {
  TuiTableModule,
  TuiTablePaginationModule,
} from '@taiga-ui/addon-table';
import { TuiAccordionModule } from '@taiga-ui/kit';
import { Customer } from '../../models/customer.model';
import { CustomersService } from '../../services/customers.service';
import BasePageComponent from '../base-page.component';

@Component({
  selector: 'app-customers-page',
  standalone: true,
  imports: [
    CommonModule,
    TuiButtonModule,
    TuiTableModule,
    TuiTablePaginationModule,
    TuiAccordionModule,
  ],
  templateUrl: './customers-page.component.html',
  styleUrl: './customers-page.component.scss',
})
export class CustomersPageComponent extends BasePageComponent<Customer> {
  protected override itemFieldNames: string[] = ['ID', 'Имя'];

  public constructor(
    dbService: CustomersService,
    alertService: TuiAlertService,
    dialogService: TuiDialogService,
  ) {
    super(dbService, alertService, dialogService);
  }
}
