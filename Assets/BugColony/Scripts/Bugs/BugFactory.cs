using BugColony.Scripts.Foods;
using BugColony.Scripts.LifetimeScopes.Bugs;
using BugColony.Scripts.Messages;
using BugColony.Scripts.Settings.Bugs;
using MessagePipe;
using VContainer;

namespace BugColony.Scripts.Bugs
{
    public class BugFactory : IBugFactory
    {
        [Inject] private IObjectResolver _container;
        
        [Inject] private FoodManager _foodManager;
        [Inject] private BugManager _bugManager;
        [Inject] private IPublisher<BugCreatedMessage> _createBugPublisher;
        
        public Bug Create(BugSettings settings)
        {
            var scope = _container.CreateScope(builder => builder.RegisterBug(settings)); 
            Bug bug = scope.Resolve<Bug>();
            bug.Start();
            _createBugPublisher.Publish(new BugCreatedMessage(bug));
            return bug;
        }
    }
}