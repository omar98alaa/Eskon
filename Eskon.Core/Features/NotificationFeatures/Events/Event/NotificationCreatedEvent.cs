using MediatR;

namespace Eskon.Core.Features.NotificationFeatures.Events.Event
{

    public record NotificationCreatedEvent(
        Guid ReceiverId,
        string Content,
        string NotificationTypeName,
        Guid NotificationId,
        DateTime CreatedAt) : INotification;

}
