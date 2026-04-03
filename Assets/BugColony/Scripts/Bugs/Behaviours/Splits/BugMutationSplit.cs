using BugColony.Scripts.Settings.Bugs;
using BugColony.Scripts.Settings.Bugs.Behaviours.Splits;
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
        
        public bool TrySplit()
        {
            if (_state.Energy < _splitSettings.RequiredEnergyForSplitting) return false;
            
            Bug newBug = null;
            if (_splitSettings.RequiredBugCount <= _aliveBugCollection.Bugs.Count)
            {
                if (Random.value <= _splitSettings.Chance)
                    newBug = _factory.Create(_splitSettings.MutatedBug);
            }
            newBug ??= _factory.Create(_settings);
            
            _state.Energy = 0;
            
            int newGen = Random.Range(0, int.MaxValue);
            newBug.Gen = newGen;
            _state.Gen = newGen;
            
            newBug.Position = _view.transform.position;

            return true;
        }
    }
}