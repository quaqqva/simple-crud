using Backend.Application.Enums;
using Backend.Application.Interfaces;
using Backend.Domain.Common;
using Microsoft.AspNetCore.SignalR;

namespace Backend.Infrastructure.WebSockets
{
    public class EntityNotificationHubOperator<TEntity>(
        IHubContext<EntityNotificationHub<TEntity>> hubContext
    ) : IEntityNotificationsService<TEntity>
        where TEntity : BaseEntity
    {
        private readonly IHubContext<EntityNotificationHub<TEntity>> _hubContent = hubContext;

        public Task Broadcast(EntityActionType actionType, TEntity entity)
        {
            return _hubContent.Clients.All.SendAsync(actionType.Value, entity);
        }
    }
}
