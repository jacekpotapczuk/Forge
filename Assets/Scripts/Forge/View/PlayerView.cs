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
        }

        [SerializeField] 
        private InventoryView _inventoryView;
        
        private Player _player;
    }
}
