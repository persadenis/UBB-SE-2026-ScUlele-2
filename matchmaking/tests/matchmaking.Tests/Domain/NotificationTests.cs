using matchmaking.Domain;

namespace matchmaking.Tests.Domain;

public class NotificationTests
{
    [Fact]
    public void Constructor_GivenNewNotification_ShouldInitializeUnreadNotificationWithUtcTimestamp()
    {
        var before = DateTime.UtcNow;

        var notification = new Notification(
            recipientId: 7,
            fromId: 3,
            notificationType: NotificationType.MATCH,
            title: "It's a match!",
            description: "You matched with Alex");

        var after = DateTime.UtcNow;

        Assert.Equal(7, notification.RecipientId);
        Assert.Equal(3, notification.FromId);
        Assert.Equal(NotificationType.MATCH, notification.Type);
        Assert.Equal("It's a match!", notification.Title);
        Assert.Equal("You matched with Alex", notification.Description);
        Assert.False(notification.IsRead);
        Assert.Equal(DateTimeKind.Utc, notification.CreatedAt.Kind);
        Assert.InRange(notification.CreatedAt, before, after);
    }

    [Fact]
    public void Constructor_GivenPersistedValues_ShouldPreserveProvidedState()
    {
        var createdAt = new DateTime(2025, 11, 5, 14, 30, 0, DateTimeKind.Utc);

        var notification = new Notification(
            notificationId: 11,
            recipientId: 5,
            fromId: 8,
            notificationType: NotificationType.SUPER_LIKE,
            isRead: true,
            createdAt: createdAt,
            title: "Super-Like!",
            description: "Taylor sent you a super like.");

        Assert.Equal(11, notification.NotificationId);
        Assert.Equal(5, notification.RecipientId);
        Assert.Equal(8, notification.FromId);
        Assert.Equal(NotificationType.SUPER_LIKE, notification.Type);
        Assert.True(notification.IsRead);
        Assert.Equal(createdAt, notification.CreatedAt);
        Assert.Equal("Super-Like!", notification.Title);
        Assert.Equal("Taylor sent you a super like.", notification.Description);
    }
}
