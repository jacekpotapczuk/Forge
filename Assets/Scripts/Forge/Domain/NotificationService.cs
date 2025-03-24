using System;
using UnityEngine;

namespace Forge.Domain
{
    public class NotificationService
    {
        public Action<string, float, NotificationType> NotificationBroadcasted;
        
        public void ShowNotification(string message, float duration = 2f, NotificationType type = NotificationType.Info)
        {
            NotificationBroadcasted?.Invoke(message, duration, type);
        }
    }
}