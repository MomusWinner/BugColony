using UnityEngine;
using UnityEngine.Serialization;

namespace BugColony.Scripts.Settings.Bugs.Behaviours.Splits
{
    [CreateAssetMenu(fileName = "Mutation Split Settings", menuName = "Settings/Bugs/Splits/Mutation", order = 0)]
    public class BugMutationSplitSettings : BugSplitSettings
    {
        [SerializeField]
        [Range(0, 1)]
        private float _chance;
        public float Chance => _chance;
        
        [SerializeField]
        private int _requiredBugCount;
        public int RequiredBugCount => _requiredBugCount;
        
        [SerializeField]
        private BugSettings _mutatedBug;
        public BugSettings MutatedBug => _mutatedBug;
    }
}