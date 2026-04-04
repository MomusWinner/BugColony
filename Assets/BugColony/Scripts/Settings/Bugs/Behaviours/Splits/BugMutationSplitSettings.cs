using TriInspector;
using UnityEngine;

namespace BugColony.Scripts.Settings.Bugs.Behaviours.Splits
{
    [CreateAssetMenu(fileName = "Mutation Split Settings", menuName = "Settings/Bugs/Splits/Mutation", order = 0)]
    public class BugMutationSplitSettings : BugSplitSettings
    {
        public float Chance => _chance;
        public int RequiredBugCount => _requiredBugCount;
        public BugSettings MutatedBug => _mutatedBug;
        
        [Title("Mutation")]
        [SerializeField]
        [Range(0, 1)]
        private float _chance;
        
        [SerializeField]
        private int _requiredBugCount;
        
        [SerializeField]
        private BugSettings _mutatedBug;
    }
}