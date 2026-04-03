using BugColony.Scripts.Messages;
using BugColony.Scripts.Settings.Foods;
using MessagePipe;
using VContainer;
using VContainer.Unity;

namespace BugColony.Scripts.Foods
{
    public class FoodFactory : IFoodFactory
    {
        [Inject] private IObjectResolver _container;
        [Inject] private IPublisher<CreateFoodMessage> _publisher;

        public Food Create(FoodSettings settings)
        {
            Food food = _container.Instantiate(settings.Prefab);
            food.NutritionalValue = settings.NutritionalValue;
            _publisher.Publish(new CreateFoodMessage(food));
            return food;
        }
    }

    public interface IFoodFactory
    {
        Food Create(FoodSettings settings);
    }
}