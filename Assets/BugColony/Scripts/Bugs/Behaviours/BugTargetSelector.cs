using System.Collections.Generic;
using System.Linq;
using BugColony.Scripts.Foods;
using BugColony.Scripts.Settings.Bugs;
using R3;
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

        public void StartTargetSearch()
        {
            Observable
                .EveryUpdate()
                .Subscribe(_ =>
                {
                    if (!_state.Target)
                        _state.Target = GetNearestTarget();
                })
                .RegisterTo(_state.CancellationTokenSource.Token);
        }

        public Transform GetNearestTarget()
        {
            ResourceType diet = _settings.Diet;
            float minDistance = float.MaxValue;
            Transform target = null;
            Vector3 position = _state.Movable.Position;

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
        
        private void FindNearest(IEnumerable<ITarget> items, Vector3 position, ref float minDistance, ref Transform target)
        {
            foreach (var item in items)
            {
                float distance = (item.GetTarget().position - position).sqrMagnitude;
                if (distance < minDistance)
                {
                    minDistance = distance;
                    target = item.GetTarget();
                }
            }
        }
    
    }
}