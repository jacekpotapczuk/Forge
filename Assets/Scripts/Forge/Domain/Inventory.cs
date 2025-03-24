
using System.Collections.Generic;
using UnityEngine;

namespace Forge.Domain
{
    public class Inventory
    {
        public ItemStack[,] ItemStacks => _itemStacks;
        
        public Inventory(int columnsCount, int rowsCount)
        {
            _rowsCount = rowsCount;
            _columnsCount = columnsCount;
            
            _itemStacks = new ItemStack[_columnsCount, _rowsCount];
            Clear();
        }

        public void Clear()
        {
            _itemLocations = new Dictionary<Item, (int, int)>();
            
            for (var y = 0; y < _rowsCount; y++)
            {
                for (var x = 0; x < _columnsCount; x++)
                {
                    _itemStacks[y, x] = new ItemStack();
                }
            }
        }
        

        public void AddStartingItems(IReadOnlyList<StartingItemTemplate> startingItemTemplates)
        {
            foreach (var startingItemTemplate in startingItemTemplates)
            {
                var itemStack = startingItemTemplate.Generate();

                if (itemStack == null)
                {
                    continue;
                }

                var didAdd = TryAddItemStack(itemStack);

                if (!didAdd)
                {
                    Debug.LogError($"Can't add starting item {startingItemTemplate}."); 
                }
            }
        }

        public bool TryAddItem(Item item, int amount)
        {
            if (item == null)
            {
                return false;
            }
            
            if (_itemLocations.ContainsKey(item))
            {
                var (y, x) = _itemLocations[item];
                
                _itemStacks[y, x].Add(item, amount);
                return true;
            }

            // this could be cached but for any reasonable player inventory size this shouldn't be a problem
            for (var y = 0; y < _rowsCount; y++)
            {
                for (var x = 0; x < _columnsCount; x++)
                {
                    var stack = _itemStacks[y, x];

                    if (!stack.IsEmpty)
                    {
                        continue;
                    }
                    
                    stack.Add(item, amount);
                    return true;
                }
            }

            return false;
        }

        public bool TryAddItemStack(ItemStack itemStack)
        {
            if (itemStack == null || itemStack.IsEmpty)
            {
                return false;
            }

            return TryAddItem(itemStack.Item, itemStack.Amount);
        }

        public IEnumerator<ItemStack> GetEnumerator()
        {
            for (var y = 0; y < _rowsCount; y++)
            {
                for (var x = 0; x < _columnsCount; x++)
                {
                    var stack = _itemStacks[y, x];

                    yield return stack;
                }
            }
        }

        private readonly ItemStack[,] _itemStacks;
        private readonly int _rowsCount;
        private readonly int _columnsCount;
        
        private Dictionary<Item, (int, int)> _itemLocations;
    }
}
