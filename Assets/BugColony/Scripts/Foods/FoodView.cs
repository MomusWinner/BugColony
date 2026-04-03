using System;
using BugColony.Scripts.Bugs;
using UnityEngine;
using UnityEngine.Pool;

namespace BugColony.Scripts.Foods
{
    public class FoodView : MonoBehaviour, ITarget
    {
        public event Action<FoodView> Destroyed;
        public ResourceType ResourceType => ResourceType.Food;

        private IObjectPool<FoodView> _pool;
        private int _nutritionalValue;
        private bool _isActive;

        public void Initialize(IObjectPool<FoodView> pool, int nutritionalValue)
        {
            _isActive = true;
            _pool = pool;
            _nutritionalValue = nutritionalValue;
        }

        public int Eat()
        {
            Destroyed?.Invoke(this);
            ReturnToPool();
            return _nutritionalValue;
        }

        public Vector3 GetPosition() => transform.position;
        public bool IsAlive() => _isActive;
        
        private void OnDestroy()
        {
            _isActive = false;
        }
        
        private void ReturnToPool()
        {
            _isActive = false;
            _pool?.Release(this);
        } 
    }
}