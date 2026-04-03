using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace BugColony.Scripts.Bugs
{
    public class BugView : MonoBehaviour
    {
        public event Action<BugView> Destroyed;
        
        private bool _isDestroyed;

        public Transform GetTarget() => transform;
        
        private CancellationTokenSource _eatingCts = new();
        
        public bool IsAlive() => !_isDestroyed;
        
        public void OnDestroy()
        {
            _isDestroyed = true;
            Destroyed?.Invoke(this);
        }
        
        public void Destroy()
        {
            if (_isDestroyed) return;
            Destroy(gameObject);
        }

        public UniTask StartEatAnimation(Vector3 targetPosition)
        {
            _eatingCts.Cancel();
            _eatingCts.Dispose();
            _eatingCts = new CancellationTokenSource();
            
            using var tokenCts = CancellationTokenSource.CreateLinkedTokenSource(
                _eatingCts.Token,
                this.GetCancellationTokenOnDestroy()
            );

            Vector3 startPosition = transform.position;
            transform.rotation = Quaternion.LookRotation(targetPosition - transform.position);
            transform.DOMove(transform.position + Vector3.up * 0.5f, 0.2f).WithCancellation(tokenCts.Token);
            return DOTween.Sequence()
                .Append(transform.DOMove(transform.position + transform.forward * 0.7f, 0.4f))
                .Append(transform.DOMove(startPosition, 0.15f))
                .WithCancellation(tokenCts.Token);
        }

        public UniTask StartDieAnimation()
        {
            return DOTween.Sequence()
                .Append(transform.DOMove(transform.position - Vector3.up * 0.8f,2f))
                .AppendInterval(0.3f)
                .WithCancellation(this.GetCancellationTokenOnDestroy());
        }
    }
}