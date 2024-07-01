using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    // Settings
    [SerializeField] private Transform centerOfMass;
    [SerializeField] private float motorForce;
    [SerializeField] private float maxSteerAngle;

    [HideInInspector] public float steer;
    [HideInInspector] public float acceleration;

    private Rigidbody _rigidbody;
    private InputController inputController;
    private Wheel[] wheels;

    void Start()
    {
        inputController = FindObjectOfType<InputController>();
        wheels = GetComponentsInChildren<Wheel>();
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.centerOfMass = centerOfMass.localPosition;
    }

    void Update()
    {
        steer = inputController.steerInput;
        acceleration = inputController.acceleratorInput;

        foreach (var wheel in wheels)
        {
            wheel._steerAngle = steer * maxSteerAngle;
            wheel._torque = acceleration * motorForce;
        }    
    }
}
