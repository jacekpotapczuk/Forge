using UnityEngine;

namespace Forge.Domain
{
    public abstract class StatusEffect : ScriptableObject
    {
        public string Name => _name;
        public abstract void ApplyTo(Player player);
        public abstract void RemoveFrom(Player player);

        [SerializeField]
        private string _name;
    }
}