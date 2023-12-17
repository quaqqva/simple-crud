import { Component } from '@angular/core';
import { TuiAlertService } from '@taiga-ui/core';

import BasePageComponent from '../base-page.component';
import { Address } from '../../models/address.model';
import { AddressesService } from '../../services/addresses.service';

@Component({
  selector: 'app-addresses-page',
  standalone: true,
  imports: [],
  templateUrl: './addresses-page.component.html',
  styleUrl: './addresses-page.component.scss',
})
export class AddressesPageComponent extends BasePageComponent<Address> {
  public constructor(
    dbService: AddressesService,
    alertService: TuiAlertService,
  ) {
    super(dbService, alertService);
  }
}
