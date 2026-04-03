using BugColony.Scripts.Settings.Bugs;

namespace BugColony.Scripts.Bugs
{
    public interface IBugFactory
    {
        Bug Create(BugSettings settings);
    }
}