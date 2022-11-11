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

        var position = transform.position;
        transform.position = new Vector3
        {
            x = position.x + velocityInCameraSpace.x,
            y = position.y,
            z = position.z + velocityInCameraSpace.z
        };

        transform.LookAt(lockOnCoordinator.LockedOnPosition);
    }
}