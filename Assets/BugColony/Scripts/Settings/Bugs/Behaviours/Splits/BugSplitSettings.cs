using UnityEngine;

namespace BugColony.Scripts.Settings.Bugs.Behaviours.Splits
{
    public class BugSplitSettings : ScriptableObject
    {
        public int RequiredEnergyForSplitting => _requiredEnergyForSplitting;
        public float SpawnRadius=> _spawnRadius;
        
        [SerializeField] private int _requiredEnergyForSplitting;
        [SerializeField] private float _spawnRadius;
    }
}