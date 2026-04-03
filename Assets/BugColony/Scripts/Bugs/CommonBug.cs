using BugColony.Scripts.Bugs.Behaviours;
using BugColony.Scripts.Bugs.Behaviours.Movement;
using BugColony.Scripts.Bugs.Behaviours.Splits;
using Cysharp.Threading.Tasks;
using VContainer;

namespace BugColony.Scripts.Bugs
{
    public sealed class CommonBug : Bug
    {
        [Inject] private IBugMovement _movementBehaviour;
        [Inject] private IBugSplit _splitBehaviour;
        [Inject] private IBugTargetSelector _targetSelector;
        
        private bool _isEating;
        
        protected override void Update()
        {
            if (_splitBehaviour.CanSplit())
            {
                _movementBehaviour.StopMoving();
                _splitBehaviour.Split();
            }
            
            if (_isEating) return;
            if (State.Target is null || !State.Target.IsAlive())
                State.Target = _targetSelector.GetTarget();
            
            if (State.Target is not null && State.Target.IsAlive())
            {
                if ((State.Target.GetPosition() - Position).magnitude <= Settings.EatingDistance)
                {
                    _movementBehaviour.StopMoving();
                    EatWithDelayAsync().Forget();
                }
                else
                {
                    _movementBehaviour.MoveToTarget(State.Target.GetPosition());
                }
            }
        }
        
        private async UniTaskVoid EatWithDelayAsync()
        {
            _isEating = true;
            await View.StartEatAnimation(State.Target.GetPosition());
            
            if (State.OnDestroyCts.IsCancellationRequested)
                return;
            if (State.Target != null && State.Target.IsAlive())
                State.Energy += State.Target.Eat();
            _isEating = false;
        }
    }
}