using System;
using BugColony.Scripts.Bugs.Behaviours.Eating;
using BugColony.Scripts.Bugs.Behaviours.Movement;
using BugColony.Scripts.Settings.Bugs.Behaviours.Movements;
using VContainer;

namespace BugColony.Scripts.LifetimeScopes.Bugs.Behaviours
{
    public static class BugMovementLifetimeScope
    {
        public static void RegisterBugMovementBehaviour(this IContainerBuilder builder, BugMovementSettings settings)
        {
            switch (settings)
            {
                case BugWalkingSettings walking:
                    builder.RegisterInstance(walking);
                    builder.Register<IBugMovement, BugWalking>(Lifetime.Scoped);
                    return;
                default:
                    throw new NotImplementedException();
            }
        }
    }
}