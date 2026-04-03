using BugColony.Scripts.Settings.Bugs;
using BugColony.Scripts.Settings.Foods;
using UnityEngine;

namespace BugColony.Scripts.Settings
{
    [CreateAssetMenu(fileName = "Game Settings", menuName = "Settings/Game", order = 0)]
    public class GameSettings : ScriptableObject
    {
        [SerializeField] private Bounds _arenaBounds;
        public Bounds ArenaBounds => _arenaBounds;
        
        [SerializeField] private SpawnSettings _spawnSettings;
        public SpawnSettings SpawnSettings => _spawnSettings;
        
        [SerializeField] private BugCollectionSettings _bugCollection;
        public BugCollectionSettings BugCollection => _bugCollection;
        
        [SerializeField] private FoodCollectionSettings _foodCollection;
        public FoodCollectionSettings FoodCollection => _foodCollection;
    }
}