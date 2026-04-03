using System;
using BugColony.Scripts.Bugs.Behaviours;
using BugColony.Scripts.Bugs.Behaviours.Eating;
using BugColony.Scripts.Bugs.Behaviours.Movement;
using BugColony.Scripts.Bugs.Behaviours.Splits;
using BugColony.Scripts.Settings.Bugs;
using R3;
using UnityEngine;
using VContainer;

namespace BugColony.Scripts.Bugs
{
    public class Bug : IEatable, ITarget
    {
        public event Action<Bug> OnDie;
        
        public ResourceType ResourceType => ResourceType.Bug;
        public int Gen { get => _state.Gen; set => _state.Gen = value;}
        public ResourceType Diet => _settings.Diet;
        public IMovable Movable => _state.Movable;
        
        [Inject] private BugView _view;
        [Inject] private BugState _state;
        [Inject] private BugSettings _settings;
        [Inject] private IBugEating _eatingBehaviour;
        [Inject] private IBugMovement _movementBehaviour;
        [Inject] private IBugSplit _splitBehaviour;
        [Inject] private IBugTargetSelector _targetSelector;

        public void Start()
        {
            _view.Bug = this;
            _view.Destroyed += _ => _state.Reset();
            _view.OnCollided += eatable =>
            {
                if (IsEatable(eatable)) _eatingBehaviour.EatResource(eatable);
            };
            
            _state.Movable = _view;
            
            if (_settings.HasLifeTime)
            {
                Observable
                    .Interval(TimeSpan.FromSeconds(_settings.LifeTime))
                    .Subscribe(_ => Die())
                    .RegisterTo(_state.CancellationTokenSource.Token);
            }
            
            Observable
                .EveryUpdate(_state.CancellationTokenSource.Token)
                .Subscribe(_ => Update())
                .RegisterTo(_state.CancellationTokenSource.Token);
            
            _targetSelector.StartTargetSearch();
        }

        private void Update()
        {
            if (_state.Target)
                _movementBehaviour.MoveToTarget(_state.Target.position);
            _splitBehaviour.TrySplit();
        }

        public int Eat()
        {
            Die();
            return _settings.NutritionalValue;
        }

        public void MoveToTarget(Vector3 pos)
        {
            _movementBehaviour.MoveToTarget(pos);
        }

        public void EatResource(IEatable eatable) 
        {
            _eatingBehaviour.EatResource(eatable);
        }

        public void Die()
        {
            _state.Reset();
            _view.Destroy();
            OnDie?.Invoke(this);
        }
        
        public Transform GetTarget() => _view.GetTarget();

        private bool IsEatable(IEatable eatable)
        {
            if (eatable is Bug bug && bug.Gen == Gen)
                return false;
            return Diet.HasFlag(eatable.ResourceType);
        }
    }
}