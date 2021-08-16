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
        public BoolReactiveProperty isGround { get; private set; }
        public Vector2ReactiveProperty velocity { get; private set; }
        public bool isLeftPressed
        {
            get => _isLeftPressed;
            set
            {
                _isLeftPressed = value;
                this.refreshVelocity();
            }
        }
        public bool isRightPressed
        {
            get => _isRightPressed;
            set
            {
                _isRightPressed = value;
                this.refreshVelocity();
            }
        }
        private bool _isLeftPressed;
        private bool _isRightPressed;
        public ReactiveProperty<Direction> direction { get; } = new ReactiveProperty<Direction>(Direction.Left);

        // Call this before game start to reset runtime status
        public void Init()
        {
            this.isGround = new BoolReactiveProperty();
            this.velocity = new Vector2ReactiveProperty();
        }

        private void refreshVelocity()
        {
            // Update direction
            if(this.isRightPressed != this.isLeftPressed)
            {
                this.direction.Value = this.isLeftPressed ? Direction.Left : Direction.Right;
            }
            // Update velocity
            var scale = this.speed * (this.isLeftPressed || this.isRightPressed ? 1 : 0);
            var dirVec = this.direction.Value == Direction.Left ? Vector2.left : Vector2.right;
            this.velocity.Value = dirVec * scale;
        }
    }
}