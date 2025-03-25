using System;
using System.Collections.Generic;

namespace Forge.Domain
{
    /// <summary>
    /// Manages all <see cref="StatusEffect"/> that affect <see cref="Player"/>
    /// </summary>
    public class StatusEffects : IDisposable
    {
        public Action<StatusEffect, HashSet<Item>> StatusEffectUpdated;
        
        public IReadOnlyDictionary<StatusEffect, HashSet<Item>> StatusEffectByProviders => _statusEffectByProviders;
        
        public StatusEffects(Player player)
        {
            _player = player ?? throw new NullReferenceException(nameof(player));

            _player.Inventory.NewItemAdded += AddFrom;
            _player.Inventory.ItemCleared += RemoveFrom;
        }
        
        public void Dispose()
        {
            _player.Inventory.NewItemAdded -= AddFrom;
            _player.Inventory.ItemCleared -= RemoveFrom;
        }

        private readonly Dictionary<StatusEffect, HashSet<Item>> _statusEffectByProviders = new ();
        private readonly Player _player;
        
        private void AddFrom(Item item)
        {
            foreach (var statusEffect in item.Template.StatusEffects)
            {
                if (_statusEffectByProviders.ContainsKey(statusEffect))
                {
                    _statusEffectByProviders[statusEffect].Add(item);
                    StatusEffectUpdated?.Invoke(statusEffect, _statusEffectByProviders[statusEffect]);
                }
                else
                {
                    var providers = new HashSet<Item>();
                    providers.Add(item);
                    _statusEffectByProviders.Add(statusEffect, providers);
                    statusEffect.ApplyTo(_player);
                    StatusEffectUpdated?.Invoke(statusEffect, _statusEffectByProviders[statusEffect]);
                }
            }
        }

        private void RemoveFrom(Item item)
        {
            foreach (var statusEffect in item.Template.StatusEffects)
            {
                if (!_statusEffectByProviders.ContainsKey(statusEffect))
                {
                    throw new Exception($"{item} {statusEffect} not present in player status effects. This should not happen");
                    
                }
                
                _statusEffectByProviders[statusEffect].Remove(item);

                var count = _statusEffectByProviders[statusEffect].Count;

                if (count == 0)
                {
                    statusEffect.RemoveFrom(_player);
                    _statusEffectByProviders.Remove(statusEffect);
                    
                    StatusEffectUpdated?.Invoke(statusEffect, new HashSet<Item>());
                }
                else
                {
                    StatusEffectUpdated?.Invoke(statusEffect, _statusEffectByProviders[statusEffect]);    
                }
            }
        }
    }
}