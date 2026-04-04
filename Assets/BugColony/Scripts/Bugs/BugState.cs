using System.Threading;

namespace BugColony.Scripts.Bugs
{
    public class BugState
    {
        public int Energy;
        public int Gen;
        public ITarget Target;
        public readonly CancellationTokenSource OnDestroyCts = new();
        public bool Enabled = true;
        public bool IsDead;
        public float CreationTime;
        
        public void Dispose()
        {
            OnDestroyCts?.Cancel();
            OnDestroyCts?.Dispose();
            Energy = 0; 
        }
    }
}