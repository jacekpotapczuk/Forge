using UnityEngine;

namespace Forge.Domain
{
    /// <summary>
    /// Defines how itme amount is generated
    /// </summary>
    public abstract class ItemSpawnBehaviourTemplate : ScriptableObject
    {
        public abstract int Generate();
    }
}