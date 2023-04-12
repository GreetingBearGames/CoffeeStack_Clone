using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    public CinemachineVirtualCamera[] cameras;
    public CinemachineVirtualCamera inGameCamera, finish1Cam, finish2Cam, finish3Cam, startCam;
    private CinemachineVirtualCamera currentCam;
    private static CameraManager _instance;
    public static CameraManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("Camera Manager is null");
            }
            return _instance;
        }
    }
    private void Awake()
    {
        _instance = this;
    }
    private void Start()
    {
        currentCam = startCam;
        for (int i = 0; i < cameras.Length; i++)
        {
            if (cameras[i] == currentCam)
            {
                cameras[i].Priority = 20;
            }
            else
            {
                cameras[i].Priority = 10;
            }
        }
    }
    public void SwitchCamera(CinemachineVirtualCamera newCam)
    {
        currentCam = newCam;
        currentCam.Priority = 20;
        for (int i = 0; i < cameras.Length; i++)
        {
            if (cameras[i] != currentCam)
            {
                cameras[i].Priority = 10;
            }
        }
    }
}
