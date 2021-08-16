using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Zenject;

namespace GoGooseGo
{
    public class Wall : MonoBehaviour
    {
        [Inject]
        private PlayerData playerData;

        void Start()
        {
            var collider = GetComponent<Collider2D>();
            collider.OnTriggerExit2DAsObservable()
                .Where(other => other.CompareTag("Player"))
                .Subscribe(_ =>
                {
                    this.playerData.velocityLimitX = new Vector2(Mathf.NegativeInfinity, Mathf.Infinity);
                })
                .AddTo(this);
            collider.OnTriggerEnter2DAsObservable()
                .Where(other => other.CompareTag("Player"))
                .Select(other => other.GetComponent<PlayerControl>())
                .Subscribe(player =>
                {
                    var playerPos = player.transform.position;
                    var myPos = transform.position;
                    var velocity = player.GetComponent<Rigidbody2D>().velocity;
                    if(playerPos.x > myPos.x)
                    {
                        this.playerData.velocityLimitX = new Vector2(0, Mathf.Infinity);
                    }
                    else
                    {
                        this.playerData.velocityLimitX = new Vector2(Mathf.NegativeInfinity, 0);
                    }
                })
                .AddTo(this);
        }
    }
}