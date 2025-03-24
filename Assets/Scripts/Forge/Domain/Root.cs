using System.Collections.Generic;
using UnityEngine;


namespace Forge.Domain
{
    public class Root : MonoBehaviour
    {
        [SerializeField] 
        private List<StartingItemTemplate> _staringItems;
        
        public void Start()
        {
            var playerInventory = new Inventory(8, 8);
            playerInventory.AddStartingItems(_staringItems);
            
            var player = new Player(playerInventory);
            
        }
    }
}