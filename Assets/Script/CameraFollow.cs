using DG.Tweening;
using UniRx;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Vector2 offset;
    public Transform target;

    void Start()
    {
        this.target.ObserveEveryValueChanged(t => t.position)
            .Subscribe(pos =>
            {
                var newPos = pos + (Vector3) offset;
                newPos.z = transform.position.z;
                transform.DOMove(newPos, 0.3f);
            })
            .AddTo(this);
    }
}