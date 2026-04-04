using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace BugColony.Scripts.Bugs.Behaviours.Movements
{
    public interface IBugMovementBehaviour
    {
        float Speed { get; set; }
        public UniTask MoveToTarget(Vector3 target, CancellationToken token);
        public void StopMoving();
    }
}