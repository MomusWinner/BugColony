using System;
using BugColony.Helpers;
using BugColony.Scripts.Settings;
using ObservableCollections;
using R3;
using VContainer;
using VContainer.Unity;

namespace BugColony.Scripts.Bugs
{
    public class BugSpawner: IStartable, IDisposable
    {
        [Inject] private IBugFactory _factory;
        [Inject] private GameSettings _gameSettings;
        [Inject] private AliveBugCollection _aliveBugCollection;
        private IDisposable _subscription;
        
        public void Start()
        {
            SpawnWorkerBug();
            _subscription = _aliveBugCollection.Bugs.ObserveRemove().Subscribe(_ =>
            {
                if (_aliveBugCollection.Bugs.Count != 0) return;
                SpawnWorkerBug();
            });
        }

        private void SpawnWorkerBug()
        {
            var bug = _factory.Create(_gameSettings.SpawnerSettings.Bug);
            bug.Position = RandomHelper.InsideBounds(_gameSettings.ArenaBounds);
        }

        public void Dispose()
        {
            _subscription?.Dispose();
        }
    }
}