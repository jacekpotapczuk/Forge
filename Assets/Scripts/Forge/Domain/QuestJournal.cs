using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Forge.Domain
{
    /// <summary>
    /// Defines Player's Quest Journal
    /// </summary>
    public class QuestJournal : IDisposable
    {
        public Action<CraftingQuest> QuestCompleted;

        public ObservableCollection<CraftingQuest> ActiveQuests { get; } = new ();

        public QuestJournal(Player player, List<CraftingQuestTemplate> startingCraftingQuestsTemplates)
        {
            _player = player ?? throw new ArgumentNullException(nameof(player));
            
            foreach (var craftingQuestTemplate in startingCraftingQuestsTemplates)
            {
                AddQuest(craftingQuestTemplate);
            }

            player.RecipeCrafted += OnRecipeCrafted;
        }
        
        public void Dispose()
        {
            _player.RecipeCrafted -= OnRecipeCrafted;

            foreach (var q in ActiveQuests)
            {
                q.Completed -= OnQuestCompleted;
            }
            
            ActiveQuests.Clear();
        }
        
        private readonly Player _player;

        private void OnQuestCompleted(CraftingQuest quest)
        {
            ActiveQuests.Remove(quest);
            quest.Completed -= OnQuestCompleted;
            QuestCompleted?.Invoke(quest);

            if (quest.Template.NextQuest != null)
            {
                AddQuest(quest.Template.NextQuest);
            }
        }

        private void AddQuest(CraftingQuestTemplate template)
        {
            var quest = new CraftingQuest(template, _player);
            ActiveQuests.Add(quest);
            quest.Completed += OnQuestCompleted;
        }

        private void OnRecipeCrafted(RecipeTemplate recipe)
        {
            // go from behind since we might delete quests while iterating
            for (var i = ActiveQuests.Count - 1; i >= 0; i--) 
            {
                var quest = ActiveQuests[i];
                if (quest.Template.RequiredRecipe == recipe)
                {
                    quest.ProgressQuest();
                }
            }
        }
    }
}