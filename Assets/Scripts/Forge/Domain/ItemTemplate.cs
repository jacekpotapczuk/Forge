using UnityEngine;

namespace Forge.Domain
{
    [CreateAssetMenu(fileName = "ItemTemplate", menuName = "Forge/ItemTemplate")]
    public class ItemTemplate : ScriptableObject
    {
        public Sprite Sprite;
        public string Name;
        public string Description;
    }
}