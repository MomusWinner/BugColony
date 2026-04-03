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
        
        [SerializeField] private Collider _collider;
        private bool _isDestroyed;
        private bool _isRunning;
        private CancellationTokenSource _animationCts = new();
        private CancellationTokenSource _runningCts = new();

        public void Awake()
        {
            _collider = GetComponent<Collider>();
        }
        
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
        
        public bool IsAlive() => !_isDestroyed;

        public void BeginRunAnimation()
        {
            if (_isRunning) return;
            _isRunning = true;
            transform
                .DOScale(new Vector3(transform.localScale.x, 1.1f, transform.localScale.z), 0.3f)
                .SetLoops(-1, LoopType.Yoyo)
                .SetEase(Ease.InOutSine).WithCancellation(_runningCts.Token);
        }

        public void EndRunAnimation()
        {
            if (!_isRunning) return;
            _isRunning = false;
            ResetRunningAnimationCts();
        }
        
        public UniTask StartEatAnimation(Vector3 targetPosition)
        {
            ResetAnimationCts();
            Vector3 startPosition = transform.position;
            Vector3 dir = targetPosition - transform.position;
            dir.y = 0;
            transform.rotation = Quaternion.LookRotation(dir);
            _collider.enabled = false;
            return DOTween.Sequence()
                .AppendInterval(0.1f)
                .Append(transform.DOMove(transform.position + transform.forward * 1.0f, 0.2f))
                .Append(transform.DOMove(startPosition, 0.1f))
                .AppendInterval(0.1f)
                .OnComplete(() => _collider.enabled = true)
                .AwaitForComplete(TweenCancelBehaviour.Complete, _animationCts.Token);
        }

        public UniTask StartSplitAnimation(Vector3 newPosition)
        {
            ResetAnimationCts();
            _collider.enabled = false;

            return DOTween.Sequence()
                .AppendInterval(0.1f)
                .Append(transform.DOJump(newPosition, 1.5f, 1, 0.4f))
                .Append(transform.DOScale(transform.localScale * 1.2f, 0.1f).SetLoops(2, LoopType.Yoyo))
                .AppendInterval(0.1f)
                .OnComplete(() => _collider.enabled = true)
                .AwaitForComplete(TweenCancelBehaviour.Complete, _animationCts.Token);
        }

        public UniTask StartDieAnimation()
        {
            ResetAnimationCts();
            ResetRunningAnimationCts();
            return DOTween.Sequence()
                .Append(transform.DOMove(transform.position - Vector3.up * 0.8f,2f))
                .AppendInterval(0.3f)
                .WithCancellation(this.GetCancellationTokenOnDestroy());
        }

        private void ResetAnimationCts()
        {
            _animationCts.Cancel();
            _animationCts.Dispose();
            _animationCts = new CancellationTokenSource();
        }
        
        private void ResetRunningAnimationCts()
        {
            _runningCts.Cancel();
            _runningCts.Dispose();
            _runningCts = new CancellationTokenSource();
        }
    }
}