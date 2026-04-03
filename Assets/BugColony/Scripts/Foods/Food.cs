using System;
using BugColony.Scripts.Bugs;
using UnityEngine;

namespace BugColony.Scripts.Foods
{
    public class Food : MonoBehaviour, IEatable, ITarget
    {
        public event Action<Food> Destroyed;
        
        public int NutritionalValue { get; set; }

        public ResourceType ResourceType => ResourceType.Food;

        public int Eat()
        {
            Destroyed?.Invoke(this);
            Destroy(gameObject);
            return NutritionalValue;
        }

        public Transform GetTarget() => transform;
    }
}