using System;
using System.Collections.Generic;
using System.Text;
using Forge.Domain;
using TMPro;
using UnityEngine;

namespace Forge.View
{
    
    [RequireComponent(typeof(TMP_Text))]
    public class StatusEffectView : MonoBehaviour
    {
        private void Awake()
        {
            _text = GetComponent<TMP_Text>();
        }

        public void Initialize(StatusEffect statusEffect, HashSet<Item> items)
        {
            _statusEffect = statusEffect ?? throw new NullReferenceException(nameof(statusEffect));

            var sb = new StringBuilder($"- {statusEffect.Name} (source: ");

            foreach (var item in items)
            {
                sb.Append(item.Template.Name);
                sb.Append(", ");
            }

            sb.Remove(sb.Length - 2, 2);
            sb.Append(")");

            _text.text = sb.ToString();
        }

        private TMP_Text _text;
        private StatusEffect _statusEffect;
    }
}