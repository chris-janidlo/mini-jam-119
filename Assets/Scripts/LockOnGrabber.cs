using UnityEngine;

public class LockOnGrabber : MonoBehaviour
{
    [SerializeField] private LockOnCoordinator lockOnCoordinator;

    private void Start()
    {
        lockOnCoordinator.LockOn(transform);
    }
}