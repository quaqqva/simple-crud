import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { DbService } from './db.service';
import { Address } from '../models/address.model';

@Injectable({
  providedIn: 'root',
})
export class AddressesService extends DbService<Address> {
  constructor(httpClient: HttpClient) {
    super(httpClient, 'addresses');
  }
}
