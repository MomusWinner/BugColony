using System;
using System.Threading;
using BugColony.Scripts.Settings.Bugs;
using Cysharp.Threading.Tasks;
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
            State.CreationTime = Time.time;
            
            View.Destroyed += _ =>
            {
                if (IsAlive()) State.Dispose();;
            };
            
            StartUpdating().Forget();
        }

        protected abstract UniTask Update(CancellationToken token);
        private async UniTaskVoid StartUpdating()
        {
            try
            {
                var token = State.OnDestroyCts.Token;
                while (!State.OnDestroyCts.IsCancellationRequested)
                {
                    await UniTask.Yield(token, true);
                    if (Settings.HasLifeTime &&  Time.time - State.CreationTime > Settings.LifeTime)
                    {
                        Die().Forget();
                        break;
                    }
                    if (!State.Enabled) continue;
                    await Update(token);
                }
            }
            catch (OperationCanceledException)
            {
            }
        }

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

        public virtual bool IsAlive() => !State.IsDead && State.Enabled && View;
    }
}