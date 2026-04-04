using System.Threading;
using BugColony.Scripts.Settings.Bugs.Behaviours.Movements;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace BugColony.Scripts.Bugs.Behaviours.Movements
{
    public class BugWalking : IBugMovementBehaviour
    {
        public float Speed { get; set; }
        private readonly BugView _view;
        private Vector3 _target;
        private bool _isWalking;

        public BugWalking(BugWalkingSettings settings, BugState state, BugView view)
        {
            Speed = Random.Range(settings.MinSpeed, settings.MaxSpeed);
            _view = view;
        }

        public async UniTask MoveToTarget(Vector3 target, CancellationToken token)
        {
            if (_isWalking)
            {
                _target = target;
                return;
            }
            
            _isWalking = true;
            _view.BeginRunAnimation(token);
            
            while (!token.IsCancellationRequested && _isWalking)
            {
                Vector3 diff = _target - _view.transform.position;
                Vector3 dir = diff.normalized;
                _view.transform.position += dir * (Speed * Time.deltaTime);
                if (diff.sqrMagnitude < 0.01)
                    StopMoving();
                
                await UniTask.Yield(token); 
            }
        }

        public void StopMoving()
        {
            if (!_isWalking) return;
            _isWalking = false;
            _view.EndRunAnimation();
        }
    }
}