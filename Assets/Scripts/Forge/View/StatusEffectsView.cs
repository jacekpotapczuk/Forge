using System;
using System.Collections.Generic;
using Forge.Domain;
using UnityEngine;
using UnityEngine.UI;

namespace Forge.View
{
    /// <summary>
    /// View for <see cref="StatusEffect"/>
    /// </summary>
    [RequireComponent(typeof(VerticalLayoutGroup))]
    public class StatusEffectsView : MonoBehaviour
    {
        public void Initialize(StatusEffects statusEffects)
        {
            _statusEffects = statusEffects ?? throw new NullReferenceException(nameof(statusEffects));

            _statusEffects.StatusEffectUpdated += OnStatusEffectUpdated;

            foreach (var statusEffectPair in _statusEffects.StatusEffectByProviders)
            {
                OnStatusEffectUpdated(statusEffectPair.Key, statusEffectPair.Value);
            }
        }

        public void OnDestroy()
        {
            _statusEffects.StatusEffectUpdated -= OnStatusEffectUpdated;
        }
        
        [SerializeField] 
        private StatusEffectView _statusEffectPrefab;
        
        private readonly Dictionary<StatusEffect, StatusEffectView> _statusEffectViews = new();
        
        private StatusEffects _statusEffects;
        

        private void OnStatusEffectUpdated(StatusEffect statusEffect, HashSet<Item> items)
        {
            // status effect removed
            if (items.Count == 0)
            {
                GameObjectPool.Destroy(_statusEffectViews[statusEffect].gameObject);
                _statusEffectViews.Remove(statusEffect);
                return;
            }
            
            
            if (_statusEffectViews.ContainsKey(statusEffect))
            {
                _statusEffectViews[statusEffect].Initialize(statusEffect, items);
            }
            else
            {
                var view = GameObjectPool.Get(_statusEffectPrefab, transform);
                view.Initialize(statusEffect, items);
                _statusEffectViews.Add(statusEffect, view);
            }
        }
    }
}