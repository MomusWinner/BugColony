using UnityEngine;

namespace BugColony.Scripts.Bugs
{
    public interface ITarget : IEatable
    {
        Vector3 GetPosition();
        bool IsAlive();
    }
}