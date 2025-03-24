using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Forge.Domain
{
    public class GameWorld
    {
        public IReadOnlyList<Player> Players => _players;
        
        public ObservableCollection<Machine> Machines { get; private set; }
        
        public NotificationService NotificationService { get; }
        
        public GameWorld()
        {
            _players = new List<Player>();
            Machines = new ObservableCollection<Machine>();
            NotificationService = new NotificationService();
        }

        public void Update(float deltaTime)
        {
            foreach (var machine in Machines)
            {
                machine.Update(deltaTime);
            }
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
            var machine = new Machine(this, machineTemplate);
            Machines.Add(machine);
        }
        
        private List<Player> _players;
    }
}