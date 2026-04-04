using System.Threading;
using Cysharp.Threading.Tasks;

namespace BugColony.Scripts.Bugs.Behaviours.EatingBehaviours
{
    public class BugEatingBehaviour : IBugEatingBehaviour
    {
        private readonly BugView _view;
        private readonly BugState _state;

        public BugEatingBehaviour(BugView view, BugState state)
        {
            _view = view;
            _state = state;
        }
        
        public async UniTask Eat(ITarget eatable, CancellationToken cancellationToken)
        {
            await _view.StartEatAnimation(_state.Target.GetPosition(), cancellationToken);
            
            if (cancellationToken.IsCancellationRequested)
                return;
            if (eatable is not null && eatable.IsAlive())
                _state.Energy += eatable.Eat();
        }
    }
}