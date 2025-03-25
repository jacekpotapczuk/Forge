
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Forge.Domain
{
    /// <summary>
    /// Stores Player's Inventory
    /// </summary>
    public class Inventory : IDisposable
    {
        public Action<Item> NewItemAdded;
        public Action<Item> ItemCleared;
        
        public Inventory(int columnsCount, int rowsCount)
        {
            _rowsCount = rowsCount;
            _columnsCount = columnsCount;
            
            _itemStacks = new ItemStack[_columnsCount, _rowsCount];
            Clear();
        }
        
        public void Dispose()
        {
            for (var y = 0; y < _rowsCount; y++)
            {
                for (var x = 0; x < _columnsCount; x++)
                {
                    if (_itemStacks[y, x] != null)
                    {
                        _itemStacks[y, x].Cleared -= OnItemStackCleared;
                        _itemStacks[y, x].NewItemAdded -= OnNewItemAdded;
                    }
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
        
        private void Clear()
        {
            for (var y = 0; y < _rowsCount; y++)
            {
                for (var x = 0; x < _columnsCount; x++)
                {
                    if (_itemStacks[y, x] != null)
                    {
                        _itemStacks[y, x].Cleared -= OnItemStackCleared;
                        _itemStacks[y, x].NewItemAdded -= OnNewItemAdded;
                    }
                    
                    _itemStacks[y, x] = new ItemStack();
                    _itemStacks[y, x].Cleared += OnItemStackCleared;
                    _itemStacks[y, x].NewItemAdded += OnNewItemAdded;
                }
            }
        }

        private void OnNewItemAdded(Item item) 
            => NewItemAdded?.Invoke(item);

        private void OnItemStackCleared(Item clearedItem)
            => ItemCleared?.Invoke(clearedItem);

        private bool TryAddItem(Item item, int amount)
        {
            if (item == null)
            {
                return false;
            }

            // this could be cached but for any reasonable player inventory size this shouldn't be a problem
            for (var y = 0; y < _rowsCount; y++)
            {
                for (var x = 0; x < _columnsCount; x++)
                {
                    var stack = _itemStacks[y, x];
                    if (stack.Item == item)
                    {
                        _itemStacks[y, x].Add(item, amount);
                    }
                }
            }
            
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
    }
}
