using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : MonoBehaviour
{
    [SerializeField] private bool steer;
    [SerializeField] private bool invertSteer;
    [SerializeField] private bool power;

    public float _steerAngle;
    public float _torque;

    private WheelCollider wheelCollider;
    private Transform wheelTransform;

    private void Start()
    {
        wheelCollider = GetComponentInChildren<WheelCollider>();
        wheelTransform = GetComponentInChildren<MeshRenderer>().GetComponent<Transform>();
    }

    private void Update()
    {
        wheelCollider.GetWorldPose(out Vector3 pos, out Quaternion rot);
        wheelTransform.position = pos;
        wheelTransform.rotation = rot;
    }

    private void FixedUpdate()
    {
        if (steer)
        {
            wheelCollider.steerAngle = _steerAngle * (invertSteer ? -1 : 1);
        }

        if (power)
        {
            wheelCollider.motorTorque = _torque;
        }
    }
}
