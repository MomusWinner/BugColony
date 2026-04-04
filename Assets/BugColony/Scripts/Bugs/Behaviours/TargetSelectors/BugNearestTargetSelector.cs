using System.Collections.Generic;
using System.Linq;
using BugColony.Scripts.Foods;
using BugColony.Scripts.Settings.Bugs;
using UnityEngine;

namespace BugColony.Scripts.Bugs.Behaviours.TargetSelectors
{
    public class BugNearestTargetSelector : IBugTargetSelector
    {
        private readonly AliveBugCollection _aliveBugCollection;
        private readonly AliveFoodCollection _aliveFoodCollection;
        private readonly BugState _state;
        private readonly BugView _view;
        private readonly BugSettings _settings;

        public BugNearestTargetSelector(AliveBugCollection aliveBugCollection,
            AliveFoodCollection aliveFoodManage,
            BugState state,
            BugView view,
            BugSettings settings)
        {
            _aliveBugCollection = aliveBugCollection;
            _aliveFoodCollection = aliveFoodManage;
            _state = state;
            _view = view;
            _settings = settings;
        }

        public ITarget GetTarget()
        {
            ResourceType diet = _settings.Diet;
            float minDistance = float.MaxValue;
            ITarget target = null;
            Vector3 position = _view.transform.position;

            if (diet.HasFlag(ResourceType.Bug))
            {
                FindNearest(
                    _aliveBugCollection.Bugs.Where(b => b.Gen != _state.Gen),
                    position,
                    ref minDistance,
                    ref target
                );
            }
            
            if (diet.HasFlag(ResourceType.Food))
            {
                FindNearest(
                    _aliveFoodCollection.Foods,
                    position,
                    ref minDistance,
                    ref target
                );
            }

            return target;
        }
        
        private void FindNearest(IEnumerable<ITarget> targets, Vector3 position, ref float minDistance, ref ITarget target)
        {
            foreach (var t in targets)
            {
                if (!t.IsAlive()) continue;
                float distance = (t.GetPosition() - position).sqrMagnitude;
                if (!(distance < minDistance)) continue;
                minDistance = distance;
                target =  t;
            }
        }
    }
}