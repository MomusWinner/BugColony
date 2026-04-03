using System;
using BugColony.Helpers;
using BugColony.Scripts.Settings;
using BugColony.Scripts.Settings.Bugs;
using ObservableCollections;
using VContainer;
using R3;

namespace BugColony.Scripts.Bugs
{
    public class BugSpawner: IDisposable
    {
        [Inject] private IBugFactory _factory;
        [Inject] private BugCollectionSettings _bugSettings;
        [Inject] private GameSettings _gameSettings;
        [Inject] private BugManager _bugManager;
        private IDisposable _subscription;
        
        public void Start()
        {
            SpawnWorkerBug();
            _subscription = _bugManager.Bugs.ObserveRemove().Subscribe(_ =>
            {
                if (_bugManager.Bugs.Count != 0) return;
                SpawnWorkerBug();
            });
        }

        private void SpawnWorkerBug()
        {
            var bug = _factory.Create(_bugSettings.BugSettings[0]);
            bug.Movable.position = RandomHelper.InsideBounds(_gameSettings.ArenaBounds);
        }

        public void Dispose()
        {
            _subscription?.Dispose();
        }
    }
}