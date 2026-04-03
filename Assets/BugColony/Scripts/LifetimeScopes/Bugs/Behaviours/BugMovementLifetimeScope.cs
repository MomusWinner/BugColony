using System;
using System.Collections.Generic;
using BugColony.Scripts.Bugs.Behaviours.Movement;
using BugColony.Scripts.Settings.Bugs.Behaviours.Movements;
using VContainer;

namespace BugColony.Scripts.LifetimeScopes.Bugs.Behaviours
{
    public static class BugMovementLifetimeScope
    {
        private static Dictionary<Type, Type> _settingTypeToBehaviour = new()
        {
            { typeof(BugWalkingSettings), typeof(BugWalking) },
        };
        
        public static void RegisterBugMovementBehaviour(this IContainerBuilder builder, BugMovementSettings settings)
        {
            if (!_settingTypeToBehaviour.TryGetValue(settings.GetType(), out var behaviour))
                throw new ArgumentException("Your bug movement settings are not registered.");
            builder.RegisterInstance(settings).As(settings.GetType());
            builder.Register(behaviour, Lifetime.Scoped).As(typeof(IBugMovement));
        }
    }
}