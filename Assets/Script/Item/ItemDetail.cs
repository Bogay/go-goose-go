using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GoGooseGo
{
    public class ItemDetail : MonoBehaviour
    {
        [SerializeField]
        private new Text name;
        [SerializeField]
        private ItemSlot slot;
        [SerializeField]
        private Text description;

        public void Show(ItemData item, int count)
        {
            this.name.text = item.name;
            this.slot.Show(item, count);
            this.description.text = item.description;
        }
    }
}