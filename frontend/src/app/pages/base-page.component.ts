import { FormGroup } from '@angular/forms';
import { TuiAlertService, TuiDialogService } from '@taiga-ui/core';
import { TuiTablePagination } from '@taiga-ui/addon-table';
import { TUI_PROMPT, TuiPromptData } from '@taiga-ui/kit';
// eslint-disable-next-line import/no-extraneous-dependencies
import { PolymorpheusContent } from '@tinkoff/ng-polymorpheus';
import { DbService } from '../services/db.service';

export default abstract class BasePageComponent<T extends object> {
  protected items: T[] = [];

  protected shownItems: T[] = [];

  protected currentItem?: T;

  protected editedItem?: T;

  protected abstract form: FormGroup;

  protected abstract itemFieldNames: string[];

  private pageSize: number = 10;

  private pageNum: number = 1;

  public constructor(
    private dbService: DbService<T>,
    private alertService: TuiAlertService,
    private dialogService: TuiDialogService,
  ) {
    this.onReload();
  }

  protected async onCreate(entity: Partial<T>): Promise<void> {
    try {
      // const newEntity =
      await this.dbService.create(entity);
      this.onReload();
      // this.items.push(newEntity);
      this.showOkMessage('Запись успешно добавлена');
    } catch (error) {
      this.showErrorMesage((error as Error).message);
    }
  }

  protected async onUpdate(entity: T): Promise<void> {
    try {
      // const index = this.items.indexOf(entity);
      // const newEntity =
      await this.dbService.update(entity);
      this.onReload();
      // this.items[index] = newEntity;
      this.showOkMessage('Запись успешно обновлена');
    } catch (error) {
      this.showErrorMesage((error as Error).message);
    }
  }

  protected async onDelete(entity: T): Promise<void> {
    try {
      await this.dbService.delete(entity);
      // const indexOfEntity = this.items.indexOf(entity);
      // this.items = this.items.filter((item, index) => index !== indexOfEntity);
      this.onReload();
      this.showOkMessage('Запись успешно удалена');
    } catch (error) {
      this.showErrorMesage((error as Error).message);
    }
  }

  protected async onReload(): Promise<void> {
    this.items = await this.dbService.getAll();
    this.paginate();
  }

  protected onPaginationChange(pagination: TuiTablePagination) {
    const { size, page } = pagination;
    this.pageSize = size;
    this.pageNum = page + 1;
    this.paginate();
  }

  private paginate(): void {
    this.shownItems = this.items.slice(
      this.pageSize * (this.pageNum - 1),
      this.pageSize * this.pageNum,
    );
  }

  protected showOkMessage(message: string): void {
    this.alertService
      .open(message, {
        status: 'success',
        autoClose: true,
      })
      .subscribe();
  }

  protected showErrorMesage(message: string): void {
    this.alertService
      .open(message, {
        status: 'error',
        autoClose: true,
      })
      .subscribe();
  }

  protected showDetailsDialog(content: PolymorpheusContent, entity: T): void {
    this.currentItem = entity;
    this.dialogService.open(content).subscribe();
  }

  protected showCreateDialog(): void {
    this.editedItem = undefined;
    window.scrollTo({
      top: document.documentElement.offsetHeight,
      behavior: 'smooth',
    });
  }

  protected showUpdateDialog(entity?: T): void {
    this.editedItem = entity;
    window.scrollTo({
      top: document.documentElement.offsetHeight,
      behavior: 'smooth',
    });
  }

  protected showDeleteDialog(entity: T): void {
    const promptData: TuiPromptData = {
      yes: 'Да',
      no: 'Нет',
    };
    this.dialogService
      .open<boolean>(TUI_PROMPT, {
        label: 'Вы уверены, что хотите удалить запись?',
        size: 'm',
        data: promptData,
      })
      .subscribe((isYes) => {
        if (isYes) this.onDelete(entity);
      });
  }

  protected onFormSubmit(): void {
    if (!this.form.valid)
      this.showErrorMesage('Пожалуйста, заполните форму корректно');
    const isUpdating = Boolean(this.editedItem);
    const entity = this.entityFromForm();
    if (isUpdating) this.onUpdate(entity as T);
    else this.onCreate(entity);
  }

  protected abstract entityFromForm(): Partial<T>;
}
