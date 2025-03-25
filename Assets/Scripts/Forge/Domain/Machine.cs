using System;
using System.Linq;
using UnityEngine;

namespace Forge.Domain
{
    /// <summary>
    /// Defines Machine instance
    /// </summary>
    public class Machine : IDisposable
    {
        public Action ProcessingStarted;
        public Action ProcessingEnded;
        
        public MachineTemplate Template { get; }

        public ItemStack[] Inputs => _inputs;
        public ItemStack Output => _output;

        public float TimeUntilCompletion => _timeUntilCompletion;
        public RecipeTemplate ProceedRecipe => _currentlyProceededRecipeTemplate;
        public Player Crafter => _crafter;

        public Machine(GameWorld gameWorld, MachineTemplate template)
        {
            Template = template ?? throw new NullReferenceException();
            _gameWorld = gameWorld ?? throw new NullReferenceException();

            var inputsCount = Template.GetNumberOfInputs();

            _inputs = new ItemStack[inputsCount];

            for (var i = 0; i < inputsCount; i++)
            {
                _inputs[i] = new ItemStack();
            }

            _output = new ItemStack();
        }
        
        
        public void Dispose()
        {
            for (var i = Inputs.Length - 1; i >= 0; i--)
            {
                Inputs[i].Dispose();
            }

            Output.Dispose();
        }

        public void Update(float deltaTime)
        {
            if (!_isProcessing)
            {
                return;
            }

            _timeUntilCompletion -= deltaTime;

            if (_timeUntilCompletion <= 0)
            {
                FinishProcessing();
            }
        }

        public bool TryProcess(Player crafter)
        {
            if (_isProcessing)
            {
                _gameWorld.NotificationService.ShowNotification($"{Template.Name} machine is busy!", type: NotificationType.Error);
                return false;
            }
            var anyNonEmpty = false;
            
            foreach (var input in _inputs)
            {
                anyNonEmpty |= !input.IsEmpty;
                
                if (anyNonEmpty)
                {
                    break;
                }
            }

            if (!anyNonEmpty)
            {
                _gameWorld.NotificationService.ShowNotification("Provide inputs for processing", type: NotificationType.Error);
                return false;
            }

            foreach (var recipe in Template.RecipeTemplates)
            {
                var areInputsProvided = true;
                
                foreach (var recipeInput in recipe.InputItemTemplates)
                {
                    var isInputProvided = Inputs.Any(_ => _.Item != null && _.Item.Template == recipeInput && _.Amount > 0);
                    areInputsProvided &= isInputProvided;
                }

                if (areInputsProvided)
                {
                    ProcessRecipe(recipe, crafter);
                    return true;
                }
            }

            _gameWorld.NotificationService.ShowNotification("No valid recipe for given inputs", type: NotificationType.Error);
            return false;
        }
        
        private readonly GameWorld _gameWorld;
        // todo: caching can be added to inputs, so there is no need to search whole list to check if given item is in inputs
        private readonly ItemStack[] _inputs;
        private readonly ItemStack _output;

        private bool _isProcessing = false;
        private float _timeUntilCompletion;
        private RecipeTemplate _currentlyProceededRecipeTemplate;
        private Player _crafter;

        private void ProcessRecipe(RecipeTemplate recipeTemplate, Player crafter)
        {
            foreach (var recipeInput in recipeTemplate.InputItemTemplates)
            {
                var correspondingInput = _inputs.First(_ => _.Item != null && _.Item.Template == recipeInput && _.Amount > 0);
                
                correspondingInput.RemoveOne();
            }

            StartProcessing(recipeTemplate, crafter);
        }

        private void StartProcessing(RecipeTemplate recipeTemplate, Player crafter)
        {
            _isProcessing = true;
            _timeUntilCompletion = recipeTemplate.CompletionTime + crafter.CraftingTimeReduction;
            _currentlyProceededRecipeTemplate = recipeTemplate;
            _crafter = crafter;
            ProcessingStarted?.Invoke();
        }

        private void FinishProcessing()
        {
            var successChance = _currentlyProceededRecipeTemplate.SuccessChancePercentage + _crafter.Luck;
            var rand = UnityEngine.Random.value;
            
            if (_output.Item != null && _output.Item.Template != _currentlyProceededRecipeTemplate.OutputItemTemplate && _output.Amount > 0)
            {
                _gameWorld.NotificationService.ShowNotification("Removing item from output.\n Next time remember about taking your outputs before next craft!");
                _output.RemoveAll();
            }
            
            if (rand > successChance)
            {
                _gameWorld.NotificationService.ShowNotification($"{Template.Name} failed crafting. You lost all inputs");
            }
            else
            {
                var outputItem = new Item(_currentlyProceededRecipeTemplate.OutputItemTemplate);
                _output.Add(outputItem, 1);
                _crafter.NotifySuccessfulCraft(_currentlyProceededRecipeTemplate);
            }
            
            _isProcessing = false;
            _timeUntilCompletion = 0f;
            _currentlyProceededRecipeTemplate = null;
            _crafter = null;
            ProcessingEnded?.Invoke();
        }
    }
}