using BugColony.Scripts.Messages;
using MessagePipe;
using ObservableCollections;
using VContainer;

namespace BugColony.Scripts.Foods
{
    public class AliveFoodCollection 
    {
        public IObservableCollection<FoodView> Foods => _foods;
        private readonly ObservableList<FoodView> _foods = new();
        
        [Inject]
        public AliveFoodCollection(ISubscriber<FoodCreatedMessage> createFoodSubscriber)
        {
            createFoodSubscriber.Subscribe( message =>
            {
                _foods.Add(message.Food);
                message.Food.Destroyed += f => _foods.Remove(f);
            });
        }
    }
}