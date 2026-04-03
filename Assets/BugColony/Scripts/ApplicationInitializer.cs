using BugColony.Scripts.Settings;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace BugColony.Scripts
{
    public class ApplicationInitializer : IInitializable
    {
        [Inject] private GameSettings _gameSettings; 
        
        public void Initialize()
        {
            Application.targetFrameRate = _gameSettings.TargetFPS;
        }
    }
}