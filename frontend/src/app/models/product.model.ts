/* eslint-disable import/no-cycle */
import { Order } from './order.model';
import { Workshop } from './workshop.model';

export type Product = {
  code?: number;
  orders: Order[];
  workshop: Workshop;
  name: string;
  price: number;
  workshopNumber: number;
};
