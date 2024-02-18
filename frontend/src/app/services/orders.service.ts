import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Order } from '../models/order.model';
import { DbService } from './db.service';

@Injectable({
  providedIn: 'root',
})
export class OrdersService extends DbService<Order> {
  constructor(httpClient: HttpClient) {
    super(httpClient, 'orders');
  }
}
