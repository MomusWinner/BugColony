using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace BugColony.Scripts.Bugs
{
    [RequireComponent(typeof(Collider))]
    public class BugView : MonoBehaviour
    {
        public event Action<BugView> Destroyed;
        
        [SerializeField] private Collider _collider;
        private bool _isDestroyed;
        
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
            transform.DOKill();
            Destroy(gameObject);
        }
        
        public bool IsAlive() => !_isDestroyed;

        public void BeginRunAnimation(CancellationToken token)
        {
            ResetAnimation();
            transform
                .DOScale(new Vector3(transform.localScale.x, 1.5f, transform.localScale.z), 0.3f)
                .SetLoops(-1, LoopType.Yoyo)
                .SetEase(Ease.InOutSine)
                .WithCancellation(token);
        }

        public void EndRunAnimation()
        {
            ResetAnimation();
            transform.localScale = Vector3.one;
        }
        
        public UniTask StartEatAnimation(Vector3 targetPosition, CancellationToken token = default)
        {
            ResetAnimation();
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
                .WithCancellation(token);
        }

        public UniTask StartSplitAnimation(Vector3 newPosition, CancellationToken cancellationToken = default)
        {
            ResetAnimation();
            _collider.enabled = false;
            
            return DOTween.Sequence()
                .AppendInterval(0.1f)
                .Append(transform.DOJump(newPosition, 1.5f, 1, 0.4f))
                .Append(transform.DOScale(transform.localScale * 1.2f, 0.1f).SetLoops(2, LoopType.Yoyo))
                .AppendInterval(0.1f)
                .OnComplete(() => _collider.enabled = true)
                .WithCancellation(cancellationToken);
        }

        public UniTask StartDieAnimation(CancellationToken token = default)
        {
            ResetAnimation();
            return DOTween.Sequence()
                .Append(transform.DOMove(transform.position - Vector3.up * 0.8f,2f))
                .AppendInterval(0.3f)
                .WithCancellation(token);
        }

        private void ResetAnimation()
        {
            transform.localScale = Vector3.one;
            transform.DOKill(true);
        }
    }
}