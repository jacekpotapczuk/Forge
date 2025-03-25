using System;
using System.Collections.Generic;
using UnityEngine;

namespace Forge.Domain
{
    /// <summary>
    /// Starting point for all gameplay logic
    /// </summary>
    public class Root : MonoBehaviour
    {
        public GameWorld GameWorld { get; private set; }
        
        public void Awake()
        {
            GameWorld = new GameWorld();
            
            // todo: obviously column/row count can be extracted here, but the game doesn't handle
            // resizing inventory on graphical side very well so I keep them as constants here 
            var playerInventory = new Inventory(4, 4);
            var player = new Player(GameWorld, playerInventory, _startingQuests);
            
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

        public void OnDestroy()
        {
            GameWorld.Dispose();
        }

        [SerializeField] 
        private List<StartingItemTemplate> _staringItems;

        [SerializeField] 
        private List<MachineTemplate> _startingMachines;
        
        [SerializeField] 
        private List<CraftingQuestTemplate> _startingQuests;
    }
}