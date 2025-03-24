
namespace Forge.Domain
{
    public class Player
    {
        public Inventory Inventory { get; } 
        
        public Player(Inventory inventory)
        {
            Inventory = inventory;
        }
    }
}
