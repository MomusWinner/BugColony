using System;
using BugColony.Scripts.Bugs.Behaviours;
using BugColony.Scripts.Bugs.Behaviours.Eating;
using BugColony.Scripts.Bugs.Behaviours.Movement;
using BugColony.Scripts.Bugs.Behaviours.Splits;
using BugColony.Scripts.Settings.Bugs;
using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;
using VContainer;

namespace BugColony.Scripts.Bugs
{
    public class Bug : ITarget
    {
        public event Action<Bug> OnDie;
        public ResourceType ResourceType => ResourceType.Bug;
        public int Gen { get => _state.Gen; set => _state.Gen = value;}
        public ResourceType Diet => _settings.Diet;
        public Transform Movable => _state.Movable;
        
        [Inject] private BugView _view;
        [Inject] private BugState _state;
        [Inject] private BugSettings _settings;
        [Inject] private IBugEating _eatingBehaviour;
        [Inject] private IBugMovement _movementBehaviour;
        [Inject] private IBugSplit _splitBehaviour;
        [Inject] private IBugTargetSelector _targetSelector;
        
        private bool _isEating;

        public void Start()
        {
            _view.Destroyed += _ =>
            {
                if (IsAlive()) Die().Forget();
            };
            
            _state.Movable = _view.transform;
            
            if (_settings.HasLifeTime)
            {
                Observable
                    .Interval(TimeSpan.FromSeconds(_settings.LifeTime))
                    .Subscribe(_ => Die())
                    .RegisterTo(_state.OnDestroyCts.Token);
            }
            
            Observable
                .EveryUpdate(_state.OnDestroyCts.Token)
                .Subscribe(_ => Update())
                .RegisterTo(_state.OnDestroyCts.Token);
        }

        private void Update()
        {
            if (_isEating) return;
            if (_state.Target is null || !_state.Target.IsAlive()) 
                _state.Target = _targetSelector.GetTarget();
            
            if (_state.Target is not null && _state.Target.IsAlive())
            {
                if ((_state.Target.GetPosition() - Movable.position).magnitude <= _settings.EatingDistance)
                {
                    EatWithDelayAsync().Forget();
                }
                else
                {
                    _movementBehaviour.MoveToTarget(_state.Target.GetPosition());
                }
            }
            _splitBehaviour.TrySplit();
        }

        public int Eat()
        {
            Die().Forget();
            return _settings.NutritionalValue;
        }

        private async UniTaskVoid Die()
        {
            _state.IsDead = true;
            _state.Dispose();
            OnDie?.Invoke(this);
            if (_view.IsAlive())
            {
                await _view.StartDieAnimation();
                _view.Destroy();
            }
        }
        
        public Vector3 GetPosition() => Movable.position;

        public bool IsAlive() => !_state.IsDead;

        
        private async UniTaskVoid EatWithDelayAsync()
        {
            _isEating = true;
            await _view.StartEatAnimation(_state.Target.GetPosition());
            
            if (_state.OnDestroyCts.IsCancellationRequested)
                return;
            if (_state.Target != null && _state.Target.IsAlive())
                _state.Energy = _state.Target.Eat();
            _isEating = false;
        }
    }
}