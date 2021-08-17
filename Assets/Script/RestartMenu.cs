using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace GoGooseGo
{
    public class RestartMenu : MonoBehaviour
    {
        [SerializeField]
        private Button confirm;
        [SerializeField]
        private Button cancel;
        [Inject]
        private GameState game;

        void Start()
        {
            this.confirm.onClick.AddListener(this.game.Restart);
            this.cancel.onClick.AddListener(() => Destroy(gameObject));
        }
    }
}