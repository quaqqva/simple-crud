/* eslint-disable import/no-cycle */
import { Order } from './order.model';

export type Contract = {
  number?: number;
  completionDate: string;
  registrationDate?: string;
  customerId: number;
  orders: Order[];
};
