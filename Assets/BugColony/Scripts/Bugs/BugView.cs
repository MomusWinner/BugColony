using System;
using BugColony.Scripts.Foods;
using UnityEngine;

namespace BugColony.Scripts.Bugs
{
    public class BugView : MonoBehaviour, IMovable, ITarget
    {
        public event Action<BugView> Destroyed;
        public event Action<IEatable> OnCollided;
        
        public Vector3 Position { get => transform.position; set => transform.position = value; }
        public Bug Bug { get; set; }
        
        private bool _isDestroyed;

        public Transform GetTarget() => transform;
        
        public void OnDestroy()
        {
            _isDestroyed = true;
            Destroyed?.Invoke(this);
        }
        
        public void Destroy()
        {
            if (_isDestroyed) return;
            Destroy(gameObject);
        }

        private void OnCollisionEnter(Collision other)
        {
            IEatable eatable = other.gameObject.GetComponent<Food>();
            if (eatable == null)
            {
                var view = other.gameObject.GetComponent<BugView>();
                if (view) eatable = view.Bug;
            }
            
            if (eatable != null)
                OnCollided?.Invoke(eatable);
        }
    }
}