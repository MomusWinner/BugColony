using BugColony.Scripts.Bugs;

namespace BugColony.Scripts
{
    public interface IEatable
    {
        ResourceType ResourceType { get; }
        // returns nutritional values  
        int Eat();
    }
}