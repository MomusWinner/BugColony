using BugColony.Scripts.Foods;

namespace BugColony.Scripts.Messages
{
    public class FoodCreatedMessage
    {
        public FoodView Food { get; private set; }

        public FoodCreatedMessage(FoodView food)
        {
            Food = food;
        }
    }
}