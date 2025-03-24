using System;
using JetBrains.Annotations;

namespace Forge.Domain
{
    public class ItemStack
    {
        public Action Changed;
        
        public bool IsEmpty => Item == null;
        
        [CanBeNull]
        public Item Item { get; private set; }
        public int Amount { get; private set; }

        public ItemStack()
        {
            Item = null;
            Amount = 0;
        }

        public ItemStack(Item item, int amount)
        {
            Item = item;

            if (amount < 0)
            {
                throw new Exception("Item stack value can't be less than 0");
            }
            
            Amount = amount;
        }

        public void Add(Item item, int amount)
        {
            if (Item != null && item.Template != Item.Template)
            {
                throw new Exception($"Can't add item of different template to {nameof(ItemStack)}");
            }

            Item ??= item;
            Amount += amount;
            Changed?.Invoke();
        }

        public void Remove()
        {
            if (Item == null || Amount <= 0)
            {
                throw new Exception("There is nothing to remove!");
            }

            Amount -= 1;

            if (Amount == 0)
            {
                Clear();
            }
            
            Changed?.Invoke();
        }
        
        private void Clear()
        {
            Item = null;
        }
    }
}