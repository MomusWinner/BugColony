using System.Threading;
using BugColony.Scripts.Settings.Bugs;
using BugColony.Scripts.Settings.Bugs.Behaviours.Splits;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace BugColony.Scripts.Bugs.Behaviours.Splits
{
    public class BugMutationSplittingBehaviour : IBugSplittingBehaviour
    {
        private readonly BugState _state;
        private readonly BugView _view;
        private readonly IBugFactory _factory;
        private readonly BugSettings _settings;
        private readonly BugMutationSplitSettings _splitSettings;
        private readonly AliveBugCollection _aliveBugCollection;
        
        public BugMutationSplittingBehaviour(
            BugSettings settings,
            BugMutationSplitSettings splitSettings,
            BugState state,
            BugView view,
            IBugFactory factory,
            AliveBugCollection aliveBugCollection)
        {
            _settings = settings;
            _splitSettings = splitSettings;
            _state = state;
            _view = view;
            _factory = factory;
            _aliveBugCollection = aliveBugCollection;
        }

        public bool CanSplit() => _state.Energy >= _splitSettings.RequiredEnergyForSplitting;

        public async UniTask Split(CancellationToken ct)
        {
            if (!CanSplit()) return;
            
            Bug clone = null;
            if (_splitSettings.RequiredBugCount <= _aliveBugCollection.Bugs.Count)
            {
                if (Random.value <= _splitSettings.Chance)
                    clone = _factory.Create(_splitSettings.MutatedBug);
            }
            clone ??= _factory.Create(_settings);
            
            clone.Enabled = false;
            _state.Enabled = false;
            Vector3 startPos = _view.transform.position;
            _state.Energy = 0;
            _state.CreationTime = Time.time;
            
            int newGen = Random.Range(0, int.MaxValue);
            clone.Gen = newGen;
            _state.Gen = newGen;
            
            _view.transform.position = startPos;
            clone.View.transform.position = startPos;
            await UniTask.WhenAll(
                _view.StartSplitAnimation(RandomPosition(startPos), ct),
                clone.View.StartSplitAnimation(RandomPosition(startPos), ct)
            );
            clone.Enabled = true;
            _state.Enabled = true;
        }

        private Vector3 RandomPosition(Vector3 origin)
        {
            Vector2 circle = Random.insideUnitCircle;
            return origin + new Vector3(circle.x, origin.y, circle.y) * _splitSettings.SpawnRadius;
        }
    }
}