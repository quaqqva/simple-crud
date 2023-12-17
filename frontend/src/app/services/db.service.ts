import { HttpClient } from '@angular/common/http';
import { firstValueFrom } from 'rxjs';

export abstract class DbService<T> {
  constructor(
    private httpClient: HttpClient,
    private endpoint: string,
    private idSelector: (entity: T) => number,
  ) {}

  public async getById(id: number): Promise<T> {
    const response = await firstValueFrom(
      this.httpClient.get<T>(`${this.endpoint}/${id}`, {
        observe: 'response',
      }),
    );
    DbService.handleError(response.status);
    return response.body as T;
  }

  public getAll(): Promise<T[]> {
    return firstValueFrom(this.httpClient.get<T[]>(this.endpoint));
  }

  public async create(entity: T): Promise<T> {
    const response = firstValueFrom(
      this.httpClient.post(this.endpoint, entity),
    );
    return (await response) as T;
  }

  public async update(entity: T): Promise<T> {
    const id = this.idSelector(entity);
    const response = await firstValueFrom(
      this.httpClient.put<T>(`${this.endpoint}/${id}`, entity, {
        observe: 'response',
      }),
    );
    DbService.handleError(response.status);
    return response.body as T;
  }

  public async delete(id: number): Promise<void> {
    const response = await firstValueFrom(
      this.httpClient.delete(`${this.endpoint}/${id}`, {
        observe: 'response',
      }),
    );
    DbService.handleError(response.status);
  }

  private static handleError(status: number): void {
    if (status === 404) throw Error('Сущность с таким ID не найдена');
    else if (status === 400)
      throw Error(
        'Сущность не может быть удалена по соображениям целостности. Измените или удалите сущности, зависимые от данной',
      );
    else if (status === 500) throw Error('Внутренняя ошибка сервера');
  }
}
