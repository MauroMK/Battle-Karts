using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    //TODO change to new input system

    private string inputSteerAxis = "Horizontal";
    private string inputAcceleratorAxis = "Vertical";

    [HideInInspector] public float steerInput;
    [HideInInspector] public float acceleratorInput;

    void Start()
    {
        
    }

    void Update()
    {
        steerInput = Input.GetAxis(inputSteerAxis);    
        acceleratorInput = Input.GetAxis(inputAcceleratorAxis);    
    }
}
