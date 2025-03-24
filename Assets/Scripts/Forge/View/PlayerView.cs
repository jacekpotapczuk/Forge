using System;
using Forge.Domain;
using UnityEngine;

namespace Forge.View
{
    public class PlayerView : MonoBehaviour
    {
        public Player Player => _player;

        public void Awake()
        {
            _player = _root.Player;

            if (_player == null)
            {
                throw new Exception($"Assigned Player for {nameof(PlayerView)} is null");
            }
        }
        
        [SerializeField] 
        private Root _root;

        private Player _player;
    }
}
