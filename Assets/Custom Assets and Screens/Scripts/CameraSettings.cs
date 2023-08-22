using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSettings : MonoBehaviour
{
    public float stageCamFarClipPlane = 100;
    public Vector3 stageCamInitialDistance = new Vector3(0, 9, -53);
    public Vector3 stageCamInitialRotation = new Vector3(7, 0, 0);
    public bool stageCamEnableLookAt = true;
    public float stageCamFieldOfView = 12;
    public bool stageCamEnableZoom = true;
    public float stageCamMinimumZoom = 40;
    public float stageCamMaximumZoom = 60;
    public float stageCamMaximumPlayerDistance = 18;

    private void Awake()
    {
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().farClipPlane = stageCamFarClipPlane;
        UFE.config.cameraOptions.initialDistance = stageCamInitialDistance;
        UFE.config.cameraOptions.initialRotation = stageCamInitialRotation;
        UFE.config.cameraOptions.enableLookAt = stageCamEnableLookAt;
        UFE.config.cameraOptions.initialFieldOfView = stageCamFieldOfView;
        UFE.config.cameraOptions.minZoom = stageCamMinimumZoom;
        UFE.config.cameraOptions.maxZoom = stageCamMaximumZoom;
        UFE.config.cameraOptions.enableZoom = stageCamEnableZoom;
        UFE.config.cameraOptions._maxDistance = stageCamMaximumPlayerDistance;
    }

}
