using System;

namespace Forge.Domain
{
    public class Player
    {
        public Inventory Inventory { get; } 
        public StatusEffects StatusEffects { get; }

        public float Luck { get; private set; } = 0f;

        public float CraftingTimeReduction { get; private set; } = 0f;
        
        public Player(GameWorld gameWorld, Inventory inventory)
        {
            _gameWorld = gameWorld ?? throw new NullReferenceException(nameof(gameWorld));
            Inventory = inventory ?? throw new NullReferenceException(nameof(inventory));
            
            StatusEffects = new StatusEffects(this);
        }

        public void ChangeLuck(float change)
        {
            Luck += change;
        }

        public void ChangeCraftingTimeReduction(float change)
        {
            CraftingTimeReduction += change;
        }

        private GameWorld _gameWorld;
    }
}
