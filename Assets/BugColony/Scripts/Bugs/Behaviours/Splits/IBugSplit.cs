namespace BugColony.Scripts.Bugs.Behaviours.Splits
{
    public interface IBugSplit
    {
        bool CanSplit();
        void Split();
        bool IsSplitting();
    }
}