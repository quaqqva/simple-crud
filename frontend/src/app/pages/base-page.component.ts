import { TuiAlertService } from '@taiga-ui/core';
import { DbService } from '../services/db.service';

export default abstract class BasePageComponent<T> {
  protected items: T[] = [];

  public constructor(
    private dbService: DbService<T>,
    private alertService: TuiAlertService,
  ) {
    this.onReload().then(console.log);
  }

  protected async onCreate(entity: T): Promise<void> {
    try {
      const newEntity = await this.dbService.create(entity);
      this.items.push(newEntity);
      this.showOkMessage('Запись успешно добавлена');
    } catch (error) {
      this.showErrorMesage((error as Error).message);
    }
  }

  protected async onUpdate(entity: T): Promise<void> {
    try {
      const index = this.items.indexOf(entity);
      const newEntity = await this.dbService.update(entity);
      this.items[index] = newEntity;
      this.showOkMessage('Запись успешно обновлена');
    } catch (error) {
      this.showErrorMesage((error as Error).message);
    }
  }

  protected async onDelete(entity: T): Promise<void> {
    try {
      this.dbService.delete(entity);
      this.items = this.items.filter((item) => item !== entity);
      this.showOkMessage('Запись успешно удалена');
    } catch (error) {
      this.showErrorMesage((error as Error).message);
    }
  }

  protected async onReload(): Promise<void> {
    this.items = await this.dbService.getAll();
  }

  protected showOkMessage(message: string): void {
    this.alertService.open(message);
  }

  protected showErrorMesage(message: string): void {
    this.alertService.open(message);
  }
}
