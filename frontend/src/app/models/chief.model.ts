/* eslint-disable import/no-cycle */
import { Workshop } from './workshop.model';

export type Chief = {
  id: number;
  firstName: string;
  lastName: string;
  patronymic?: string;
  workshops: Workshop[];
};
