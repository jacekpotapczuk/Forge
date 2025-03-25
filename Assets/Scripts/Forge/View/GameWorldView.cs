using System;
using System.Collections.Generic;
using Forge.Domain;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace Forge.View
{
    /// <summary>
    /// View for <see cref="GameWorld"/>
    /// </summary>
    public class GameWorldView : MonoBehaviour
    {
        public GameWorld GameWorld => _gameWorld;

        public void Awake()
        {
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
            _notificationView.Initialize(_gameWorld.NotificationService);
        }

        [SerializeField] 
        private Root _root;

        [SerializeField]
        private MachinesView _machinesView; 

        [SerializeField] 
        private PlayerView _playerViewPrefab;

        [SerializeField] 
        private NotificationView _notificationView;


        private readonly List<PlayerView> _playerViews = new();
        private GameWorld _gameWorld;
    }
}