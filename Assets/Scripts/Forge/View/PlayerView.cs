using System;
using Forge.Domain;
using UnityEngine;

namespace Forge.View
{
    /// <summary>
    /// View for <see cref="Player"/>
    /// </summary>
    public class PlayerView : MonoBehaviour
    {
        public void Initialize(Player player)
        {
            _player = player ?? throw new NullReferenceException(nameof(player));
            
            _inventoryView.Initialize(player.Inventory);
            _statusEffectsView.Initialize(player.StatusEffects);
            _questJournalView.Initialize(player.QuestJournal);
        }

        [SerializeField] 
        private InventoryView _inventoryView;

        [SerializeField] 
        private StatusEffectsView _statusEffectsView;
        
        [SerializeField] 
        private QuestJournalView _questJournalView;
        
        private Player _player;
    }
}
