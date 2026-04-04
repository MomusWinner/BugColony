using System.Threading;
using BugColony.Scripts.Bugs.Behaviours.EatingBehaviours;
using BugColony.Scripts.Bugs.Behaviours.Movements;
using BugColony.Scripts.Bugs.Behaviours.Splits;
using BugColony.Scripts.Bugs.Behaviours.TargetSelectors;
using Cysharp.Threading.Tasks;
using VContainer;

namespace BugColony.Scripts.Bugs
{
    public sealed class CommonBug : Bug
    {
        [Inject] private IBugMovementBehaviour _movementBehaviour;
        [Inject] private IBugSplittingBehaviour _splittingBehaviourBehaviour;
        [Inject] private IBugTargetFinder _targetFinder;
        [Inject] private IBugEatingBehaviour _eatingBehaviour;
        
        protected override async UniTask Update(CancellationToken token)
        {
            if (_splittingBehaviourBehaviour.CanSplit())
            {
                _movementBehaviour.StopMoving();
                await _splittingBehaviourBehaviour.Split(token);
            }
            
            if (State.Target is null || !State.Target.IsAlive())
                State.Target = _targetFinder.GetTarget();
            
            if (State.Target is not null && State.Target.IsAlive())
            {
                if ((State.Target.GetPosition() - Position).magnitude <= Settings.EatingDistance)
                {
                    _movementBehaviour.StopMoving();
                    await _eatingBehaviour.Eat(State.Target, token);
                }
                else
                {
                    _movementBehaviour.MoveToTarget(State.Target.GetPosition(), token).Forget();
                }
            }
        }
    }
}