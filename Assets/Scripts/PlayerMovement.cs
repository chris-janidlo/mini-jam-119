using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;

    [SerializeField] private Transform cameraTransform;
    [SerializeField] private LockOnCoordinator lockOnCoordinator;

    private void Update()
    {
        var velocity = new Vector3
        {
            x = Input.GetAxis("Horizontal"),
            z = Input.GetAxis("Vertical")
        }.normalized * (speed * Time.deltaTime);

        var velocityInCameraSpace = cameraTransform.TransformDirection(velocity);

        transform.position += velocityInCameraSpace;

        transform.LookAt(lockOnCoordinator.LockedOnPosition);
    }
}