
namespace Forge.Domain
{
    public class Player
    {
        public Inventory Inventory { get; } 
        
        public Player(GameWorld gameWorld, Inventory inventory)
        {
            Inventory = inventory;
            _gameWorld = gameWorld;
        }

        private GameWorld _gameWorld;
    }
}
