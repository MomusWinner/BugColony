using BugColony.Scripts.Foods;

namespace BugColony.Scripts.Messages
{
    public class CreateFoodMessage
    {
        public Food Food { get; private set; }

        public CreateFoodMessage(Food food)
        {
            Food = food;
        }
    }
}