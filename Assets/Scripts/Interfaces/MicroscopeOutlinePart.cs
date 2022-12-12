
// Copyright (c) 2022 MauricioRB06 <https://github.com/MauricioRB06>
// Supervisory Teacher: herrera78 <https://github.com/herrera78>
//
// The Purpose Of This Script Is:
//
// Sets the behavior of the Outline on the microscope parts.
//
// Last Update: 11.12.2022 By MauricioRB06

using Biology.Microscope;
using Managers;
using UnityEngine;

namespace Interfaces
{
    
    // Components required for this script work.
    [RequireComponent(typeof(Outline))]
    [RequireComponent(typeof(RotatingPart))]
    public class MicroscopeOutlinePart: MonoBehaviour
    {
        [Header("Outline Camera Settings")][Space(5)]
        [Tooltip("Set here the camera on which the Outline will be activated.")]
        [SerializeField] [Range(1,8)] private int cameraToOutline = 1;
        
        // They are used to store the reference to the required components.
        private Outline _outline;
        private RotatingPart _rotatingPart;
        
        // It is used to know if the part is blocked or not.
        private bool _partIsLock;
        
        public void Awake()
        {
            _outline = GetComponent<Outline>();
            _rotatingPart = GetComponent<RotatingPart>();
            _outline.enabled = false;
            _rotatingPart.RotationLock(false);
        }

        private void OnEnable() { MicroscopeCm.MicroscopeCameraDelegate += OutlineChange; }
        private void OnDisable() { MicroscopeCm.MicroscopeCameraDelegate -= OutlineChange; }
        
        private void OutlineChange(int activeCamera)
        {
            if (_partIsLock) return;
            
            if (activeCamera == cameraToOutline && BiologyStepManager.instance.MicroscopeOn)
            {
                _outline.enabled = true;
                _outline.OutlineWidth = 10;
                _rotatingPart.RotationLock(true);
            }
            else
            {
                _outline.enabled = false;
                _outline.OutlineWidth = 0;
                _rotatingPart.RotationLock(false);
            }
        }
        
        public void LockPart()
        {
            _partIsLock = !_partIsLock;
            _outline.enabled = !_outline.enabled;
        }
        
    }
}
