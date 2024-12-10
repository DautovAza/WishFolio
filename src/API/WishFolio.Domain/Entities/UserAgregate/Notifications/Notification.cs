namespace WishFolio.Domain.Entities.UserAgregate.Notifications;

public class Notification
{
    public Guid NotificationId { get; private set; }
    public NotificationType Type { get; private set; }
    public string Message { get; private set; }
    public bool IsRead { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public Notification(NotificationType type, string message)
    {
        NotificationId = Guid.NewGuid();
        Type = type;
        Message = message;
        IsRead = false;
        CreatedAt = DateTime.UtcNow;
    }

    public void MarkAsRead()
    {
        IsRead = true;
    }
}
