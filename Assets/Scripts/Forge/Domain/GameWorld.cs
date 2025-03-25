using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Forge.Domain
{
    /// <summary>
    /// Stores current state of the world.
    /// </summary>
    public class GameWorld : IDisposable
    {
        public IReadOnlyList<Player> Players => _players;
        
        public ObservableCollection<Machine> Machines { get; private set; } = new();

        public NotificationService NotificationService { get; } = new();
        
        public void Dispose()
        {
            for (var i = _players.Count - 1; i >= 0; i--)
            {
                _players[i].Dispose();
            }
            
            _players.Clear();
            
            for (var i = Machines.Count - 1; i >= 0; i--)
            {
                Machines[i].Dispose();
            }
            
            Machines.Clear();
        }

        public void Update(float deltaTime)
        {
            for (var i = Machines.Count - 1; i >= 0; i--)
            {
                var machine = Machines[i];
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
        
        private readonly List<Player> _players = new();
    }
}