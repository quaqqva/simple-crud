/* eslint-disable import/no-cycle */
import { Contract } from './contract.model';
import { Product } from './product.model';

export type Order = {
  id: number;
  productQuantity: number;
  productId: number;
  contractId: number;
  contract: Contract;
  product: Product;
};
