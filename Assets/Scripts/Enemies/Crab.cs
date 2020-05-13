using DG.Tweening;
using UnityEngine;

namespace Enemies
{
    public class Crab : RouteFollower, WeakSpot.IListener
    {
        private void Perish()
        {
            foreach (Collider col in GetComponentsInChildren<Collider>()) col.enabled = false;
            DOTween.Sequence()
                .Append(transform.DOJump(transform.position + Vector3.down, 2, 1, 0.75f))
                .Join(transform.DORotate(Vector3.forward * 240, 0.5f, RotateMode.LocalAxisAdd))
                .AppendCallback(() =>
                {
                    gameObject.SetActive(false);
                    Destroy(this);
                });
        }

        public void OnWeakSpotHit(Collider other)
        {
            Perish();
        }
    }
}