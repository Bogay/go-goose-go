using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
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
        [Inject]
        private ItemCollection collection;

        private void Start()
        {
            this.collection.selected
                .Subscribe(s =>
                {
                    if(s == null)
                    {
                        // Close UI
                        gameObject.SetActive(false);
                    }
                    else
                    {
                        // Re-enable UI
                        gameObject.SetActive(true);
                        var count = this.collection.items[s];
                        this.Show(s, count);
                    }
                })
                .AddTo(this);
        }

        public void Show(ItemData item, int count)
        {
            this.name.text = item.name;
            this.slot.Show(item, count);
            this.description.text = item.description;
        }
    }
}