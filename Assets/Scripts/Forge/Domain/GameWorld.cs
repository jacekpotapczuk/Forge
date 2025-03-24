using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Forge.Domain
{
    public class GameWorld
    {
        public IReadOnlyList<Player> Players => _players;
        
        public ObservableCollection<Machine> Machines { get; private set; }
        
        public GameWorld()
        {
            _players = new List<Player>();

            Machines = new ObservableCollection<Machine>();
        }

        public void AddPlayer(Player player)
        {
            if (_players.Contains(player))
            {
                throw new Exception($"Player {player} is already in the game world");
            }
            
            _players.Add(player);
        }

        public void SpawnMachine(MachineTemplate machineTemplate)
        {
            var machine = new Machine(machineTemplate);
            Machines.Add(machine);
        }

        private List<Player> _players;
        

    }
}