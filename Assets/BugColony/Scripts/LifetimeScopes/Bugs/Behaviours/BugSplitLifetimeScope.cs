using System;
using System.Collections.Generic;
using BugColony.Scripts.Bugs.Behaviours.Splits;
using BugColony.Scripts.Settings.Bugs.Behaviours.Splits;
using VContainer;

namespace BugColony.Scripts.LifetimeScopes.Bugs.Behaviours
{
    public static class BugSplitLifetimeScope
    {
        private static Dictionary<Type, Type> _settingTypeToBehaviour = new()
        {
            { typeof(BugMutationSplitSettings), typeof(BugMutationSplit) },
        };
        
        public static void RegisterBugSplitBehaviour(this IContainerBuilder builder, BugSplitSettings settings)
        {
            if (!_settingTypeToBehaviour.TryGetValue(settings.GetType(), out var behaviour))
                throw new ArgumentException("Your bug split settings are not registered.");
            builder.RegisterInstance(settings).As(settings.GetType());
            builder.Register(behaviour, Lifetime.Scoped).As(typeof(IBugSplit));
        }
    }
}