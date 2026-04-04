using BugColony.Scripts.Bugs;
using BugColony.Scripts.Foods;
using BugColony.Scripts.Settings;
using MessagePipe;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace BugColony.Scripts.LifetimeScopes
{
    public class  GameLifetimeScope : LifetimeScope
    {
        [SerializeField] private GameSettings _gameSettings;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterMessagePipe();
            builder.RegisterBuildCallback(c => GlobalMessagePipe.SetProvider(c.AsServiceProvider()));
            
            builder.RegisterInstance(_gameSettings);
            
            builder.Register<AliveBugCollection>(Lifetime.Singleton);
            builder.Register<IBugFactory, BugFactory>(Lifetime.Singleton);
            
            builder.Register<AliveFoodCollection>(Lifetime.Singleton);
            builder.RegisterEntryPoint<FoodPool>().As<IFoodFactory>();
            
            builder.RegisterEntryPoint<ScoreManager>().AsSelf();

            builder.RegisterEntryPoint<ApplicationInitializer>();
            builder.RegisterEntryPoint<BugSpawner>();
            builder.RegisterEntryPoint<FoodSpawner>();
        }
    }
}