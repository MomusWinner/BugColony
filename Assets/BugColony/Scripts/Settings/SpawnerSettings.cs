using BugColony.Scripts.Settings.Bugs;
using BugColony.Scripts.Settings.Foods;
using UnityEngine;

namespace BugColony.Scripts.Settings
{
    [CreateAssetMenu(fileName = "Spawner Settings", menuName = "Settings/Spawn", order = 0)]
    public class SpawnerSettings : ScriptableObject
    {
        [SerializeField] private BugSettings _bug;
        public BugSettings Bug => _bug;
        
        [SerializeField] private FoodSettings _food;
        public FoodSettings Food => _food;
        
        [SerializeField] private float _foodSpawnInterval;
        public float FoodSpawnInterval => _foodSpawnInterval;
    }
}