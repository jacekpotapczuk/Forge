using UnityEngine;

namespace Forge.Domain
{
    /// <summary>
    /// Base Status effect class
    /// </summary>
    public abstract class StatusEffect : ScriptableObject
    {
        public string Name => _name;
        public abstract void ApplyTo(Player player);
        public abstract void RemoveFrom(Player player);

        [SerializeField]
        private string _name;
    }
}