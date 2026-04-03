using R3;
using TMPro;
using UnityEngine;
using VContainer;

namespace BugColony.Scripts.UI
{
    public class ScoreUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text _totalDeadWorkerBug ;
        [SerializeField] private TMP_Text _totalDeadPredatorBug;
        
        [Inject] private ScoreManager _scoreManager;
        
        private readonly CompositeDisposable _disposables = new();

        private void Start()
        {
            _disposables.Add(_scoreManager.TotalDeadWorkerBug.Subscribe(value => {
                _totalDeadWorkerBug.text = $"Total Dead Worker Bug {value.ToString()}";
            }));
            _disposables.Add(_scoreManager.TotalDeadPredatorBug.Subscribe(value =>
            {
                _totalDeadPredatorBug.text = $"Total Dead Predator Bug {value.ToString()}";
            }));
        }

        private void OnDestroy()
        {
            _disposables?.Dispose();
        }
    }
}