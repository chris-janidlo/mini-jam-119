using UnityAtoms.BaseAtoms;
using UnityEngine;

public class LockOnCamera : MonoBehaviour
{
    [SerializeField] private Vector3 offset;

    [SerializeField] private LockOnCoordinator lockOnCoordinator;
    [SerializeField] private Vector3Variable playerPosition;

    private void Update()
    {
        var playerToTargetHeading = lockOnCoordinator.LockedOnPosition - playerPosition.Value;
        var rotation = Quaternion.LookRotation(playerToTargetHeading);

        var rotatedOffset = rotation * offset;

        transform.SetPositionAndRotation(playerPosition.Value + rotatedOffset, rotation);
    }
}