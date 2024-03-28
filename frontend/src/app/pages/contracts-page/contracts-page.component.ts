import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import {
  FormBuilder,
  FormControl,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import {
  TuiTableModule,
  TuiTablePaginationModule,
} from '@taiga-ui/addon-table';
import { TuiAccordionModule } from '@taiga-ui/kit';
import {
  TuiAlertService,
  TuiButtonModule,
  TuiDialogService,
} from '@taiga-ui/core';
import { Contract } from '../../models/contract.model';
import { ContractsService } from '../../services/contracts.service';
import BasePageComponent from '../base-page.component';

@Component({
  selector: 'app-contracts-page',
  standalone: true,
  imports: [
    CommonModule,
    TuiButtonModule,
    TuiTableModule,
    TuiTablePaginationModule,
    TuiAccordionModule,
    FormsModule,
    ReactiveFormsModule,
  ],
  templateUrl: './contracts-page.component.html',
  styleUrl: './contracts-page.component.scss',
})
export class ContractsPageComponent extends BasePageComponent<Contract> {
  form = this.formBuilder.group({
    completionDate: new FormControl(null, [Validators.required]),
    registrationDate: new FormControl(null),
    customerId: new FormControl(null, [
      Validators.required,
      Validators.pattern(/\d+/),
    ]),
  });
  protected override itemFieldNames = [
    'ID',
    'Дата выполнения',
    'Дата регистрации',
  ];

  public constructor(
    dbService: ContractsService,
    alertService: TuiAlertService,
    dialogService: TuiDialogService,
    private formBuilder: FormBuilder,
  ) {
    super(dbService, alertService, dialogService);
  }

  protected override entityFromForm(): Partial<Contract> {
    return {
      id: this.editedItem?.id,
      completionDate: this.form.get('completionDate')!.value!,
      registrationDate: this.form.get('registrationDate')?.value || undefined,
      customerId: this.form.get('customerId')!.value!,
    };
  }
}
