using UnityEngine;

namespace BugColony.Scripts.Settings.Bugs.Behaviours.EatingSettings
{
    [CreateAssetMenu(fileName = "Bug Common Eating Settings", menuName = "Settings/Bugs/Eating/Common", order = 0)]
    public class BugCommonEatingSettings : BugEatingSettings
    {
        public float Delay => _delay;
        public float EatingDistance => _eatingDistance;
        
        [SerializeField] private float _delay;
        [SerializeField] private float _eatingDistance;
    }
}