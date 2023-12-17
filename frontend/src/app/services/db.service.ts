import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { firstValueFrom } from 'rxjs';

export abstract class DbService<T> {
  constructor(
    private httpClient: HttpClient,
    private endpoint: string,
    private idSelector: (entity: T) => number | undefined,
  ) {}

  public async getById(id: number): Promise<T> {
    try {
      const response = await firstValueFrom(
        this.httpClient.get<T>(`${this.endpoint}/${id}`, {
          observe: 'response',
        }),
      );
      return response.body as T;
    } catch (error) {
      DbService.handleError((error as HttpErrorResponse).status);
      throw error;
    }
  }

  public getAll(): Promise<T[]> {
    return firstValueFrom(this.httpClient.get<T[]>(this.endpoint));
  }

  public async create(entity: T): Promise<T> {
    try {
      const response = await firstValueFrom(
        this.httpClient.post(this.endpoint, entity),
      );
      return (await response) as T;
    } catch (error) {
      DbService.handleError((error as HttpErrorResponse).status);
      throw error;
    }
  }

  public async update(entity: T): Promise<T> {
    const id = this.idSelector(entity);
    if (!id) throw Error('ID не предоставлен');

    try {
      const response = await firstValueFrom(
        this.httpClient.put<T>(`${this.endpoint}/${id}`, entity, {
          observe: 'response',
        }),
      );
      return response.body as T;
    } catch (error) {
      DbService.handleError((error as HttpErrorResponse).status);
      throw error;
    }
  }

  public async delete(entity: T): Promise<void> {
    const id = this.idSelector(entity);
    await firstValueFrom(
      this.httpClient.delete(`${this.endpoint}/${id}`, {
        observe: 'response',
      }),
    ).catch((errResponse) => DbService.handleError(errResponse.status));
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
