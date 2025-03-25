using UnityEngine;

namespace Forge.Domain
{
    /// <summary>
    /// Defines how item amount is generated
    /// </summary>
    public abstract class ItemSpawnBehaviourTemplate : ScriptableObject
    {
        public abstract int Generate();
    }
}