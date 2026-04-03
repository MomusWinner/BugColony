using System.Collections.Generic;
using UnityEngine;

namespace BugColony.Scripts.Settings.Bugs
{
    [CreateAssetMenu(fileName = "Bug Collection", menuName = "Settings/Bugs/Collection", order = 0)]
    public class BugCollectionSettings : ScriptableObject
    {
        [SerializeField] private List<BugSettings> _bugSettings;
        public List<BugSettings> BugSettings => _bugSettings;
    }
}