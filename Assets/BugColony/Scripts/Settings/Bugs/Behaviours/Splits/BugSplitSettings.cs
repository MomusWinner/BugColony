using UnityEngine;

namespace BugColony.Scripts.Settings.Bugs.Behaviours.Splits
{
    public class BugSplitSettings : ScriptableObject
    {
        [SerializeField] private int _requiredEnergyForSplitting;
        public int RequiredEnergyForSplitting => _requiredEnergyForSplitting;
    }
}