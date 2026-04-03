using BugColony.Scripts.Settings.Bugs;
using BugColony.Scripts.Settings.Bugs.Behaviours.Splits;
using UnityEngine;

namespace BugColony.Scripts.Bugs.Behaviours.Splits
{
    public class BugCommonSplit : IBugSplit
    {
        private readonly BugState _state;
        private readonly IBugFactory _factory;
        private readonly BugSettings _settings;
        private readonly BugMutationSplitSettings _splitSettings;
        private readonly BugManager _bugManager;
        
        public BugCommonSplit(
            BugSettings settings,
            BugMutationSplitSettings splitSettings,
            BugState state,
            IBugFactory factory,
            BugManager bugManager)
        {
            _settings = settings;
            _splitSettings = splitSettings;
            _state = state;
            _factory = factory;
            _bugManager = bugManager;
        }
        
        public bool TrySplit()
        {
            if (_state.Energy < _splitSettings.RequiredEnergyForSplitting) return false;
            
            Bug newBug = null;
            if (_splitSettings.RequiredBugCount <= _bugManager.Bugs.Count)
            {
                if (Random.value <= _splitSettings.Chance)
                    newBug = _factory.Create(_splitSettings.MutatedBug);
            }
            newBug ??= _factory.Create(_settings);
            
            _state.Energy = 0;
            
            int newGen = Random.Range(0, int.MaxValue);
            newBug.Gen = newGen;
            _state.Gen = newGen;
            
            newBug.Movable.position = _state.Movable.position;

            return true;
        }
    }
}