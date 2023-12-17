import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import {
  TuiAlertService,
  TuiButtonModule,
  TuiDialogService,
} from '@taiga-ui/core';
import {
  TuiTableModule,
  TuiTablePaginationModule,
} from '@taiga-ui/addon-table';

import BasePageComponent from '../base-page.component';
import { Address } from '../../models/address.model';
import { AddressesService } from '../../services/addresses.service';

@Component({
  selector: 'app-addresses-page',
  standalone: true,
  imports: [
    CommonModule,
    TuiButtonModule,
    TuiTableModule,
    TuiTablePaginationModule,
  ],
  templateUrl: './addresses-page.component.html',
  styleUrl: './addresses-page.component.scss',
})
export class AddressesPageComponent extends BasePageComponent<Address> {
  public columns = ['ID', 'Страна', 'Город', 'Улица', 'Здание'];

  public constructor(
    dbService: AddressesService,
    alertService: TuiAlertService,
    dialogService: TuiDialogService,
  ) {
    super(dbService, alertService, dialogService);
  }
}
