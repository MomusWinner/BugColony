using UnityEngine;

namespace BugColony.Scripts.Bugs.Behaviours.Movement
{
    public interface IBugMovement
    {
        float Speed { get; set; }
        public void MoveToTarget(Vector3 target);
    }
}