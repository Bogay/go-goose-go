using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Zenject;

namespace GoGooseGo
{
    public class StageEnd : MonoBehaviour
    {
        [Inject(Id = "RestartMenu")]
        private GameObject menu;
        [Inject]
        DiContainer container;

        private void Start()
        {
            GetComponent<Collider2D>().OnTriggerEnter2DAsObservable()
                .Where(other => other.CompareTag("Player"))
                .Subscribe(_ => this.container.InstantiatePrefab(this.menu, FindObjectOfType<Canvas>().transform))
                .AddTo(this);
        }
    }
}