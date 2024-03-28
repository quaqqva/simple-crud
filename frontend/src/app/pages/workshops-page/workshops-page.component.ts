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
import { Workshop } from '../../models/workshop.model';
import { WorkshopsService } from '../../services/workshops.service';
import BasePageComponent from '../base-page.component';

@Component({
  selector: 'app-workshops-page',
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
  templateUrl: './workshops-page.component.html',
  styleUrl: './workshops-page.component.scss',
})
export class WorkshopsPageComponent extends BasePageComponent<Workshop> {
  form = this.formBuilder.group({
    name: new FormControl(null, [Validators.required]),
    phoneNumber: new FormControl(null, [Validators.required]),
    chiefId: new FormControl(null, [
      Validators.required,
      Validators.pattern(/\d+/),
    ]),
  });
  protected override itemFieldNames: string[] = [
    'ID',
    'Название',
    'Номер телефона',
  ];

  public constructor(
    dbService: WorkshopsService,
    alertService: TuiAlertService,
    dialogService: TuiDialogService,
    private formBuilder: FormBuilder,
  ) {
    super(dbService, alertService, dialogService);
  }

  protected override entityFromForm(): Partial<Workshop> {
    return {
      id: this.editedItem?.id,
      name: this.form.get('name')!.value!,
      phoneNumber: this.form.get('phoneNumber')!.value!,
      chiefId: this.form.get('chiefId')!.value!,
    };
  }
}
