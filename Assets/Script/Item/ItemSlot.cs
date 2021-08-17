using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace GoGooseGo
{
    public class ItemSlot : MonoBehaviour
    {
        [SerializeField]
        private Text countText;
        [SerializeField]
        private Image sprite;
        [Inject]
        private ItemCollection collection;

        public ItemData item { get; private set; }
        private int count;

        private void Start()
        {
            // If this.item is null, it means "cancel selection"
            GetComponent<Button>().onClick.AddListener(() =>
            {
                Debug.Log(this.item);
                this.collection.selected.Value = this.item;
            });
        }

        public void Show(ItemData item, int count)
        {
            if(count == 0)
            {
                Destroy(gameObject);
                return;
            }
            this.item = item;
            this.count = count;
            this.sprite.sprite = item.sprite;
            this.countText.text = count.ToString();
        }
    }
}