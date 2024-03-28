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
import { Product } from '../../models/product.model';
import { ProductsService } from '../../services/products.service';
import BasePageComponent from '../base-page.component';

@Component({
  selector: 'app-products-page',
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
  templateUrl: './products-page.component.html',
  styleUrl: './products-page.component.scss',
})
export class ProductsPageComponent extends BasePageComponent<Product> {
  form = this.formBuilder.group({
    name: new FormControl(null, [Validators.required]),
    price: new FormControl(null, [
      Validators.required,
      Validators.pattern(/\d+/),
    ]),
    workshopId: new FormControl(null, [
      Validators.required,
      Validators.pattern(/\d+/),
    ]),
  });
  protected override itemFieldNames: string[] = ['ID', 'Название', 'Цена'];

  public constructor(
    dbService: ProductsService,
    alertService: TuiAlertService,
    dialogService: TuiDialogService,
    private formBuilder: FormBuilder,
  ) {
    super(dbService, alertService, dialogService);
  }

  protected override entityFromForm(): Partial<Product> {
    return {
      id: this.editedItem?.id,
      name: this.form.get('name')!.value!,
      price: this.form.get('price')!.value!,
      workshopId: this.form.get('workshopId')!.value!,
    };
  }
}
