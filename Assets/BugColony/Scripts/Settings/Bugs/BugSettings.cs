using BugColony.Scripts.Bugs;
using BugColony.Scripts.Settings.Bugs.Behaviours.Movements;
using BugColony.Scripts.Settings.Bugs.Behaviours.Splits;
using UnityEngine;

namespace BugColony.Scripts.Settings.Bugs
{
    public class BugSettings : ScriptableObject
    {
        [SerializeField] private BugView _prefab;
        public BugView Prefab => _prefab;
        
        [SerializeField] private int _nutritionalValue;
        public int NutritionalValue => _nutritionalValue;

        [SerializeField] private float _eatingDistance;
        public float EatingDistance => _eatingDistance;
        
        [SerializeField] private ResourceType _diet;
        public ResourceType Diet => _diet;

        [SerializeField] private bool _hasLifeTime;
        public bool HasLifeTime => _hasLifeTime;
        
        [SerializeField] private float _lifeTime;
        public float LifeTime => _lifeTime;
        
        [SerializeField] private BugMovementSettings _movementSettings;
        public BugMovementSettings MovementSettings => _movementSettings;
        
        [SerializeField] private BugSplitSettings _splitSettings;
        public BugSplitSettings SplitSettings => _splitSettings;
    }
}