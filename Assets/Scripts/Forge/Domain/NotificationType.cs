using System;
using UnityEngine;

namespace Forge.Domain
{
    /// <summary>
    /// Notification type
    /// </summary>
    public enum NotificationType
    {
        Info = 0,
        Error = 1,
    }
    
    public static class NotificationTypeExtensionMethods
    {
        public static Color GetColor(this NotificationType type)
        {
            switch (type)
            {
                case NotificationType.Info:
                    return Color.white;
                case NotificationType.Error:
                    return Color.red;
                default:
                    throw new NotImplementedException();
            }
        }
    }
}