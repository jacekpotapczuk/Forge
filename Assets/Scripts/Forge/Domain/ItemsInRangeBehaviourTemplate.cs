using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Forge.Domain
{
    /// <summary>
    /// Returns Amount between MinAmount and MaxAmount
    /// </summary>
    [CreateAssetMenu(fileName = "ItemsInRangeBehaviour", menuName = "Forge/SpawnBehaviour/ItemsInRangeBehaviour")]
    public class ItemsInRangeBehaviourTemplate : ItemSpawnBehaviourTemplate
    {
        public override int Generate()
        {
            if (_maxAmount < _minAmount)
            {
                throw new Exception($"{nameof(_maxAmount)} can't be smaller than {nameof(_minAmount)}");
            }
            
            return Random.Range(_minAmount, _maxAmount);
        }
        
        [SerializeField][Range(0, 999)]
        private int _minAmount = 0;
        
        [SerializeField][Range(0, 999)] // todo: editor tool to ensure Max > Min
        private int _maxAmount = 5;
    }
}