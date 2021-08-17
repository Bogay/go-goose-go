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
        private DiContainer container;
        [Inject]
        private GameState game;

        private void Start()
        {
            var collider = GetComponent<Collider2D>();
            collider.OnTriggerEnter2DAsObservable()
                .Where(other => other.CompareTag("Player"))
                .Subscribe(_ =>
                {
                    this.container.InstantiatePrefab(this.menu, FindObjectOfType<Canvas>().transform);
                    this.game.isStop.Value = true;
                })
                .AddTo(this);
        }
    }
}