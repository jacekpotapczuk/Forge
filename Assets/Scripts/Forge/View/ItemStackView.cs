using Forge.Domain;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Forge.View
{
    public class ItemStackView : MonoBehaviour
    {
        public void Initialize(ItemStack itemStack)
        {
            if (_itemStack != null)
            {
                _itemStack.Changed -= UpdateView;
            }

            _itemStack = itemStack;
            itemStack.Changed += UpdateView;
            UpdateView();
        }

        [SerializeField]
        private Image _image;
        
        [SerializeField]
        private TextMeshProUGUI _text;

        private ItemStack _itemStack;
        
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

    }
}