using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraFollow : MonoBehaviour
{
    public Camera mainCamera;
    public RenderTexture ps1RenderTexture;

    void Start()
    {
        // Ensure the camera setup on start
        SetupCamera();

        // Subscribe to the sceneLoaded event
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        // Unsubscribe from the sceneLoaded event
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Ensure the camera setup after loading a new scene
        SetupCamera();
    }

    private void SetupCamera()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }

        if (mainCamera != null && ps1RenderTexture != null)
        {
            mainCamera.targetTexture = ps1RenderTexture;
        }
    }
}
