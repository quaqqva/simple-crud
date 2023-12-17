import { Component } from '@angular/core';
import { TuiAlertService } from '@taiga-ui/core';
import { Customer } from '../../models/customer.model';
import { CustomersService } from '../../services/customers.service';
import BasePageComponent from '../base-page.component';

@Component({
  selector: 'app-customers-page',
  standalone: true,
  imports: [],
  templateUrl: './customers-page.component.html',
  styleUrl: './customers-page.component.scss',
})
export class CustomersPageComponent extends BasePageComponent<Customer> {
  public constructor(
    dbService: CustomersService,
    alertService: TuiAlertService,
  ) {
    super(dbService, alertService);
  }
}
