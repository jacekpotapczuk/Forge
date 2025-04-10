﻿using System;
using System.Collections.Generic;
using System.Linq;
using Forge.Domain;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Forge.View
{
    /// <summary>
    /// View for <see cref="ItemStack"/>
    /// </summary>
    public class ItemStackView : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public void Initialize(ItemStack itemStack)
        {
            if (_itemStack != null)
            {
                _itemStack.Changed -= UpdateView;
            }

            _itemStack = itemStack ?? throw new NullReferenceException(nameof(itemStack));
            itemStack.Changed += UpdateView;
            UpdateView();

            _parentCanvas = GetComponentInParent<Canvas>();
        }

        public void OnDestroy()
        {
            if (_itemStack == null)
            {
                return;
            }
            
            _itemStack.Changed -= UpdateView;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (_itemStack.Item == null)
            {
                return;
            }
            
            _draggedElement = GameObjectPool.Get(this, _parentCanvas.transform);
            var rectTransform = _draggedElement.GetComponent<RectTransform>();
            rectTransform.sizeDelta = GetComponent<RectTransform>().sizeDelta;
            _draggedElement.Initialize(new ItemStack(_itemStack.Item, 1));
        }
        
        public void OnDrag(PointerEventData eventData)
        {
            if (_draggedElement == null)
            {
                return;
            }
            _draggedElement.transform.position = Input.mousePosition;
        }
        
        public void OnEndDrag(PointerEventData eventData)
        {
            if (_draggedElement != null)
            {
                GameObjectPool.Destroy(_draggedElement.gameObject);    
            }
            
            var pointerData = new PointerEventData(EventSystem.current)
            {
                position = Input.mousePosition
            };
            
            var results = new List<RaycastResult>();
            _parentCanvas.GetComponent<GraphicRaycaster>().Raycast(pointerData, results);

            // todo: could be optimized by using layers etc
            results = results.Where(_ => _.gameObject.GetComponent<ItemStackView>() != null).ToList();
            if (results.Count > 1)
            {
                Debug.LogError("Multiple ItemStackViews in the same position. This could cause issues.");
            }

            if (results.Count <= 0)
            {
                return;
            }

            var result = results[0].gameObject.GetComponent<ItemStackView>();

            var canAdd = result.CanAddItem(_draggedElement);

            if (canAdd)
            {
                _itemStack.RemoveOne();
                result.AddItem(_draggedElement);
                
            }
        }

        [SerializeField]
        private Image _image;
        
        [SerializeField]
        private TextMeshProUGUI _text;
        
        [CanBeNull]
        private ItemStackView _draggedElement;
        private ItemStack _itemStack;
        private Canvas _parentCanvas;
        
        private void UpdateView()
        {
            if (_itemStack?.Item == null)
            {
                _image.sprite = null;
                _text.text = string.Empty;
                return;
            }
            
            _image.sprite = _itemStack.Item.Template.Sprite;
            _text.text = _itemStack.Amount.ToString();
        }

        private bool CanAddItem(ItemStackView itemToAdd)
        {
            if (itemToAdd == null)
            {
                return false;
            }

            if (_itemStack.Item != null && (itemToAdd._itemStack.Item != _itemStack.Item))
            {
                return false;
            }

            return true;
        }

        private void AddItem(ItemStackView itemToAdd) 
            => _itemStack.Add(itemToAdd._itemStack.Item, itemToAdd._itemStack.Amount);
        
    }
}