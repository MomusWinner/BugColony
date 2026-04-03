using System;

namespace BugColony.Scripts
{
    [Flags]
    public enum ResourceType
    {
        None = 0,
        All = ~0,
        Food = 1 << 0,
        Bug = 1 << 1
    }
}