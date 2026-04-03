using BugColony.Scripts.Settings.Bugs.Behaviours.Movements;
using R3;
using UnityEngine;

namespace BugColony.Scripts.Bugs.Behaviours.Movement
{
    public class BugWalking : IBugMovement
    {
        public float Speed { get; set; }
        private readonly BugView _view;
        private Vector3 _target;
        private bool _isWalking;
        
        public BugWalking(BugWalkingSettings settings, BugState state, BugView view)
        {
            Speed = Random.Range(settings.MinSpeed, settings.MaxSpeed);
            _view = view;
            
            Observable.EveryUpdate().Subscribe(_ =>
            {
                if (!_isWalking) return;
                Vector3 diff = _target - _view.transform.position;
                Vector3 dir = diff.normalized;
                _view.transform.position += dir * (Speed * Time.deltaTime);
                if (diff.sqrMagnitude < 0.01)
                    StopMoving();
                
            }).RegisterTo(state.OnDestroyCts.Token);
        }

        public void MoveToTarget(Vector3 target)
        {
            if (_isWalking)
            {
                _target = target;
                return;
            }
            
            _isWalking = true;
            _view.BeginRunAnimation();
        }

        public void StopMoving()
        {
            if (!_isWalking) return;
            _isWalking = false;
            _view.EndRunAnimation();
        }
    }
}