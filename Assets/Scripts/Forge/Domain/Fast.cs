using UnityEngine;

namespace Forge.Domain
{
    
    [CreateAssetMenu(fileName = "Fast", menuName = "Forge/StatusEffect/Fast")]
    public class Fast : StatusEffect
    {
        [SerializeField] [Range(0, 5)]
        private float _reducedTime;

        public override void ApplyTo(Player player)
        {
            player.ChangeCraftingTimeReduction(-_reducedTime);
        }

        public override void RemoveFrom(Player player)
        {
            player.ChangeCraftingTimeReduction(_reducedTime);
        }
    }
}