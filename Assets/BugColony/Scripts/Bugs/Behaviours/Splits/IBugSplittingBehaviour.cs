using System.Threading;
using Cysharp.Threading.Tasks;

namespace BugColony.Scripts.Bugs.Behaviours.Splits
{
    public interface IBugSplittingBehaviour
    {
        bool CanSplit();
        UniTask Split(CancellationToken cancellationToken);
    }
}