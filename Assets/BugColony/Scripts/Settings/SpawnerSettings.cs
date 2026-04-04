using System.ComponentModel.DataAnnotations;
using BugColony.Scripts.Settings.Bugs;
using BugColony.Scripts.Settings.Foods;
using UnityEngine;

namespace BugColony.Scripts.Settings
{
    [CreateAssetMenu(fileName = "Spawner Settings", menuName = "Settings/Spawn", order = 0)]
    public class SpawnerSettings : ScriptableObject
    {
        public BugSettings Bug => _bug;
        public FoodSettings Food => _food;
        public float FoodSpawnInterval => _foodSpawnInterval;
        
        [Required]
        [SerializeField]
        private BugSettings _bug;
        [Required]
        [SerializeField]
        private FoodSettings _food;
        
        [SerializeField]
        private float _foodSpawnInterval;
    }
}