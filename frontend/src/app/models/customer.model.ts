/* eslint-disable import/no-cycle */
import { Address } from './address.model';
import { Contract } from './contract.model';

export type Customer = {
  id?: number;
  name: string;
  addressId: number;
  address: Address;
  contracts: Contract[];
};
