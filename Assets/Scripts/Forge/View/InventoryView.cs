using Forge.Domain;
using UnityEngine;
using UnityEngine.UI;

namespace Forge.View
{
    [RequireComponent(typeof(GridLayoutGroup))]
    public class InventoryView : MonoBehaviour
    {
        [SerializeField]
        private PlayerView _playerView;

        [SerializeField] 
        private ItemStackView _itemStackViewPrefab;
        
        public void Start()
        {
            _gridLayoutGroup = GetComponent<GridLayoutGroup>();
            _inventory = _playerView.Player.Inventory;

            Clear();
            CreteView();
        }

        public void Clear()
        {
            var childCount = transform.childCount;

            for (var i = childCount - 1; i >= 0; i--)
            {
                GameObjectPool.Destroy(transform.GetChild(i).gameObject);
            }
        }
        
        private GridLayoutGroup _gridLayoutGroup;
        private Inventory _inventory;
        
        private void CreteView()
        {
            foreach (var itemStack in _inventory)
            {
                var itemStackView = GameObjectPool.Get(_itemStackViewPrefab, transform, worldPositionStays: false);
                itemStackView.Initialize(itemStack);
            }
        }
    }
}