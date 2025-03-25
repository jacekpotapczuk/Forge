using UnityEngine;

namespace Forge.Domain
{
    /// <summary>
    /// Status effects that changes how lucky Player is
    /// </summary>
    [CreateAssetMenu(fileName = "Lucky", menuName = "Forge/StatusEffect/Lucky")]
    public class Lucky : StatusEffect
    {
        public override void ApplyTo(Player player) 
            => player.ChangeLuck(_luckPercentagePoints);

        public override void RemoveFrom(Player player) 
            => player.ChangeLuck(-_luckPercentagePoints);
        
        [SerializeField] [Range(0, 1)]
        private float _luckPercentagePoints;
    }
}