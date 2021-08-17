using System.Linq;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace GoGooseGo
{
    // Attach this to the Bag button
    public class ItemViewControl : MonoBehaviour
    {
        [SerializeField]
        private GameObject bagPanel;
        [SerializeField]
        private Button closeBtn;
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
            foreach(var kv in this.items.items)
            {
                this.addItem(kv.Key, kv.Value);
            }
            this.items.items.ObserveAdd()
                .Subscribe(x => this.addItem(x.Key, x.Value))
                .AddTo(this);
            this.items.items.ObserveReplace()
                .Subscribe(x => this.updateItem(x.Key, x.NewValue))
                .AddTo(this);
            this.closeBtn.onClick.AddListener(() =>
            {
                this.items.selected.Value = null;
                this.bagPanel.gameObject.SetActive(false);
            });
            // Click the bag will open item panel
            GetComponent<Button>().onClick.AddListener(() =>
            {
                this.bagPanel.gameObject.SetActive(true);
            });
        }

        private void addItem(ItemData item, int count)
        {
            // Use DI container to instantiate so that we would not missing references
            var newSlot = container.InstantiatePrefab(this.slot, this.grid)
                .GetComponent<ItemSlot>();
            newSlot.Show(item, count);
        }
        private void updateItem(ItemData item, int count)
        {
            foreach(var slot in this.grid.GetComponentsInChildren<ItemSlot>())
            {
                if(slot.item == item)
                {
                    slot.Show(item, count);
                    return;
                }
            }
            Debug.LogWarning($"Can not find: {slot}");
        }
    }
}