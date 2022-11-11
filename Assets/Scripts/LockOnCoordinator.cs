using UnityEngine;

[CreateAssetMenu(menuName = "Mini Jam 119/Lock On Coordinator", fileName = "newLockOnCoordinator.asset")]
public class LockOnCoordinator : ScriptableObject
{
    private Transform _target;

    public Vector3 LockedOnPosition => _target.position;

    public void LockOn(Transform target)
    {
        _target = target;
    }
}