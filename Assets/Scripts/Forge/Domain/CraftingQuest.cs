using System;

namespace Forge.Domain
{
    /// <summary>
    /// Stores quest and its progress
    /// </summary>
    public class CraftingQuest
    {
        public Action<CraftingQuest> Completed;
        public Action<CraftingQuest, int> ProgressMoved;
        public CraftingQuestTemplate Template => _template;
        public int Progress => _progress;
        
        public CraftingQuest(CraftingQuestTemplate template, Player player)
        {
            _template = template ?? throw new ArgumentNullException(nameof(template));
            _player = player ?? throw new ArgumentNullException(nameof(player));
        }

        public void ProgressQuest(int amount = 1)
        {
            _progress += 1;
            ProgressMoved.Invoke(this, _progress);

            if (_progress >= _template.Amount)
            {
                _player.UnlockMachine(_template.MachineReward);
                Completed?.Invoke(this);
            }
        }
        
        private readonly CraftingQuestTemplate _template;
        private readonly Player _player;
        
        private int _progress = 0;

    }
}