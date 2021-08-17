using System.Collections.Generic;
using System.Linq;
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

        public void Add(ItemData item)
        {
            Debug.Log($"Got item: {item.name}");
            for(int i = 0; i < items.Count; i++)
            {
                if(this.items[i].id == item.id)
                {
                    this.itemCounts[i]++;
                    return;
                }
            }
            this.items.Add(item);
            this.itemCounts.Add(1);
        }
    }
}