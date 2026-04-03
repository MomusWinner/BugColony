using UnityEngine;

namespace BugColony.Scripts.Settings
{
    [CreateAssetMenu(fileName = "Game Settings", menuName = "Settings/Game", order = 0)]
    public class GameSettings : ScriptableObject
    {
        [SerializeField] private Bounds _arenaBounds;
        public Bounds ArenaBounds => _arenaBounds;
        
        [SerializeField] private SpawnerSettings _spawnerSettings;
        public SpawnerSettings SpawnerSettings => _spawnerSettings;
    }
}