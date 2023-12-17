import { Routes } from '@angular/router';

const routes: Routes = [
  {
    path: 'address',
    loadComponent: () =>
      import('./pages/addresses-page/addresses-page.component').then(
        (module) => module.AddressesPageComponent,
      ),
  },
  {
    path: 'chief',
    loadComponent: () =>
      import('./pages/chiefs-page/chiefs-page.component').then(
        (module) => module.ChiefsPageComponent,
      ),
  },
  {
    path: 'contract',
    loadComponent: () =>
      import('./pages/contracts-page/contracts-page.component').then(
        (module) => module.ContractsPageComponent,
      ),
  },
  {
    path: 'customer',
    loadComponent: () =>
      import('./pages/customers-page/customers-page.component').then(
        (module) => module.CustomersPageComponent,
      ),
  },
  {
    path: 'order',
    loadComponent: () =>
      import('./pages/orders-page/orders-page.component').then(
        (module) => module.OrdersPageComponent,
      ),
  },
  {
    path: 'product',
    loadComponent: () =>
      import('./pages/products-page/products-page.component').then(
        (module) => module.ProductsPageComponent,
      ),
  },
  {
    path: 'workshop',
    loadComponent: () =>
      import('./pages/workshops-page/workshops-page.component').then(
        (module) => module.WorkshopsPageComponent,
      ),
  },
];
export default routes;
