/* eslint-disable import/no-cycle */
import { Order } from './order.model';
import { Workshop } from './workshop.model';

export type Product = {
  id: number;
  orders: Order[];
  workshop: Workshop;
  name: string;
  price: number;
  workshopId: number;
};
