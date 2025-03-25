using System;
using Forge.Domain;
using TMPro;
using UnityEngine;

namespace Forge.View
{
    /// <summary>
    /// View for <see cref="CraftingQuest"/>
    /// </summary>
    [RequireComponent(typeof(TMP_Text))]
    public class CraftingQuestView : MonoBehaviour
    {
        private void Awake()
        {
            _text = GetComponent<TMP_Text>();
        }
        
        public void Initialize(CraftingQuest quest)
        {
            if (_quest != null)
            {
                _quest.ProgressMoved -= OnProgressMoved;
            }
            
            _quest = quest ?? throw new NullReferenceException(nameof(quest));
            _quest.ProgressMoved += OnProgressMoved;
            
            UpdateText();
        }

        public void OnDestroy()
        {
            _quest.ProgressMoved -= OnProgressMoved;
        }

        private void OnProgressMoved(CraftingQuest arg1, int arg2) 
            => UpdateText();

        private void UpdateText()
        {
            _text.text = $"{_quest.Template.Name} - Craft {_quest.Template.RequiredRecipe.OutputItemTemplate.Name}" +
                         $" {_quest.Template.Amount} times. (current: {_quest.Progress} / {_quest.Template.Amount}) " +
                         $"[Reward: {_quest.Template.MachineReward.Name}]";
        }

        private TMP_Text _text;
        private CraftingQuest _quest;
    }
}