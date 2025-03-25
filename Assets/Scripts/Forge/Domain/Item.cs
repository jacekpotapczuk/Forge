using System;

namespace Forge.Domain
{
    /// <summary>
    /// Represents Item instance
    /// </summary>
    public class Item : IDisposable
    {
        public ItemTemplate Template { get; }

        public Item(ItemTemplate itemTemplate)
        {
            Template = itemTemplate ?? throw new NullReferenceException(nameof(itemTemplate));
        }

        public void Dispose()
        {
        }
    }
}