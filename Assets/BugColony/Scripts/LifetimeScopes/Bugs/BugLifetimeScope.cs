using System;
using System.Collections.Generic;
using BugColony.Scripts.Bugs;
using BugColony.Scripts.Bugs.Behaviours.EatingBehaviours;
using BugColony.Scripts.Bugs.Behaviours.Splits;
using BugColony.Scripts.Bugs.Behaviours.TargetSelectors;
using BugColony.Scripts.LifetimeScopes.Bugs.Behaviours;
using BugColony.Scripts.Settings.Bugs;
using VContainer;
using VContainer.Unity;

namespace BugColony.Scripts.LifetimeScopes.Bugs
{
    public static class BugLifetimeScope
    {
        private static Dictionary<Type, Type> _settingTypeToBehaviour = new()
        {
            { typeof(CommonBugSettings), typeof(CommonBug) },
        };
        
        public static void RegisterBug(this IContainerBuilder builder, BugSettings settings)
        {
            builder.RegisterInstance(settings);
            builder.Register(_ => new BugState(), Lifetime.Scoped);
            builder.RegisterBugMovementBehaviour(settings.MovementSettings);
            builder.RegisterBugSplitBehaviour(settings.SplitSettings);
            builder.Register<IBugTargetFinder, BugNearestTargetFinder>(Lifetime.Scoped);
            builder.Register<IBugEatingBehaviour, BugEatingBehaviour>(Lifetime.Scoped);
            builder.Register<IBugSplittingBehaviour, BugMutationSplittingBehaviour>(Lifetime.Scoped);
            builder.RegisterComponentInNewPrefab(settings.Prefab, Lifetime.Scoped);
            
            if (!_settingTypeToBehaviour.TryGetValue(settings.GetType(), out var bug))
                throw new ArgumentException("Your bug settings are not registered.");
            builder.Register(bug, Lifetime.Scoped).As(typeof(Bug));
        }
    }
}