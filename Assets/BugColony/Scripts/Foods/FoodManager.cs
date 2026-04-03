using BugColony.Scripts.Messages;
using MessagePipe;
using ObservableCollections;
using VContainer;

namespace BugColony.Scripts.Foods
{
    public class FoodManager 
    {
        public IObservableCollection<Food> Foods => _foods;
        private readonly ObservableList<Food> _foods = new();
        
        [Inject]
        public FoodManager(ISubscriber<CreateFoodMessage> createFoodSubscriber)
        {
            createFoodSubscriber.Subscribe( message =>
            {
                Add(message.Food);
                message.Food.Destroyed += Remove;
            } );
        }

        public void Add(Food food)
        {
            _foods.Add(food);
        }

        public void Remove(Food food)
        {
            _foods.Remove(food);
        }
    }
}