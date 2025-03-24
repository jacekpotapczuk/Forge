using UnityEngine;

namespace Forge.Domain
{
    /// <summary>
    /// Defines starting Item for player
    /// </summary>
    [CreateAssetMenu(fileName = "StaringItem", menuName = "Forge/StaringItem")]
    public class StartingItemTemplate : ScriptableObject
    {
        public ItemTemplate ItemTemplate => _itemTemplate;
        public ItemSpawnBehaviourTemplate SpawnBehaviourTemplate => _spawnBehaviourTemplate;
        
        public ItemStack Generate()
        {
            var amount = _spawnBehaviourTemplate.Generate();

            if (amount <= 0)
            {
                return null;
            }

            var item = new Item(_itemTemplate);

            return new ItemStack(item, amount);
        }
        
        [SerializeField]
        private ItemTemplate _itemTemplate;
        
        [SerializeField]
        private ItemSpawnBehaviourTemplate _spawnBehaviourTemplate;
    }
}