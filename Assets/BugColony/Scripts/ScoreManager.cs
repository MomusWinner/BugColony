using BugColony.Scripts.Bugs;
using ObservableCollections;
using R3;
using VContainer;
using VContainer.Unity;

namespace BugColony.Scripts
{
    public class ScoreManager : IStartable
    {
        public Observable<int> TotalDeadWorkerBug => _totalDeadWorkerBug;
        public Observable<int> TotalDeadPredatorBug => _totalDeadPredatorBug;
        
        private readonly ReactiveProperty<int> _totalDeadWorkerBug = new();
        private readonly ReactiveProperty<int> _totalDeadPredatorBug = new();
            
        [Inject] private  AliveBugCollection _aliveBugCollection;
        
        public void Start()
        {
            _aliveBugCollection.Bugs.ObserveRemove().Subscribe( bug =>
            {
                ResourceType diet = bug.Value.Diet;
                
                if (diet.HasFlag(ResourceType.Bug))
                    _totalDeadPredatorBug.Value += 1;
                else if (diet.HasFlag(ResourceType.Food))
                    _totalDeadWorkerBug.Value += 1;
            });
        }
    }
}