using System;
using System.Collections.Generic;
using UnityEngine;


namespace Forge.Domain
{
    public class Root : MonoBehaviour
    {
        [SerializeField] 
        private List<StartingItemTemplate> _staringItems;

        [SerializeField] 
        private List<MachineTemplate> _startingMachines;
        
        public GameWorld GameWorld { get; private set; }
        
        public void Awake()
        {
            GameWorld = new GameWorld();
            
            // todo: obviously column/row count can be extracted here, but the game doesn't handle
            // resizing inventory on graphical side very well so I keep them as constants here 
            var playerInventory = new Inventory(4, 4);
            var player = new Player(GameWorld, playerInventory);
            
            GameWorld.AddPlayer(player);

            foreach (var machineTemplate in _startingMachines)
            {
                GameWorld.SpawnMachine(machineTemplate);
            }
            
            playerInventory.AddStartingItems(_staringItems);
        }

        public void Update()
        {
            GameWorld.Update(Time.deltaTime);
        }
    }
}