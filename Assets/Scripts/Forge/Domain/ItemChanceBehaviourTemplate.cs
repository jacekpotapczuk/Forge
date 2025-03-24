using UnityEngine;

namespace Forge.Domain
{
    /// <summary>
    /// Given Percent Chance returns either Amount or zero.
    /// </summary>
    [CreateAssetMenu(fileName = "ItemChanceBehaviour",  menuName = "Forge/SpawnBehaviour/ItemChanceBehaviour")]
    public class ItemChanceBehaviourTemplate : ItemSpawnBehaviourTemplate
    {
        public override int Generate()
        {
            if (_percentChance >= 1f)
            {
                return _amount;
            }

            var rand = Random.Range(0f, 1f);

            if (rand <= _percentChance)
            {
                return _amount;
            }

            return 0;
        }
        
        
        [SerializeField][Range(1, 999)] 
        private int _amount = 1;

        [SerializeField][Range(0, 1)] 
        private float _percentChance = 1f;
    }
}