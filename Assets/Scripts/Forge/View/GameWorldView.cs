using System;
using System.Collections.Generic;
using Forge.Domain;
using UnityEngine;

namespace Forge.View
{
    public class GameWorldView : MonoBehaviour
    {
        public GameWorld GameWorld => _gameWorld;

        public void Awake()
        {
            _playerViews = new List<PlayerView>();
            _gameWorld = _root.GameWorld;

            if (_gameWorld == null)
            {
                throw new NullReferenceException();
            }
        }

        public void Start()
        {
            // todo: actually handle multiple players on ui side
            foreach (var player in _gameWorld.Players)
            {
                var playerView = GameObjectPool.Get<PlayerView>(_playerViewPrefab, transform);    
                playerView.Initialize(player);
                _playerViews.Add(playerView);
            }

            _machinesView.Initialize(_gameWorld);
        }

        [SerializeField] 
        private Root _root;

        [SerializeField]
        private MachinesView _machinesView; 

        [SerializeField] 
        private PlayerView _playerViewPrefab;


        private List<PlayerView> _playerViews;
        
        private GameWorld _gameWorld;
    }
}