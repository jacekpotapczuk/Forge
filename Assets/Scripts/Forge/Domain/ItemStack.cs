using System;
using JetBrains.Annotations;

namespace Forge.Domain
{
    public class ItemStack
    {
        public bool IsEmpty => Item == null;
        
        [CanBeNull]
        public Item Item { get; private set; }
        public int Count { get; private set; }

        public ItemStack()
        {
            Item = null;
            Count = 0;
        }

        public ItemStack(Item item, int count)
        {
            Item = item;

            if (count < 0)
            {
                throw new Exception("Item stack value can't be less than 0");
            }
            
            Count = count;
        }

        public void Add(Item item)
        {
            if (Item != null && item.Template != Item.Template)
            {
                throw new Exception($"Can't add item of different template to {nameof(ItemStack)}");
            }

            Item ??= item;
            Count += 1;
        }

        public void Remove()
        {
            if (Item == null || Count <= 0)
            {
                throw new Exception("There is nothing to remove!");
            }

            Count -= 1;

            if (Count == 0)
            {
                Clear();
            }
        }
        
        private void Clear()
        {
            Item = null;
        }
    }
}