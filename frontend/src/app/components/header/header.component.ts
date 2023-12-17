import { ChangeDetectionStrategy, Component } from '@angular/core';
import { FormControl, ReactiveFormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { TuiButtonModule, TuiTextfieldControllerModule } from '@taiga-ui/core';
import { TuiDataListWrapperModule, TuiSelectModule } from '@taiga-ui/kit';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [
    TuiSelectModule,
    TuiDataListWrapperModule,
    TuiTextfieldControllerModule,
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

  public pageSelect = new FormControl();

  public constructor(router: Router) {
    this.pageSelect.registerOnChange((item: string) => {
      router.navigateByUrl(item);
    });
  }

  // eslint-disable-next-line class-methods-use-this
  showReport(): void {}
}
