using UnityEngine;

namespace Forge.Domain
{
    /// <summary>
    /// Status effects that changes how fast Player crafts
    /// </summary>
    [CreateAssetMenu(fileName = "Fast", menuName = "Forge/StatusEffect/Fast")]
    public class Fast : StatusEffect
    {
        public override void ApplyTo(Player player) 
            => player.ChangeCraftingTimeReduction(-_reducedTime);

        public override void RemoveFrom(Player player) 
            => player.ChangeCraftingTimeReduction(_reducedTime);
        
        [SerializeField] [Range(0, 5)]
        private float _reducedTime;
    }
}