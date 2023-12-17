/* eslint-disable import/no-cycle */
import { Chief } from './chief.model';
import { Product } from './product.model';

export type Workshop = {
  number?: number;
  name: string;
  phoneNumber: string;
  chiefId: number;
  chief: Chief;
  products: Product[];
};
