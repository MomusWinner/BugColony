using System.Collections.Generic;
using System.Linq;
using BugColony.Scripts.Foods;
using BugColony.Scripts.Settings.Bugs;
using UnityEngine;

namespace BugColony.Scripts.Bugs.Behaviours
{
    public class BugTargetSelector : IBugTargetSelector
    {
        private readonly BugManager _bugManager;
        private readonly FoodManager _foodManager;
        private readonly BugState _state;
        private readonly BugSettings _settings;

        public BugTargetSelector(BugManager bugManager, FoodManager foodManage, BugState state, BugSettings settings)
        {
            _bugManager = bugManager;
            _foodManager = foodManage;
            _state = state;
            _settings = settings;
        }

        public ITarget GetTarget()
        {
            ResourceType diet = _settings.Diet;
            float minDistance = float.MaxValue;
            ITarget target = null;
            Vector3 position = _state.Movable.position;

            if (diet.HasFlag(ResourceType.Bug))
            {
                FindNearest(
                    _bugManager.Bugs.Where(b => b.Gen != _state.Gen),
                    position,
                    ref minDistance,
                    ref target
                );
            }
            
            if (diet.HasFlag(ResourceType.Food))
            {
                FindNearest(
                    _foodManager.Foods,
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