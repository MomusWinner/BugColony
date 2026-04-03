using UnityEngine;

namespace BugColony.Scripts.Settings.Bugs.Behaviours.Movements
{
    [CreateAssetMenu(fileName = "Bug Movement Settings", menuName = "Settings/Bugs/Movements/Walking", order = 0)]
    public class BugWalkingSettings : BugMovementSettings
    {
        public float MaxSpeed => _maxSpeed;
        public float MinSpeed => _minSpeed;
        
        [SerializeField] private float _minSpeed;
        [SerializeField] private float _maxSpeed;
    }
}