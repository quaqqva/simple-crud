import { ChangeDetectionStrategy, Component } from '@angular/core';
import { FormControl, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { TuiButtonModule, TuiTextfieldControllerModule } from '@taiga-ui/core';
import { TuiDataListWrapperModule, TuiSelectModule } from '@taiga-ui/kit';
import routes from '../../app.routes';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [
    TuiSelectModule,
    TuiDataListWrapperModule,
    TuiTextfieldControllerModule,
    FormsModule,
    ReactiveFormsModule,
    TuiButtonModule,
  ],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class HeaderComponent {
  public items = [
    'Начальники цехов',
    'Цехи',
    'Товары',
    'Заказы',
    'Контракты',
    'Клиенты',
    'Адресы',
  ];

  public pageSelect = new FormControl(null);

  public constructor(router: Router) {
    this.pageSelect.valueChanges.subscribe((value: unknown) => {
      const selected = value as string;
      const route = routes[this.items.indexOf(selected)].path;
      router.navigateByUrl(route!);
    });
  }

  // eslint-disable-next-line class-methods-use-this
  showReport(): void {}
}
