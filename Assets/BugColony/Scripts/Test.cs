using System;
using BugColony.Scripts.Bugs;
using BugColony.Scripts.Settings.Bugs;
using R3;
using UnityEngine;
using VContainer;

namespace BugColony.Scripts
{
    public class Test : MonoBehaviour
    {
        [Inject] private BugFactory _factory;
        [Inject] private BugCollectionSettings _bugSettings;
        
        public void Start()
        {
            var test = GameObject.CreatePrimitive(PrimitiveType.Cube);
            Observable.Interval(TimeSpan.FromSeconds(1)).Subscribe( _ =>
            {
                var test = GameObject.CreatePrimitive(PrimitiveType.Cube);
            });
        }
    }
}