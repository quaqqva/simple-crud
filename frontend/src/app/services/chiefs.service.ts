import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Chief } from '../models/chief.model';
import { DbService } from './db.service';

@Injectable({
  providedIn: 'root',
})
export class ChiefsService extends DbService<Chief> {
  constructor(httpClient: HttpClient) {
    super(httpClient, 'Chief', (chief) => chief.id);
  }
}
