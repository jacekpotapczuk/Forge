using UnityEngine;
using UnityEngine.Serialization;

namespace Forge.Domain
{
    [CreateAssetMenu(fileName = "Crafting Quest Template", menuName = "Forge/Crafting Quest")]
    public class CraftingQuestTemplate : ScriptableObject
    {
        public string Name => _name;
        public RecipeTemplate RequiredRecipe => _requiredRecipe;
        public int Amount => _amount;
        public MachineTemplate MachineReward => _machineReward;
        
        public CraftingQuestTemplate NextQuest => _nextQuest;
        
        
        [SerializeField] 
        private string _name;
        
        [FormerlySerializedAs("requiredRecipe")] [FormerlySerializedAs("_completedRecipe")] [SerializeField] 
        private RecipeTemplate _requiredRecipe;
        
        [SerializeField] [Range(1, 10)] 
        private int _amount;

        [SerializeField] 
        private MachineTemplate _machineReward;
        
        [SerializeField] // could be used to create quest chains
        private CraftingQuestTemplate _nextQuest;
    }
}