using UnityEngine;

namespace BugColony.Scripts.Settings
{
    [CreateAssetMenu(fileName = "Spawn Settings", menuName = "Settings/Spawn", order = 0)]
    public class SpawnSettings : ScriptableObject
    {
        [SerializeField] private float _workerBugSpawnInterval;
        public float WorkerBugSpawnInterval => _workerBugSpawnInterval;
        
        [SerializeField] private float _foodSpawnInterval;
        public float FoodSpawnInterval => _foodSpawnInterval;
    }
}