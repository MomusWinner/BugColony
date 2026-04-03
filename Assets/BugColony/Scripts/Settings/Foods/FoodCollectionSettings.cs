using System.Collections.Generic;
using UnityEngine;

namespace BugColony.Scripts.Settings.Foods
{
    [CreateAssetMenu(fileName = "Food Collection", menuName = "Settings/Foods/Collection", order = 0)]
    public class FoodCollectionSettings : ScriptableObject
    {
        [SerializeField] private List<FoodSettings> _foods;
        public List<FoodSettings> Foods => _foods;
    }
}