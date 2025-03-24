using System.Collections.Generic;
using UnityEngine;

namespace Forge.Domain
{
    [CreateAssetMenu(fileName = "MachineTemplate", menuName = "Forge/MachineTemplate")]
    public class MachineTemplate : ScriptableObject
    {
        public Sprite Sprite => _sprite;
        public string Name => _name;
        public string Description => _description;
        public List<RecipeTemplate> RecipeTemplates => _recipeTemplates;

        public int GetNumberOfInputs()
        {
            // todo: can be cached. Or should be constant for a given machine. If constant some editor tool would be 
            // usefull to make sure recipes have valid number of inputs.
            var count = 0;

            foreach (var recipe in _recipeTemplates)
            {
                if (recipe.InputItemTemplates.Count > count)
                {
                    count = recipe.InputItemTemplates.Count;
                }
            }

            return count;
        }
        
        [SerializeField] 
        private Sprite _sprite;
        
        [SerializeField]
        private string _name;
        
        [SerializeField]
        private string _description;

        [SerializeField] 
        private List<RecipeTemplate> _recipeTemplates;
    }
}