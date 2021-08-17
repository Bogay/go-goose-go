using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GoGooseGo
{
    public class ItemSlot : MonoBehaviour
    {
        [SerializeField]
        private Text count;
        [SerializeField]
        private Image sprite;

        public void Show(ItemData item, int count)
        {
            this.sprite.sprite = item.sprite;
            this.count.text = count.ToString();
        }
    }
}