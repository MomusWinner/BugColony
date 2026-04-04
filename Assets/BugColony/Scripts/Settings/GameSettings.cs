using TriInspector;
using UnityEngine;

namespace BugColony.Scripts.Settings
{
    [CreateAssetMenu(fileName = "Game Settings", menuName = "Settings/Game", order = 0)]
    public class GameSettings : ScriptableObject
    {
        public int TargetFPS => _targetFPS;
        public Bounds ArenaBounds => _arenaBounds;
        public SpawnerSettings SpawnerSettings => _spawnerSettings;
        
        
        [SerializeField]
        private int _targetFPS = 60;
        
        [SerializeField]
        private Bounds _arenaBounds;
        
        [Required]
        [SerializeField]
        private SpawnerSettings _spawnerSettings;
    }
}