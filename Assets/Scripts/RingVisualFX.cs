using System.Collections;
using System.Collections.Generic;
using crass;
using UnityEngine;

public class RingVisualFX : MonoBehaviour
{
    [SerializeField] private Vector2 angularAccelerationRange, angularSpeedClamp;
    [SerializeField] private BagRandomizer<float> scaleValues;
    [SerializeField] private Vector2 scaleChangeDelayRange;
    [SerializeField] private List<Transform> rings;

    private List<Vector3> _angularVelocities;

    private void Start()
    {
        _angularVelocities = new List<Vector3>(rings.Count);

        for (var i = 0; i < rings.Count; i++)
        {
            StartCoroutine(ScaleRoutine(rings[i]));
            _angularVelocities.Add(Vector3.zero);
        }
    }

    private void Update()
    {
        for (var i = 0; i < rings.Count; i++)
        {
            var velocity = _angularVelocities[i];

            velocity = new Vector3
            (
                Accelerate(velocity.x),
                Accelerate(velocity.y),
                Accelerate(velocity.z)
            );

            rings[i].rotation *= Quaternion.Euler(velocity * Time.deltaTime);
            _angularVelocities[i] = velocity;
        }
    }

    private IEnumerator ScaleRoutine(Transform ring)
    {
        while (true)
        {
            ring.localScale = Vector3.one * scaleValues.GetNext();
            yield return new WaitForSeconds(RandomExtra.Range(scaleChangeDelayRange));
        }
        // ReSharper disable once IteratorNeverReturns
    }

    private float Accelerate(float speed)
    {
        var acceleration = RandomExtra.Range(angularAccelerationRange);
        var newSpeed = speed + acceleration * Time.deltaTime;
        return Mathf.Clamp(newSpeed, angularSpeedClamp.x, angularSpeedClamp.y);
    }
}