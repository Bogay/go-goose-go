using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GoGooseGo
{
    [CreateAssetMenu(menuName = "GoGooseGo/ItemCollection")]
    public class ItemCollection : ScriptableObject
    {
        [SerializeField]
        private List<ItemData> items;

        [SerializeField]
        public List<int> itemCounts;
    }
}