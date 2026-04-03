using System;
using BugColony.Helpers;
using BugColony.Scripts.Settings;
using R3;
using VContainer;
using VContainer.Unity;

namespace BugColony.Scripts.Foods
{
    public class FoodSpawner : IStartable
    {
        [Inject] private IFoodFactory _factory;
        [Inject] private GameSettings _gameSettings;

        public void Start()
        {
            if (_gameSettings.SpawnerSettings.FoodSpawnInterval == 0) return;
            Observable
            .Interval(TimeSpan.FromSeconds(_gameSettings.SpawnerSettings.FoodSpawnInterval))
            .Subscribe( _ => 
            {
                var bug = _factory.Create();
                bug.transform.position = RandomHelper.InsideBounds(_gameSettings.ArenaBounds);
            });
        }
    }
}