using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Zenject;

namespace GoGooseGo
{
    public class ItemPick : MonoBehaviour
    {
        [SerializeField]
        private ItemData item;

        [Inject]
        private ItemCollection items;

        void Start()
        {
            GetComponent<SpriteRenderer>().sprite = this.item.sprite;
            GetComponent<Collider2D>().OnTriggerEnter2DAsObservable()
                .Where(other => other.CompareTag("Player"))
                .Subscribe(_ =>
                {
                    this.items.Add(this.item);
                    Destroy(gameObject);
                })
                .AddTo(this);
        }
    }
}