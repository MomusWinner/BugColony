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
        private bool _isRunning;
        private CancellationTokenSource _eatingCts = new();
        private CancellationTokenSource _runningCts = new();
        
        public Transform GetTarget() => transform;
        
        public bool IsAlive() => !_isDestroyed;
        
        public void OnDestroy()
        {
            transform.DOKill();
            _isDestroyed = true;
            Destroyed?.Invoke(this);
        }
        
        public void Destroy()
        {
            if (_isDestroyed) return;
            Destroy(gameObject);
        }

        public void BeginRunAnimation()
        {
            if (_isRunning) return;
            _isRunning = true;
            _runningCts = new CancellationTokenSource();
            transform
                .DOScale(new Vector3(transform.localScale.x, 1.1f, transform.localScale.z), 0.3f)
                .SetLoops(-1, LoopType.Yoyo)
                .SetEase(Ease.InOutSine).WithCancellation(_runningCts.Token);
        }

        public void EndRunAnimation()
        {
            if (!_isRunning) return;
            _isRunning = false;
            _runningCts.Cancel();
            _runningCts.Dispose();
        }
        
        public UniTask StartEatAnimation(Vector3 targetPosition)
        {
            _eatingCts.Cancel();
            _eatingCts.Dispose();
            _eatingCts = new CancellationTokenSource();
            
            Vector3 startPosition = transform.position;
            Vector3 dir = targetPosition - transform.position;
            dir.y = 0;
            transform.rotation = Quaternion.LookRotation(dir);
            transform.DOMove(transform.position + Vector3.up * 0.5f, 0.2f).WithCancellation(_eatingCts.Token);
            return DOTween.Sequence()
                .Append(transform.DOMove(transform.position + transform.forward * 0.8f, 0.4f))
                .Append(transform.DOMove(startPosition, 0.15f))
                .WithCancellation(_eatingCts.Token);
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