using BugColony.Scripts.Foods;
using UnityEngine;

namespace BugColony.Scripts.Settings.Foods
{
    [CreateAssetMenu(fileName = "Food Settings", menuName = "Settings/Foods/Food", order = 0)]
    public class FoodSettings : ScriptableObject
    {
        [SerializeField] private FoodView _prefab;
        public FoodView Prefab => _prefab;
        
        [SerializeField] private int _nutritionalValue;
        public int NutritionalValue => _nutritionalValue;
    }
}
