using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using Forge.Domain;
using UnityEngine;
using UnityEngine.UI;

namespace Forge.View
{
    /// <summary>
    /// View for <see cref="QuestJournal"/>
    /// </summary>
    [RequireComponent(typeof(VerticalLayoutGroup))]
    public class QuestJournalView : MonoBehaviour
    {
        public void Initialize(QuestJournal questJournal)
        {
            _questJournal = questJournal ?? throw new ArgumentNullException(nameof(questJournal));

            foreach (var quest in _questJournal.ActiveQuests)
            {
                OnQuestAdded(quest);    
            }

            _questJournal.ActiveQuests.CollectionChanged += OnActiveQuestsChanged;
        }

        public void OnDestroy()
        {
            _questJournal.ActiveQuests.CollectionChanged -= OnActiveQuestsChanged;
        }

        [SerializeField]
        private CraftingQuestView _questViewPrefab;
        
        private readonly Dictionary<CraftingQuest, CraftingQuestView> _quests = new ();
        
        private QuestJournal _questJournal;
        
        private void OnActiveQuestsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (var obj in e.NewItems)
                    {
                        var quest = obj as CraftingQuest;
                        OnQuestAdded(quest);
                    }

                    break;

                case NotifyCollectionChangedAction.Remove:
                    foreach (var obj in e.OldItems)
                    {
                        var quest = obj as CraftingQuest;
                        OnQuestRemoved(quest);
                    }

                    break;
                case NotifyCollectionChangedAction.Move:
                case NotifyCollectionChangedAction.Replace:
                case NotifyCollectionChangedAction.Reset:
                default:
                    //todo: implement
                    break;
            }
        }

        private void OnQuestAdded(CraftingQuest quest)
        {
            if (_quests.ContainsKey(quest))
            {
                throw new Exception("The same quest added multiple times");
            }

            var view = GameObjectPool.Get(_questViewPrefab, transform);
            _quests.Add(quest, view);
            view.Initialize(quest);
        }
        
        private void OnQuestRemoved(CraftingQuest quest)
        {
            var view = _quests[quest];
            GameObjectPool.Destroy(view.gameObject);
            _quests.Remove(quest);
        }
    }
}