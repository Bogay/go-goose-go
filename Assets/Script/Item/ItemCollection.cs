using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;

namespace GoGooseGo
{
    [CreateAssetMenu(menuName = "GoGooseGo/ItemCollection")]
    public class ItemCollection : ScriptableObject
    {
        [SerializeField]
        private List<ItemData> _items;
        [SerializeField]
        private List<int> itemCounts;
        public ReactiveProperty<ItemData> selected { get; private set; }
        public ReactiveDictionary<ItemData, int> items { get; private set; }

        public ItemCollection Init()
        {
            if(this._items.Count != this.itemCounts.Count)
                Debug.LogWarning("ItemCollection: Length not equal");
            this.selected = new ReactiveProperty<ItemData>();
            this.items = new ReactiveDictionary<ItemData, int>(this.ToDictionary());
            return this;
        }

        public Dictionary<ItemData, int> ToDictionary() => (Dictionary<ItemData, int>) this;

        public static explicit operator Dictionary<ItemData, int>(ItemCollection collection)
        {
            return Enumerable.Zip(
                    collection._items,
                    collection.itemCounts,
                    (item, count) => new
                    {
                        key = item,
                            value = count
                    })
                .ToDictionary(p => p.key, p => p.value);
        }

        public void Add(ItemData item)
        {
            try
            {
                this.items[item]++;
            }
            catch (KeyNotFoundException)
            {
                this.items[item] = 1;
            }
        }

        public void Use(ItemData item)
        {
            try
            {
                this.items[item]--;
            }
            catch (KeyNotFoundException)
            {
                Debug.Log($"Cannot delete {item}: Not found");
            }
        }
    }
}