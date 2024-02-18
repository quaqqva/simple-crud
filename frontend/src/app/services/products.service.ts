import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Product } from '../models/product.model';
import { DbService } from './db.service';

@Injectable({
  providedIn: 'root',
})
export class ProductsService extends DbService<Product> {
  constructor(httpClient: HttpClient) {
    super(httpClient, 'products');
  }
}
