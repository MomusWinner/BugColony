using System;
using BugColony.Scripts.Bugs;
using UnityEngine;

namespace BugColony.Scripts.Foods
{
    public class Food : MonoBehaviour, ITarget
    {
        public event Action<Food> Destroyed;
        
        public int NutritionalValue { get; set; }

        public ResourceType ResourceType => ResourceType.Food;

        private bool _isDestroyed;

        private void OnDestroy()
        {
            _isDestroyed = true;
        }

        public int Eat()
        {
            Destroyed?.Invoke(this);
            Destroy(gameObject);
            return NutritionalValue;
        }

        public Vector3 GetPosition() => transform.position;
        public bool IsAlive() => !_isDestroyed;
    }
}