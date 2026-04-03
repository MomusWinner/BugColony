using System.Threading;
using UnityEngine;

namespace BugColony.Scripts.Bugs
{
    public class BugState
    {
        public IMovable Movable;
        public int Energy;
        public int Gen;
        public Transform Target;
        public readonly CancellationTokenSource CancellationTokenSource = new();
        
        public void Reset()
        {
            CancellationTokenSource.Cancel();
            Energy = 0; 
            Gen = 0; 
            Movable = null;
        }
    }
}