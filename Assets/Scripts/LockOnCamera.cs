using UnityAtoms.BaseAtoms;
using UnityEngine;

public class LockOnCamera : MonoBehaviour
{
    [SerializeField] private AnimationCurve
        followDelayByHorizontalPercentageFromCenterOfScreen,
        followDelayByVerticalPercentageFromCenterOfScreen;

    [SerializeField] private Vector3 offset;

    [SerializeField] private LockOnCoordinator lockOnCoordinator;
    [SerializeField] private Vector3Variable playerPosition;
    [SerializeField] private new Camera camera;

    private Vector3 _currentFollowSpeed;

    private void Update()
    {
        var playerToTargetHeading = lockOnCoordinator.LockedOnPosition - playerPosition.Value;
        var offsetRotation = Quaternion.LookRotation(playerToTargetHeading);

        var rotatedOffset = offsetRotation * offset;
        var position = playerPosition.Value + rotatedOffset;

        var delayedPosition = DelayPosition(position);

        var positionToTargetHeading = lockOnCoordinator.LockedOnPosition - delayedPosition;
        var lookRotation = Quaternion.LookRotation(positionToTargetHeading);

        transform.SetPositionAndRotation(delayedPosition, lookRotation);
    }

    private Vector3 DelayPosition(Vector3 desiredPosition)
    {
        var screenPos = camera.WorldToScreenPoint(playerPosition.Value);
        var screenCenter = new Vector2
        {
            x = camera.pixelWidth / 2f,
            y = camera.pixelHeight / 2f
        };
        var screenPercentages = new Vector2
        {
            x = Mathf.Abs(screenPos.x - screenCenter.x) / screenCenter.x,
            y = Mathf.Abs(screenPos.y - screenCenter.y) / screenCenter.y
        };

        var horizontalFollowDelay = followDelayByHorizontalPercentageFromCenterOfScreen.Evaluate(screenPercentages.x);
        var verticalFollowDelay = followDelayByVerticalPercentageFromCenterOfScreen.Evaluate(screenPercentages.y);
        var totalFollowDelay = horizontalFollowDelay * verticalFollowDelay;

        return Vector3.SmoothDamp(transform.position, desiredPosition, ref _currentFollowSpeed, totalFollowDelay);
    }
}