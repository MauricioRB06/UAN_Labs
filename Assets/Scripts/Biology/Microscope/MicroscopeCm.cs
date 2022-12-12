
// Copyright (c) 2022 MauricioRB06 <https://github.com/MauricioRB06>
// Supervisory Teacher: herrera78 <https://github.com/herrera78>
//
// The Purpose Of This Script Is:
//
// Control the behavior of the cameras to manipulate them with the user interface and let the microscope parts
// know when they should be activated or deactivated.
//
// Documentation and References:
//
// C# Delegates: https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/delegates/
//
// Last Update: 11.12.2022 By MauricioRB06

using System;
using UnityEngine;

namespace Biology.Microscope
{
    public class MicroscopeCm : MonoBehaviour
    {
        [Header("Camera Controller")][Space(5)]
        [Tooltip("Enter the main camera driver here.")]
        [SerializeField] private GameObject cameraAnimator;
        
        // Variable to be used to reference the camera control system in Cinemachine to perform camera changes.
        private Animator _cinemachineCam;
        private int _cameraToSwitch = 1;
        
        // Delegate to notify about the change of camera.
        public static event Action<int> MicroscopeCameraDelegate;
        
        private void Awake() { _cinemachineCam = cameraAnimator.GetComponent<Animator>(); }
        
        public void SwitchCamera(int cameraToSwitch)
        {
            switch (cameraToSwitch)
            {
                case 1:
                {
                    _cameraToSwitch++;
                    if(_cameraToSwitch > 8) _cameraToSwitch = 1;
                    break;
                }
                case 0:
                {
                    _cameraToSwitch--;
                    if(_cameraToSwitch < 1) _cameraToSwitch = 8;
                    break;
                }
            }

            switch (_cameraToSwitch)
            {
                case 1:
                    _cinemachineCam.Play("G1 Camera 1");
                    MicroscopeCameraDelegate?.Invoke(1);
                    break;
                case 2:
                    _cinemachineCam.Play("G1 Camera 2");
                    MicroscopeCameraDelegate?.Invoke(2);
                    break;
                case 3:
                    _cinemachineCam.Play("G1 Camera 3");
                    MicroscopeCameraDelegate?.Invoke(3);
                    break;
                case 4:
                    _cinemachineCam.Play("G1 Camera 4");
                    MicroscopeCameraDelegate?.Invoke(4);
                    break;
                case 5:
                    _cinemachineCam.Play("G1 Camera 5");
                    MicroscopeCameraDelegate?.Invoke(5);
                    break;
                case 6:
                    _cinemachineCam.Play("G1 Camera 6");
                    MicroscopeCameraDelegate?.Invoke(6);
                    break;
                case 7:
                    _cinemachineCam.Play("G1 Camera 7");
                    MicroscopeCameraDelegate?.Invoke(7);
                    break;
                case 8:
                    _cinemachineCam.Play("G1 Camera 8");
                    MicroscopeCameraDelegate?.Invoke(8);
                    break;
            }
        }
        
    }
}
