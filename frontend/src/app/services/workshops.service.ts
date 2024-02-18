import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Workshop } from '../models/workshop.model';
import { DbService } from './db.service';

@Injectable({
  providedIn: 'root',
})
export class WorkshopsService extends DbService<Workshop> {
  constructor(httpClient: HttpClient) {
    super(httpClient, 'workshops');
  }
}
