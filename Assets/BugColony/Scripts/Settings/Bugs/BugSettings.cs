using BugColony.Scripts.Bugs;
using BugColony.Scripts.Settings.Bugs.Behaviours.Movements;
using BugColony.Scripts.Settings.Bugs.Behaviours.Splits;
using TriInspector;
using UnityEngine;

namespace BugColony.Scripts.Settings.Bugs
{
    public class BugSettings : ScriptableObject
    {
        public BugView Prefab => _prefab;
        
        public int NutritionalValue => _nutritionalValue;

        public float EatingDistance => _eatingDistance;
        
        public ResourceType Diet => _diet;

        public bool HasLifeTime => _hasLifeTime;
        
        public float LifeTime => _lifeTime;
        
        public BugMovementSettings MovementSettings => _movementSettings;
        
        public BugSplitSettings SplitSettings => _splitSettings;
        
        [SerializeField]
        private BugView _prefab;
        
        [SerializeField]
        private int _nutritionalValue;
        
        [SerializeField]
        private float _eatingDistance;
        
        [EnumToggleButtons]
        [SerializeField]
        private ResourceType _diet;
        
        [SerializeField]
        [Unit(UnitAttribute.Second)]
        private bool _hasLifeTime;
        
        [ShowIf(nameof(_hasLifeTime))]
        [SerializeField]
        private float _lifeTime;
        
        [SerializeField]
        private BugMovementSettings _movementSettings;
        
        [SerializeField]
        private BugSplitSettings _splitSettings;
    }
}