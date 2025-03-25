using System;
using System.Collections.Generic;

namespace Forge.Domain
{
    /// <summary>
    /// Defines Player
    /// </summary>
    public class Player : IDisposable
    {
        public Action<RecipeTemplate> RecipeCrafted;
        
        public Inventory Inventory { get; } 
        public StatusEffects StatusEffects { get; }
        
        public QuestJournal QuestJournal { get; }

        public float Luck { get; private set; } = 0f;

        public float CraftingTimeReduction { get; private set; } = 0f;
        
        public Player(GameWorld gameWorld, Inventory inventory, List<CraftingQuestTemplate> startingQuests)
        {
            _gameWorld = gameWorld ?? throw new NullReferenceException(nameof(gameWorld));
            Inventory = inventory ?? throw new NullReferenceException(nameof(inventory));
            
            StatusEffects = new StatusEffects(this);
            QuestJournal = new QuestJournal(this, startingQuests);
        }
        
        public void Dispose()
        {
            Inventory.Dispose();
            StatusEffects.Dispose();
            QuestJournal.Dispose();
        }

        public void ChangeLuck(float change) 
            => Luck += change;

        public void ChangeCraftingTimeReduction(float change) 
            => CraftingTimeReduction += change;

        public void NotifySuccessfulCraft(RecipeTemplate recipe) 
            => RecipeCrafted?.Invoke(recipe);

        public void UnlockMachine(MachineTemplate machineTemplate) 
            => _gameWorld.SpawnMachine(machineTemplate);
        
        private readonly GameWorld _gameWorld;
    }
}
