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
  TuiAlertService,
  TuiButtonModule,
  TuiDialogService,
} from '@taiga-ui/core';
import {
  TuiTableModule,
  TuiTablePaginationModule,
} from '@taiga-ui/addon-table';
import { TuiAccordionModule } from '@taiga-ui/kit';
import { Customer } from '../../models/customer.model';
import { CustomersService } from '../../services/customers.service';
import BasePageComponent from '../base-page.component';

@Component({
  selector: 'app-customers-page',
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
  templateUrl: './customers-page.component.html',
  styleUrl: './customers-page.component.scss',
})
export class CustomersPageComponent extends BasePageComponent<Customer> {
  form = this.formBuilder.group({
    name: new FormControl(null, [Validators.required]),
    addressId: new FormControl(null, [
      Validators.required,
      Validators.pattern(/\d+/),
    ]),
  });
  protected override itemFieldNames: string[] = ['ID', 'Имя'];

  public constructor(
    dbService: CustomersService,
    alertService: TuiAlertService,
    dialogService: TuiDialogService,
    private formBuilder: FormBuilder,
  ) {
    super(dbService, alertService, dialogService);
  }

  protected override entityFromForm(): Partial<Customer> {
    return {
      id: this.editedItem?.id,
      name: this.form.get('name')!.value!,
      addressId: this.form.get('addressId')!.value!,
    };
  }
}
