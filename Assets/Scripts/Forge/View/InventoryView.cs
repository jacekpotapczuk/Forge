using System;
using Forge.Domain;
using UnityEngine;
using UnityEngine.UI;

namespace Forge.View
{
    /// <summary>
    /// View for <see cref="Inventory"/>
    /// </summary>
    [RequireComponent(typeof(GridLayoutGroup))]
    public class InventoryView : MonoBehaviour
    {
        [SerializeField] 
        private ItemStackView _itemStackViewPrefab;
        
        public void Start()
        {
            _gridLayoutGroup = GetComponent<GridLayoutGroup>();
            
            Clear();
            CreteView();
        }

        public void Initialize(Inventory inventory)
        {
            _inventory = inventory ?? throw new NullReferenceException(nameof(inventory));
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