using UnityAtoms.BaseAtoms;
using UnityEngine;

public class PositionSync : MonoBehaviour
{
    public Vector3Variable positionVariable;

    private void Update()
    {
        positionVariable.Value = transform.position;
    }
}