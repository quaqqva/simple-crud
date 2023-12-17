import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Customer } from '../models/customer.model';
import { DbService } from './db.service';

@Injectable({
  providedIn: 'root',
})
export class CustomersService extends DbService<Customer> {
  constructor(httpClient: HttpClient) {
    super(httpClient, 'Customer', (customer) => customer.id);
  }
}
