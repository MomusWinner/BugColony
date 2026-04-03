using BugColony.Scripts.Settings.Bugs;
using BugColony.Scripts.Settings.Bugs.Behaviours.Splits;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace BugColony.Scripts.Bugs.Behaviours.Splits
{
    public class BugMutationSplit : IBugSplit
    {
        private readonly BugState _state;
        private readonly BugView _view;
        private readonly IBugFactory _factory;
        private readonly BugSettings _settings;
        private readonly BugMutationSplitSettings _splitSettings;
        private readonly AliveBugCollection _aliveBugCollection;
        private bool _isSplitting;
        
        public BugMutationSplit(
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

        public bool CanSplit() => _state.Energy >= _splitSettings.RequiredEnergyForSplitting && !_isSplitting;

        public void Split()
        {
            if (!CanSplit()) return;
            
            Bug newBug = null;
            if (_splitSettings.RequiredBugCount <= _aliveBugCollection.Bugs.Count)
            {
                if (Random.value <= _splitSettings.Chance)
                    newBug = _factory.Create(_splitSettings.MutatedBug);
            }
            newBug ??= _factory.Create(_settings);
            
            Split(newBug).Forget();
        }

        public bool IsSplitting()
        {
            return _isSplitting;
        }

        private async UniTaskVoid Split(Bug clone)
        {
            clone.Enabled = false;
            _state.Enabled = false;
            Vector3 startPos = _view.transform.position;
            _state.Energy = 0;
            
            int newGen = Random.Range(0, int.MaxValue);
            clone.Gen = newGen;
            _state.Gen = newGen;
            
            _view.transform.position = startPos;
            clone.View.transform.position = startPos;
            await UniTask.WhenAll(
                _view.StartSplitAnimation(RandomPosition(startPos)),
                clone.View.StartSplitAnimation(RandomPosition(startPos))
            );
            _isSplitting = false;
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