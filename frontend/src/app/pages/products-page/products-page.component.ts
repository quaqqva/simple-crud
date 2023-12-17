import { Component } from '@angular/core';
import { TuiAlertService } from '@taiga-ui/core';
import { Product } from '../../models/product.model';
import { ProductsService } from '../../services/products.service';
import BasePageComponent from '../base-page.component';

@Component({
  selector: 'app-products-page',
  standalone: true,
  imports: [],
  templateUrl: './products-page.component.html',
  styleUrl: './products-page.component.scss',
})
export class ProductsPageComponent extends BasePageComponent<Product> {
  public constructor(
    dbService: ProductsService,
    alertService: TuiAlertService,
  ) {
    super(dbService, alertService);
  }
}
