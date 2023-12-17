import { Component } from '@angular/core';
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
  ],
  templateUrl: './products-page.component.html',
  styleUrl: './products-page.component.scss',
})
export class ProductsPageComponent extends BasePageComponent<Product> {
  protected override itemFieldNames: string[] = ['ID', 'Название', 'Цена'];

  public constructor(
    dbService: ProductsService,
    alertService: TuiAlertService,
    dialogService: TuiDialogService,
  ) {
    super(dbService, alertService, dialogService);
  }
}
