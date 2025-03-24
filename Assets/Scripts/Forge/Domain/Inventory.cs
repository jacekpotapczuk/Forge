
using System.Collections.Generic;

namespace Forge.Domain
{
    public class Inventory
    {
        public Inventory(int columnsCount, int rowsCount)
        {
            _rowsCount = rowsCount;
            _columnsCount = columnsCount;
            
            _itemStacks = new ItemStack[_columnsCount, _rowsCount];
            _itemLocations = new Dictionary<Item, (int, int)>();
            
            for (var y = 0; y < _rowsCount; y++)
            {
                for (var x = 0; x < _columnsCount; x++)
                {
                    _itemStacks[y, x] = new ItemStack();
                }
            }
        }

        public bool TryAddItem(Item item)
        {
            if (_itemLocations.ContainsKey(item))
            {
                var (y, x) = _itemLocations[item];
                
                _itemStacks[y, x].Add(item);
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
                    
                    stack.Add(item);
                    return true;
                }
            }

            return false;
        }

        private readonly ItemStack[,] _itemStacks;
        private readonly Dictionary<Item, (int, int)> _itemLocations;
        private readonly int _rowsCount;
        private readonly int _columnsCount;
    }
}
