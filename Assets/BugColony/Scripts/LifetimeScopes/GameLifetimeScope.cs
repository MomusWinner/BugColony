using BugColony.Scripts.Bugs;
using BugColony.Scripts.Foods;
using BugColony.Scripts.Settings;
using MessagePipe;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace BugColony.Scripts
{
    public class  GameLifetimeScope : LifetimeScope
    {
        [SerializeField] private GameSettings _gameSettings;
        
        protected override void Configure(IContainerBuilder builder)
        {
            var options = builder.RegisterMessagePipe();
            builder.RegisterBuildCallback(c => GlobalMessagePipe.SetProvider(c.AsServiceProvider()));
            
            builder.RegisterInstance(_gameSettings);
            builder.RegisterInstance(_gameSettings.BugCollection);
            builder.RegisterInstance(_gameSettings.FoodCollection);
            
            builder.Register<BugManager>(Lifetime.Singleton);
            builder.Register<BugFactory>(Lifetime.Singleton).As<IBugFactory>();
            builder.Register<BugSpawner>(Lifetime.Singleton);
            
            builder.Register<FoodManager>(Lifetime.Singleton);
            builder.Register<FoodFactory>(Lifetime.Singleton).As<IFoodFactory>();
            builder.Register<FoodSpawner>(Lifetime.Singleton);
            
            builder.RegisterEntryPoint<ScoreManager>().AsSelf();
            
            builder.RegisterEntryPoint<EntryPoint>();
        }
    }
}