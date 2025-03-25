using System.Collections.Generic;
using UnityEngine;

namespace Forge.Domain
{
    /// <summary>
    /// Defines Template for Items
    /// </summary>
    [CreateAssetMenu(fileName = "ItemTemplate", menuName = "Forge/ItemTemplate")]
    public class ItemTemplate : ScriptableObject
    {
        public Sprite Sprite => _sprite;
        public string Name => _name;
        public string Description => _description;
        public List<StatusEffect> StatusEffects => _statusEffects;
        
        [SerializeField]
        private Sprite _sprite;
        
        [SerializeField]
        private string _name;
        
        [SerializeField]
        private string _description;

        [SerializeField] 
        private List<StatusEffect> _statusEffects;
    }
}