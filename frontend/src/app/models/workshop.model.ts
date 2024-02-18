/* eslint-disable import/no-cycle */
import { Chief } from './chief.model';
import { Product } from './product.model';

export type Workshop = {
  id: number;
  name: string;
  phoneNumber: string;
  chiefId: number;
  chief: Chief;
  products: Product[];
};
