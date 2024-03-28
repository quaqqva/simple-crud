import { ChangeDetectionStrategy, Component } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
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
import { TuiAccordionModule } from '@taiga-ui/kit';

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
    TuiAccordionModule,
    FormsModule,
    ReactiveFormsModule,
  ],
  templateUrl: './addresses-page.component.html',
  styleUrl: './addresses-page.component.scss',
  changeDetection: ChangeDetectionStrategy.Default,
})
export class AddressesPageComponent extends BasePageComponent<Address> {
  form = this.formBuilder.group({
    country: new FormControl(null, [Validators.required]),
    city: new FormControl(null, [Validators.required]),
    street: new FormControl(null, [Validators.required]),
    building: new FormControl(null),
  });

  protected override itemFieldNames = [
    'ID',
    'Страна',
    'Город',
    'Улица',
    'Здание',
  ];

  public constructor(
    dbService: AddressesService,
    alertService: TuiAlertService,
    dialogService: TuiDialogService,
    private formBuilder: FormBuilder,
  ) {
    super(dbService, alertService, dialogService);
  }

  protected override entityFromForm(): Partial<Address> {
    return {
      id: this.editedItem?.id || undefined,
      country: this.form.get('country')!.value!,
      city: this.form.get('city')!.value!,
      street: this.form.get('street')!.value!,
      building: this.form.get('building')!.value || undefined,
    };
  }
}
