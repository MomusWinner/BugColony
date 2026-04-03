using BugColony.Scripts.Messages;
using MessagePipe;
using ObservableCollections;
using VContainer;

namespace BugColony.Scripts.Bugs
{
    public class BugManager 
    {
        public IObservableCollection<Bug> Bugs => _bugs;
        private readonly ObservableList<Bug> _bugs = new();
        
        [Inject]
        public BugManager(ISubscriber<BugCreatedMessage> createBugSubscriber)
        {
            createBugSubscriber.Subscribe(message =>
            {
                Add(message.Bug);
                message.Bug.OnDie += Remove;
            });
        }

        private void Add(Bug bug)
        {
            _bugs.Add(bug);
        }

        private void Remove(Bug bug)
        {
            _bugs.Remove(bug);
        }
    }
}