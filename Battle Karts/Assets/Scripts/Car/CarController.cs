using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Unity.Netcode;

public class CarController : NetworkBehaviour
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

    [SerializeField] private CinemachineVirtualCamera playerCamera;
    [SerializeField] private AudioListener playerAudioListener;

    private void Awake()
    {
        inputController = FindObjectOfType<InputController>();
        wheels = GetComponentsInChildren<Wheel>();
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.centerOfMass = centerOfMass.localPosition;
    }

    public override void OnNetworkSpawn()
    {
        if (!IsOwner)
        {
            playerAudioListener.enabled = false;
            playerCamera.Priority = 0;
            return;
        }

        playerAudioListener.enabled = true;
        playerCamera.Priority = 100;
    }

    void FixedUpdate()
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
