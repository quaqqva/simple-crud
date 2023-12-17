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
import { Order } from '../../models/order.model';
import { OrdersService } from '../../services/orders.service';
import BasePageComponent from '../base-page.component';

@Component({
  selector: 'app-orders-page',
  standalone: true,
  imports: [
    CommonModule,
    TuiButtonModule,
    TuiTableModule,
    TuiTablePaginationModule,
    TuiAccordionModule,
  ],
  templateUrl: './orders-page.component.html',
  styleUrl: './orders-page.component.scss',
})
export class OrdersPageComponent extends BasePageComponent<Order> {
  protected override itemFieldNames: string[] = [
    'ID',
    'Номер контракта',
    'Код продукта',
    'Количество продукта',
  ];

  public constructor(
    dbService: OrdersService,
    alertService: TuiAlertService,
    dialogService: TuiDialogService,
  ) {
    super(dbService, alertService, dialogService);
  }
}
