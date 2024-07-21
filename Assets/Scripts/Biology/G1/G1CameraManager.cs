
// Copyright (c) 2022 MauricioRB06 <https://github.com/MauricioRB06>
// Supervisory Teacher: herrera78 <https://github.com/herrera78>
//
// The Purpose Of This Script Is:
//
// Sets the behavior of the camera manager.
//
// Last Update: 11.12.2022 By MauricioRB06

using UnityEngine;

namespace Biology.G1
{
    public class G1CameraManager : MonoBehaviour
    {
        // Singleton Pattern Instance.
        public static G1CameraManager Instance;

        [Header("Camera Controller")]
        [SerializeField] private GameObject cameraAnimator;
        
        // Variable to be used to reference the camera control system in Cinemachine to perform camera changes.
        private Animator _cinemachineCam;

        public G1CameraManager()
        {
            // Singleton definition.
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        
        private void Awake() { _cinemachineCam = cameraAnimator.GetComponent<Animator>(); }
        
        public void SwitchCamera(bool cameraToSwitch)
        {
            switch (cameraToSwitch)
            {
                case true:
                    _cinemachineCam.Play("G1 Camera 1");
                    break;
                case false:
                    _cinemachineCam.Play("G1 Camera 2");
                    break;
            }
        }
        
    }
}
