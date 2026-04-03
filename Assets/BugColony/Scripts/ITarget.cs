using UnityEngine;

namespace BugColony.Scripts
{
    public interface ITarget : IEatable
    {
        Vector3 GetPosition();
        bool IsAlive();
    }
}