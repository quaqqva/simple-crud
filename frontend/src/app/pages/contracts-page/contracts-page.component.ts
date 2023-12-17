import { Component } from '@angular/core';
import { TuiAlertService } from '@taiga-ui/core';
import { Contract } from '../../models/contract.model';
import { ContractsService } from '../../services/contracts.service';
import BasePageComponent from '../base-page.component';

@Component({
  selector: 'app-contracts-page',
  standalone: true,
  imports: [],
  templateUrl: './contracts-page.component.html',
  styleUrl: './contracts-page.component.scss',
})
export class ContractsPageComponent extends BasePageComponent<Contract> {
  public constructor(
    dbService: ContractsService,
    alertService: TuiAlertService,
  ) {
    super(dbService, alertService);
  }
}
