using System;
using System.Collections.Generic;
using Forge.Domain;
using TMPro;
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
            _forgeButton.onClick.AddListener(OnForgeButtonClicked);
            _progressBarHolder.SetActive(false);
        }

        public void OnDisable()
        {
            _forgeButton.onClick.RemoveListener(OnForgeButtonClicked);
        }

        private void OnForgeButtonClicked()
        {
            if (_machine == null)
            {
                return;
            }
            
            _machine.TryProcess(_gameWorld.Players[0]);
        }

        public void Initialize(GameWorld gameWorld, Machine machine)
        {
            _gameWorld = gameWorld ?? throw new NullReferenceException(nameof(gameWorld));
            _machine = machine ?? throw new NullReferenceException(nameof(machine));
            UpdateGraphics();
            _machine.ProcessingStarted += OnProcessingStarted;
            _machine.ProcessingEnded += OnProcessingEnded;
        }

        public void Update()
        {
            if (!_shouldUpdateProgressBar)
            {
                return;
            }

            _progressBar.fillAmount = 1f - (_machine.TimeUntilCompletion / (_machine.ProceedRecipe.CompletionTime + _machine.Crafter.CraftingTimeReduction));
        }

        private void OnProcessingEnded()
        {
            _progressBarHolder.gameObject.SetActive(false);
            _shouldUpdateProgressBar = false;
        }

        private bool _shouldUpdateProgressBar = false;

        private void OnProcessingStarted()
        {
            _progressBarHolder.SetActive(true);
            _shouldUpdateProgressBar = true;
        }

        public void OnDestroy()
        {
            _machine.ProcessingStarted -= OnProcessingStarted;
            _machine.ProcessingEnded -= OnProcessingEnded;
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

            foreach (var input in _machine.Inputs)
            {
                var itemSTack = GameObjectPool.Get(_inputItemStackPrefab, _inputsHolder);
                itemSTack.Initialize(input);
                _inputItemStacks.Add(itemSTack);
            }
            
            _outputItemStackView.Initialize(_machine.Output);
            _title.text = _machine.Template.Name;
        }

        [SerializeField]
        private Transform _inputsHolder;
        
        [SerializeField]
        private ItemStackView _outputItemStackView;

        [SerializeField] 
        private ItemStackView _inputItemStackPrefab;

        [SerializeField] 
        private Button _forgeButton;
        
        [SerializeField] 
        private Image _progressBar;
        
        [SerializeField] 
        private GameObject _progressBarHolder;

        [SerializeField] 
        private TMP_Text _title;

        private Machine _machine;
        private GameWorld _gameWorld;
        private Image _image;
        private List<ItemStackView> _inputItemStacks;
    }
}