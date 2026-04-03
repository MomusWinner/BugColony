using System;
using BugColony.Scripts.Settings.Bugs;
using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;
using VContainer;

namespace BugColony.Scripts.Bugs
{
    public abstract class Bug : ITarget
    {
        public event Action<Bug> OnDie;
        public virtual ResourceType ResourceType => ResourceType.Bug;
        public bool Enabled { get => State.Enabled; set => State.Enabled = value; }
        public int Gen { get => State.Gen; set => State.Gen = value;}
        public ResourceType Diet => Settings.Diet;
        public Vector3 Position {get => View.transform.position; set => View.transform.position = value;}
        public BugView View => _view;
        
        [Inject] protected BugState State;
        [Inject] protected BugSettings Settings;
        [Inject] private BugView _view;
        
        public virtual void Start()
        {
            View.Destroyed += _ =>
            {
                if (IsAlive()) State.Dispose();;
            };
            
            if (Settings.HasLifeTime)
            {
                Observable
                    .Interval(TimeSpan.FromSeconds(Settings.LifeTime))
                    .Subscribe(_ => Die().Forget())
                    .RegisterTo(State.OnDestroyCts.Token);
            }
            
            Observable
                .EveryUpdate(State.OnDestroyCts.Token)
                .Subscribe(_ =>
                {
                    if (IsAlive() && Enabled) Update();
                })
                .RegisterTo(State.OnDestroyCts.Token);
        }

        protected abstract void Update();

        public virtual int Eat()
        {
            Die().Forget();
            return Settings.NutritionalValue;
        }

        protected virtual async UniTaskVoid Die()
        {
            State.IsDead = true;
            State.Dispose();
            OnDie?.Invoke(this);
            if (View.IsAlive())
            {
                await View.StartDieAnimation();
                View.Destroy();
            }
        }
        
        public virtual Vector3 GetPosition() => View.transform.position;

        public virtual bool IsAlive() => !State.IsDead && State.Enabled;
    }
}