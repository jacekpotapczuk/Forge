using System.Collections.Generic;
using UnityEngine;


namespace Forge.Domain
{
    public class Root : MonoBehaviour
    {
        [SerializeField] 
        private List<StartingItemTemplate> _staringItems;

        public Player Player { get; private set; }
        
        public void Awake()
        {
            var playerInventory = new Inventory(8, 8);
            playerInventory.AddStartingItems(_staringItems);
            
            var player = new Player(playerInventory);
            Player = player;
        }
    }
}