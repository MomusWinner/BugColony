using BugColony.Scripts.Settings;
using DG.Tweening;
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
            DOTween.SetTweensCapacity(1_500, 1_500/4);
        }
    }
}