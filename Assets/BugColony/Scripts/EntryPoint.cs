using System;
using BugColony.Scripts.Bugs;
using BugColony.Scripts.Foods;
using VContainer;
using VContainer.Unity;

namespace BugColony.Scripts
{
    public class EntryPoint : IStartable, IDisposable
    {
        [Inject] private BugSpawner _bugSpawner;
        [Inject] private FoodSpawner _foodSpawner;
        
        public void Start()
        {
            _bugSpawner.Start();
            _foodSpawner.Start();
        }

        public void Dispose()
        {
            _bugSpawner?.Dispose();
        }
    }
}