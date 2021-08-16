using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace GoGooseGo
{
    public class PlayerControl : MonoBehaviour
    {
        [Inject]
        private PlayerData playerData;

        private void Start()
        {
            this.playerData.direction
                .Subscribe(dir =>
                {
                    var y = dir == PlayerData.Direction.Left ? 0 : 180;
                    transform.eulerAngles = new Vector3(0, y, 0);
                }).AddTo(this);
            var animation = GetComponent<Animation>();
            this.playerData.velocity
                .Subscribe(vec =>
                {
                    if(vec.sqrMagnitude > 0)
                        animation.Play("Walk");
                    else
                        animation.Stop();
                })
                .AddTo(this);
            // Ground check
            var collider = GetComponent<Collider2D>();
            collider.OnTriggerEnter2DAsObservable()
                .Where(other => other.CompareTag("Ground"))
                .Do(_ => Debug.Log("I am enter"))
                .Subscribe(other =>
                {
                    this.playerData.isGround.Value = true;
                })
                .AddTo(this);
            collider.OnTriggerExit2DAsObservable()
                .Where(other => other.CompareTag("Ground"))
                .Do(_ => Debug.Log("I am exit"))
                .Subscribe(_ => this.playerData.isGround.Value = false)
                .AddTo(this);
            var rb2d = GetComponent<Rigidbody2D>();
            this.playerData.velocity
                .Subscribe(vel => rb2d.velocity = vel)
                .AddTo(this);
        }

        private void FixedUpdate()
        {
            this.playerData.RefreshVelocity(Time.fixedDeltaTime);
        }

        #region Input Action Hanlder
        private void OnJump()
        {
            this.playerData.Jump();
        }

        private void OnLeft(InputValue input)
        {
            this.playerData.isLeftPressed = input.Get<float>() > 0;
        }

        private void OnRight(InputValue input)
        {
            this.playerData.isRightPressed = input.Get<float>() > 0;
        }
        #endregion
    }
}