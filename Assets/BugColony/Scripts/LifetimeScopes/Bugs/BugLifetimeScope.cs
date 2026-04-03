using BugColony.Scripts.Bugs;
using BugColony.Scripts.Bugs.Behaviours;
using BugColony.Scripts.Bugs.Behaviours.Eating;
using BugColony.Scripts.Bugs.Behaviours.Splits;
using BugColony.Scripts.LifetimeScopes.Bugs.Behaviours;
using BugColony.Scripts.Settings.Bugs;
using VContainer;
using VContainer.Unity;

namespace BugColony.Scripts.LifetimeScopes.Bugs
{
    public static class BugLifetimeScope
    {
        public static void RegisterBug(this IContainerBuilder builder, BugSettings settings)
        {
            builder.RegisterInstance(settings);
            builder.Register(_ => new BugState(), Lifetime.Scoped);
            builder.RegisterBugMovementBehaviour(settings.MovementSettings);
            builder.RegisterBugSplitBehaviour(settings.SplitSettings);
            builder.Register<IBugEating, BugCommonEating>(Lifetime.Scoped);
            builder.Register<IBugTargetSelector, BugTargetSelector>(Lifetime.Scoped);
            builder.Register<IBugSplit, BugCommonSplit>(Lifetime.Scoped);
            builder.RegisterComponentInNewPrefab(settings.Prefab, Lifetime.Scoped);
            builder.Register<Bug>(Lifetime.Scoped);
        }
    }
}