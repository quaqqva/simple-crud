import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Contract } from '../models/contract.model';
import { DbService } from './db.service';

@Injectable({
  providedIn: 'root',
})
export class ContractsService extends DbService<Contract> {
  constructor(httpClient: HttpClient) {
    super(httpClient, 'Contract', (contract) => contract.number);
  }
}
