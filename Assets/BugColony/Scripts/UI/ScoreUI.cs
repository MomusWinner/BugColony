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
            if (_disposables == null)
            {
                Debug.Log("Is null");
            }
            _disposables.Add(_scoreManager.TotalDeadWorkerBug.Subscribe(value => {
                _totalDeadWorkerBug.text = value.ToString();
            }));
            _disposables.Add(_scoreManager.TotalDeadPredatorBug.Subscribe(value =>
            {
                _totalDeadPredatorBug.text = value.ToString();
            }));
        }

        private void OnDestroy()
        {
            _disposables.Dispose();
        }
    }
}