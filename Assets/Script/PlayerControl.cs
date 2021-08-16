using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace GoGooseGo
{
    public class PlayerControl : MonoBehaviour
    {
        [Inject]
        public PlayerData playerData;

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
        }

        private void Update()
        {
            transform.position += (Vector3) this.playerData.velocity.Value * Time.deltaTime;
        }

        #region Input Action Hanlder
        private void OnJump()
        {

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