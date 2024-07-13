using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Unity.Netcode;

public struct InputPayload : INetworkSerializable
{
    public int tick;
    public Vector3 inputVector;

    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue(ref tick);
        serializer.SerializeValue(ref inputVector);
    }
}

public struct StatePayload : INetworkSerializable
{
    public int tick;
    public Vector3 position;
    public Quaternion rotation;
    public Vector3 velocity;
    public Vector3 angularVelocity;

    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue(ref tick);
        serializer.SerializeValue(ref position);
        serializer.SerializeValue(ref rotation);
        serializer.SerializeValue(ref velocity);
        serializer.SerializeValue(ref angularVelocity);
    }
}

public class CarController : NetworkBehaviour
{
    //* Settings
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

    //* Netcode general
    NetworkTimer timer;
    const float k_serverTickRate = 60f;
    const int k_bufferSize = 1024;

    //* Netcode client specific
    CircularBuffer<StatePayload> clientStateBuffer;
    CircularBuffer<InputPayload> clientInputBuffer;
    StatePayload lastServerState;
    StatePayload lastProcessedState;

    //* Netcode server specific
    CircularBuffer<StatePayload> serverStateBuffer;
    Queue<InputPayload> serverInputQueue;

    private void Awake()
    {
        inputController = FindObjectOfType<InputController>();
        wheels = GetComponentsInChildren<Wheel>();
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.centerOfMass = centerOfMass.localPosition;

        //* Network
        timer = new NetworkTimer(k_serverTickRate);
        clientInputBuffer = new CircularBuffer<InputPayload>(k_bufferSize);
        clientStateBuffer = new CircularBuffer<StatePayload>(k_bufferSize);

        serverStateBuffer = new CircularBuffer<StatePayload>(k_bufferSize);
        serverInputQueue = new Queue<InputPayload>();
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

    void Update()
    {
        timer.Update(Time.deltaTime);
    }

    void FixedUpdate()
    {
        if (!IsOwner)
        {
            return;
        }

        while (timer.ShouldTick())
        {
            HandleClientTick();
            //HandleServerTick();
        }
    }

    #region Network
    void HandleClientTick()
    {
        if (!IsClient)
        {
            return;
        }

        var currentTick = timer.currentTick;
        var bufferIndex = currentTick % k_bufferSize;

        InputPayload inputPayload= new InputPayload() {
            tick = currentTick,
            // inputVector = (Vector3)input.Move
        };

        clientInputBuffer.Add(inputPayload, bufferIndex);
        SendToServerRpc(inputPayload);

        StatePayload statePayload = ProcessMovement(inputPayload);
        clientStateBuffer.Add(statePayload, bufferIndex);

        //* HandleServerReconciliation();
    }

    [ServerRpc]
    void SendToServerRpc(InputPayload input)
    {
        serverInputQueue.Enqueue(input);
    }

    StatePayload ProcessMovement(InputPayload input)
    {
        Move();

        return new StatePayload()
        {
            tick = input.tick,
            position = transform.position,
            rotation = transform.rotation,
            velocity = _rigidbody.velocity,
            angularVelocity = _rigidbody.angularVelocity,
        };
    }

    #endregion

    void Move()
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
