using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using Forge.Domain;
using UnityEngine;
using UnityEngine.UI;

namespace Forge.View
{
    /// <summary>
    /// Handles List of all available <see cref="MachineView"/>
    /// </summary>
    [RequireComponent(typeof(VerticalLayoutGroup))]
    public class MachinesView : MonoBehaviour
    {
        public void Initialize(GameWorld gameWorld)
        {
            if (_gameWorld != null) 
            {
                _gameWorld.Machines.CollectionChanged -= OnMachinesChanged;
            }
            
            _gameWorld = gameWorld ?? throw new NullReferenceException(nameof(gameWorld));
            _gameWorld.Machines.CollectionChanged += OnMachinesChanged;

            foreach (var machine in gameWorld.Machines)
            {
                SpawnMachineView(machine);
            }
        }

        public void OnDestroy()
        {
            _gameWorld.Machines.CollectionChanged -= OnMachinesChanged;
        }
        
        [SerializeField] 
        private MachineView _machineViewPrefab;
        
        private readonly List<MachineView> _machineViews = new();
        
        private GameWorld _gameWorld;

        private void OnMachinesChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (var obj in e.NewItems)
                    {
                        var machine = obj as Machine;
                        SpawnMachineView(machine);
                    }

                    break;

                case NotifyCollectionChangedAction.Remove:
                    throw new NotImplementedException("Todo: implement functionality to remove machines");
                case NotifyCollectionChangedAction.Move:
                case NotifyCollectionChangedAction.Replace:
                case NotifyCollectionChangedAction.Reset:
                default:
                    // todo: implement
                    break;
            }
        }

        private void SpawnMachineView(Machine machine)
        {
            var machineView = GameObjectPool.Get(_machineViewPrefab, transform);
            machineView.Initialize(_gameWorld, machine);
            _machineViews.Add(machineView);
        }
    }
}