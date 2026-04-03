using System;
using System.Collections.Generic;
using BugColony.Scripts.Bugs.Behaviours.Eating;
using BugColony.Scripts.Bugs.Behaviours.Movement;
using BugColony.Scripts.Foods;
using BugColony.Scripts.LifetimeScopes.Bugs;
using BugColony.Scripts.Messages;
using BugColony.Scripts.Settings.Bugs;
using MessagePipe;
using VContainer;

namespace BugColony.Scripts.Bugs
{
    public enum MovementType
    {
        Walking
    }

    public enum EatingType
    {
        Common,
        Delayed,
    }
        
    public class BugFactory : IBugFactory
    {
        [Inject] private IObjectResolver _container;
        
        [Inject] private FoodManager _foodManager;
        [Inject] private BugManager _bugManager;
        [Inject] private IPublisher<BugCreatedMessage> _createBugPublisher;
        
        private readonly Dictionary<EatingType, Type> _eatingTypeToBehaviour = new()
        {
            { EatingType.Common, typeof(BugCommonEating) },
            { EatingType.Delayed, typeof(BugDelayEating) },
        };
        
        private readonly Dictionary<MovementType, Type> _movementTypeToBehaviour = new()
        {
            { MovementType.Walking, typeof(BugWalking) },
        };

        public Bug Create(BugSettings settings)
        {
            var scope = _container.CreateScope(builder => builder.RegisterBug(settings)); 
            Bug bug = scope.Resolve<Bug>();
            bug.Start();
            _createBugPublisher.Publish(new BugCreatedMessage(bug));
            return bug;
        }
    }
}