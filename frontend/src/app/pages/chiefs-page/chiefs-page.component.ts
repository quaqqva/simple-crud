import { Component } from '@angular/core';
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
import { CommonModule } from '@angular/common';
import {
  TuiTableModule,
  TuiTablePaginationModule,
} from '@taiga-ui/addon-table';
import { TuiAccordionModule } from '@taiga-ui/kit';

import { ChiefsService } from '../../services/chiefs.service';
import { Chief } from '../../models/chief.model';
import BasePageComponent from '../base-page.component';

@Component({
  selector: 'app-chiefs-page',
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
  templateUrl: './chiefs-page.component.html',
  styleUrl: './chiefs-page.component.scss',
})
export class ChiefsPageComponent extends BasePageComponent<Chief> {
  form = this.formBuilder.group({
    firstName: new FormControl(null, [Validators.required]),
    lastName: new FormControl(null, [Validators.required]),
    patronymic: new FormControl(null),
  });
  protected override itemFieldNames = ['ID', 'Фамилия', 'Имя', 'Отчество'];

  public constructor(
    dbService: ChiefsService,
    alertService: TuiAlertService,
    dialogService: TuiDialogService,
    private formBuilder: FormBuilder,
  ) {
    super(dbService, alertService, dialogService);
  }

  protected override entityFromForm(): Partial<Chief> {
    return {
      id: this.editedItem?.id || undefined,
      firstName: this.form.get('firstName')!.value!,
      lastName: this.form.get('lastName')!.value!,
      patronymic: this.form.get('patronymic')!.value || undefined,
    };
  }
}
