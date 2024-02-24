using backend.Utilities.Enums;
using Microsoft.AspNetCore.SignalR;

namespace backend.WebSocket
{
    public class EntityNotificationHubOperator<TEntity>(IHubContext<EntityNotificationHub<TEntity>> hubContext)
    {
        private readonly IHubContext<EntityNotificationHub<TEntity>> _hubContent = hubContext;

        public Task Broadcast(EntityActionType actionType, TEntity entity) {
            return _hubContent.Clients.All.SendAsync(actionType.Value, entity); 
        }
    }
}