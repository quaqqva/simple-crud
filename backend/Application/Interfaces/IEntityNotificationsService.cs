using Backend.Application.Enums;
using Backend.Domain.Common;

namespace Backend.Application.Interfaces;

public interface IEntityNotificationsService<TEntity> where TEntity : BaseEntity
{
    public Task Broadcast(EntityActionType actionType, TEntity entity);
}