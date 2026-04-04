using System.Threading;
using Cysharp.Threading.Tasks;

namespace BugColony.Scripts.Bugs.Behaviours.EatingBehaviours
{
    public interface IBugEatingBehaviour
    {
        public UniTask Eat(ITarget eatable, CancellationToken cancellationToken = default);
    }
}