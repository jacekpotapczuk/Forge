using System;
using System.Collections;
using Forge.Domain;
using TMPro;
using UnityEngine;

namespace Forge.View
{
    /// <summary>
    /// View for <see cref="NotificationService"/>
    /// </summary>
    public class NotificationView : MonoBehaviour
    {
        public void Initialize(NotificationService notificationService)
        {
            _notificationText.text = string.Empty;
            
            if (_notificationService != null)
            {
                _notificationService.NotificationBroadcasted -= ShowNotification;
            }

            _notificationService = notificationService ?? throw new NullReferenceException(nameof(notificationService));
            _notificationService.NotificationBroadcasted += ShowNotification;
        }

        public void OnDestroy()
        {
            _notificationService.NotificationBroadcasted -= ShowNotification;
        }
        
        [SerializeField] 
        private TextMeshProUGUI _notificationText;
        
        private NotificationService _notificationService;

        private void ShowNotification(string message, float duration, NotificationType notificationType)
        {
            _notificationText.text = message;
            _notificationText.color = notificationType.GetColor();
            _notificationText.gameObject.SetActive(true);
            StopAllCoroutines();
            StartCoroutine(HideAfterDelay(duration));
        }
        
        private IEnumerator HideAfterDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            _notificationText.gameObject.SetActive(false);
        }
    }
}