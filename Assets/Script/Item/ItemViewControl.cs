using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Zenject;

namespace GoGooseGo
{
    public class ItemViewControl : MonoBehaviour
    {
        [SerializeField]
        private Transform grid;
        [SerializeField]
        private ItemDetail detail;
        [Inject(Id = "ItemSlot")]
        private GameObject slot;
        [Inject]
        private ItemCollection items;
        [Inject]
        private DiContainer container;

        void Start()
        {
            this.items.items.ObserveAdd()
                .Subscribe(x => this.onItemAdd(x.Key, x.Value))
                .AddTo(this);
            this.items.items.ObserveReplace()
                .Subscribe(foo =>
                {
                    Debug.Log($"Replace {foo.Key.name} {foo.NewValue}");
                })
                .AddTo(this);
        }

        private void onItemAdd(ItemData item, int count)
        {
            // Use DI container to instantiate so that we would not missing references
            var newSlot = container.InstantiatePrefab(this.slot, this.grid)
                .GetComponent<ItemSlot>();
            newSlot.Show(item, count);
        }
        private void onItemRemove(ItemData item) { }
        private void onItemUpdate(ItemData item, int count) { }
    }
}