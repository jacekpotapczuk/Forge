using System;
using System.Collections.Generic;
using Forge.Domain;
using UnityEngine;
using UnityEngine.UI;

namespace Forge.View
{
    [RequireComponent(typeof(Image))]
    public class MachineView : MonoBehaviour
    {
        public void Awake()
        {
            _image = GetComponent<Image>();
            _inputItemStacks = new List<ItemStackView>();
        }

        public void Initialize(Machine machine)
        {
            _machine = machine;
            UpdateGraphics();
        }

        private void UpdateGraphics()
        {
            _image.sprite = _machine.Template.Sprite;

            var childCount = _inputsHolder.childCount;

            for (var i = childCount - 1; i >= 0; i--)
            {
                GameObjectPool.Destroy(_inputsHolder.GetChild(i).gameObject);
            }
            
            _inputItemStacks.Clear();

            var inputCount = _machine.Template.GetNumberOfInputs();

            for (var i = 0; i < inputCount; i++)
            {
                var itemSTack = GameObjectPool.Get(_inputItemStackPrefab, _inputsHolder);
                itemSTack.Initialize(new ItemStack());
                _inputItemStacks.Add(itemSTack);
            }
            
            _outputItemStackView.Initialize(new ItemStack());
        }

        [SerializeField]
        private Transform _inputsHolder;
        
        [SerializeField]
        private ItemStackView _outputItemStackView;

        [SerializeField] 
        private ItemStackView _inputItemStackPrefab;

        private Machine _machine;
        private Image _image;
        private List<ItemStackView> _inputItemStacks;
    }
}