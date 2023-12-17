import { Component } from '@angular/core';
import { TuiAlertService } from '@taiga-ui/core';
import { Order } from '../../models/order.model';
import { OrdersService } from '../../services/orders.service';
import BasePageComponent from '../base-page.component';

@Component({
  selector: 'app-orders-page',
  standalone: true,
  imports: [],
  templateUrl: './orders-page.component.html',
  styleUrl: './orders-page.component.scss',
})
export class OrdersPageComponent extends BasePageComponent<Order> {
  public constructor(dbService: OrdersService, alertService: TuiAlertService) {
    super(dbService, alertService);
  }
}
