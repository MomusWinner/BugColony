using UnityEngine;

namespace BugColony.Scripts.Bugs.Behaviours.Movements
{
    public interface IBugMovement
    {
        float Speed { get; set; }
        public void MoveToTarget(Vector3 target);
        public void StopMoving();
    }
}