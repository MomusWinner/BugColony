using System.Threading;
using UnityEngine;

namespace BugColony.Scripts.Bugs
{
    public class BugState
    {
        public Transform Movable;
        public int Energy;
        public int Gen;
        public ITarget Target;
        public readonly CancellationTokenSource OnDestroyCts = new();
        public bool IsDead;
        
        public void Dispose()
        {
            OnDestroyCts?.Cancel();
            OnDestroyCts?.Dispose();
            Energy = 0; 
        }
    }
}