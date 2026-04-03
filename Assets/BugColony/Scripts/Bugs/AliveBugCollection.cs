using BugColony.Scripts.Messages;
using MessagePipe;
using ObservableCollections;
using VContainer;

namespace BugColony.Scripts.Bugs
{
    public class AliveBugCollection 
    {
        public IObservableCollection<Bug> Bugs => _bugs;
        private readonly ObservableList<Bug> _bugs = new();
        
        [Inject]
        public AliveBugCollection(ISubscriber<BugCreatedMessage> createBugSubscriber)
        {
            createBugSubscriber.Subscribe(message =>
            {
                _bugs.Add(message.Bug);
                message.Bug.OnDie += b => _bugs.Remove(b);
            });
        }
    }
}