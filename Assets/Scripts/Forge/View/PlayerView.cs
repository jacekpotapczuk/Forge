using Forge.Domain;
using UnityEngine;

namespace Forge.View
{
    public class PlayerView : MonoBehaviour
    {
        public void Initialize(Player player)
        {
            _player = player;
            
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
