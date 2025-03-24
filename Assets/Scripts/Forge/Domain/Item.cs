namespace Forge.Domain
{
    public class Item
    {
        public ItemTemplate Template { get; }

        public Item(ItemTemplate itemTemplate)
        {
            Template = itemTemplate;
        }
    }
}