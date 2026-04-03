using BugColony.Scripts.Settings.Bugs.Behaviours.Movements;
using UnityEngine;

namespace BugColony.Scripts.Bugs.Behaviours.Movement
{
    public class BugWalking : IBugMovement
    {
        public float Speed { get; set; }
        private readonly BugState _state;
        
        public BugWalking(BugWalkingSettings settings, BugState state)
        {
            Speed = Random.Range(settings.MinSpeed, settings.MaxSpeed);
            _state = state;
        }

        public void MoveToTarget(Vector3 target)
        {
            Vector3 dir = (target - _state.Movable.position).normalized;
            _state.Movable.position += dir * (Speed * Time.deltaTime);
        }
    }
}