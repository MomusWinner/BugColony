using System;
using BugColony.Scripts.Bugs.Behaviours.Splits;
using BugColony.Scripts.Settings.Bugs.Behaviours.Splits;
using VContainer;

namespace BugColony.Scripts.LifetimeScopes.Bugs.Behaviours
{
    public static class BugSplitLifetimeScope
    {
        public static void RegisterBugSplitBehaviour(this IContainerBuilder builder, BugSplitSettings settings)
        {
            switch (settings)
            {
                case BugMutationSplitSettings walking:
                    builder.RegisterInstance(walking);
                    builder.Register<IBugSplit, BugCommonSplit>(Lifetime.Scoped);
                    return;
                default:
                    throw new NotImplementedException();
            }
        }
    }
}