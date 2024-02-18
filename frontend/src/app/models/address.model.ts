// eslint-disable-next-line import/no-cycle
import { Customer } from './customer.model';

export type Address = {
  id: number;
  country: string;
  city: string;
  street: string;
  building: string;
  customers: Customer[];
};
