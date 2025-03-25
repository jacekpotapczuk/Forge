using UnityEngine;

namespace Forge.Domain
{
    [CreateAssetMenu(fileName = "Lucky", menuName = "Forge/StatusEffect/Lucky")]
    public class Lucky : StatusEffect
    {
        [SerializeField] [Range(0, 1)]
        private float _luckPercentagePoints;

        public override void ApplyTo(Player player)
        {
            player.ChangeLuck(_luckPercentagePoints);
        }

        public override void RemoveFrom(Player player)
        {
            player.ChangeLuck(-_luckPercentagePoints);
        }
    }
}