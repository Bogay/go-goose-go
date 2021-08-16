using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace GoGooseGo
{
    [CreateAssetMenu(menuName = "Player/Data")]
    public class PlayerData : ScriptableObject
    {
        public enum Direction
        {
            Left,
            Right,
        }

        [field : SerializeField]
        public float speed { get; private set; }

        [field : SerializeField]
        public float jumpPower { get; private set; }

        [field : SerializeField]
        public float gravity { get; private set; } = -9.8f;

        public ReactiveProperty<Direction> direction { get; } = new ReactiveProperty<Direction>(Direction.Left);
        public BoolReactiveProperty isGround { get; private set; }
        public Vector2ReactiveProperty velocity { get; private set; }
        public bool isLeftPressed
        {
            get => _isLeftPressed;
            set
            {
                _isLeftPressed = value;
                this.RefreshVelocity();
            }
        }
        public bool isRightPressed
        {
            get => _isRightPressed;
            set
            {
                _isRightPressed = value;
                this.RefreshVelocity();
            }
        }
        private bool _isLeftPressed;
        private bool _isRightPressed;
        private bool isJump;

        // Call this before game start to reset runtime status
        public void Init()
        {
            this.isGround = new BoolReactiveProperty();
            this.velocity = new Vector2ReactiveProperty();
        }

        public void Jump()
        {
            if(!this.isGround.Value)
            {
                Debug.Log("I am not on the ground!");
                return;
            }
            this.isJump = true;
            this.RefreshVelocity();
        }

        public void RefreshVelocity(float delta = 0)
        {
            // Update direction
            if(this.isRightPressed != this.isLeftPressed)
            {
                this.direction.Value = this.isLeftPressed ? Direction.Left : Direction.Right;
            }
            // Update velocity
            var scale = this.speed * (this.isLeftPressed || this.isRightPressed ? 1 : 0);
            var horizontalVel = this.direction.Value == Direction.Left ? -1 : 1;
            var verticalVel = this.velocity.Value.y;
            if(this.isGround.Value && verticalVel < 0)
            {
                verticalVel = 0;
            }
            else if(!this.isGround.Value)
            {
                verticalVel += this.gravity * delta;

            }
            if(this.isJump)
            {
                verticalVel += this.jumpPower;
                this.isJump = false;
            }
            var newVel = new Vector2(horizontalVel * scale, verticalVel);
            this.velocity.Value = newVel;
        }
    }
}