using System;
using BugColony.Helpers;
using BugColony.Scripts.Settings;
using BugColony.Scripts.Settings.Foods;
using R3;
using VContainer;
using Random = UnityEngine.Random;

namespace BugColony.Scripts.Foods
{
    public class FoodSpawner
    {
        [Inject] private IFoodFactory _factory;
        [Inject] private FoodCollectionSettings _foodSettings;
        [Inject] private GameSettings _gameSettings;

        public void Start()
        {
            if (_gameSettings.SpawnSettings.FoodSpawnInterval == 0) return;
            Observable
            .Interval(TimeSpan.FromSeconds(_gameSettings.SpawnSettings.FoodSpawnInterval))
            .Subscribe( _ => 
            {
                var bug = _factory.Create(_foodSettings.Foods[Random.Range(0, _foodSettings.Foods.Count)]);
                bug.transform.position = RandomHelper.InsideBounds(_gameSettings.ArenaBounds);
            });
        }
    }
}