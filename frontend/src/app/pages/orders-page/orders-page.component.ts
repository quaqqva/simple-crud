import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import {
  FormBuilder,
  FormControl,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import {
  TuiTableModule,
  TuiTablePaginationModule,
} from '@taiga-ui/addon-table';
import { TuiAccordionModule, TuiInputModule } from '@taiga-ui/kit';
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
    TuiInputModule,
    FormsModule,
    ReactiveFormsModule,
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

  protected form = this.formBuilder.group({
    productCode: new FormControl(null, [
      Validators.required,
      Validators.pattern(/\d+/),
    ]),
    productQuantity: new FormControl(null, [
      Validators.required,
      Validators.pattern(/\d+/),
    ]),
    contractNumber: new FormControl(null, [
      Validators.required,
      Validators.pattern(/\d+/),
    ]),
  });

  public constructor(
    dbService: OrdersService,
    alertService: TuiAlertService,
    dialogService: TuiDialogService,
    private formBuilder: FormBuilder,
  ) {
    super(dbService, alertService, dialogService);
  }

  protected override entityFromForm(): Partial<Order> {
    const order: Partial<Order> = {
      id: this.editedItem?.id || undefined,
      productCode: Number(this.form.get('productCode')?.value),
      productQuantity: Number(this.form.get('productQuantity')?.value),
      contractNumber: Number(this.form.get('contractNumber')?.value),
    };
    return order;
  }
}
