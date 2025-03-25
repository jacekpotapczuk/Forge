using System;
using System.Collections.Generic;
using System.Text;
using Forge.Domain;
using TMPro;
using UnityEngine;

namespace Forge.View
{
    /// <summary>
    /// View for <see cref="StatusEffect"/>
    /// </summary>
    [RequireComponent(typeof(TMP_Text))]
    public class StatusEffectView : MonoBehaviour
    {
        public void Awake()
        {
            _text = GetComponent<TMP_Text>();
        }

        public void Initialize(StatusEffect statusEffect, HashSet<Item> items)
        {
            statusEffect = statusEffect ?? throw new NullReferenceException(nameof(statusEffect));

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
    }
}