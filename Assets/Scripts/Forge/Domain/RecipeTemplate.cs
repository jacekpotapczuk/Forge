using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Forge.Domain
{
    [CreateAssetMenu(fileName = "RecipeTemplate", menuName = "Forge/RecipeTemplate")]
    public class RecipeTemplate : ScriptableObject
    {
        public List<ItemTemplate> InputItemTemplates => _inputItemTemplates;
        public ItemTemplate OutputItemTemplate => _outputTemplate;
        public float CompletionTime => _completionTime;
        public float SuccessChancePercentage => _successChancePercentage;
        
        [SerializeField]
        private List<ItemTemplate> _inputItemTemplates;
        
        [SerializeField]
        private ItemTemplate _outputTemplate;
        
        [SerializeField] [Range(1f, 10f)]
        private float _completionTime = 2f;

        [SerializeField] [Range(0, 1)]
        private float _successChancePercentage = 1f;

    }
}