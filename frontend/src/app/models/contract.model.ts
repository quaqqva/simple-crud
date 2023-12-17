/* eslint-disable import/no-cycle */
import { Customer } from './customer.model';
import { Order } from './order.model';

export type Contract = {
  number?: number;
  completionDate: string;
  registrationDate?: string;
  customerId: number;
  customer: Customer;
  orders: Order[];
};
