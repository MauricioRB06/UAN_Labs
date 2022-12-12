
// Copyright (c) 2022 MauricioRB06 <https://github.com/MauricioRB06>
// Supervisory Teacher: herrera78 <https://github.com/herrera78>
//
// The Purpose Of This Script Is:
//
// Establish the behavior of the parts that can rotate.
//
// Last Update: 11.12.2022 By MauricioRB06

using Interfaces;
using Managers;
using UnityEngine;

namespace Biology.Microscope
{
    public class RotatingPart : MonoBehaviour
    {
        
        [Header("Rotation Settings")] [Space(5)]
        [Tooltip("The speed at which the part rotates.")]
        [SerializeField] [Range(1f,20f)] private float rotationSpeed = 8f;
        [Space(15)]
        
        [Tooltip("Enables or Disables rotation on the X-Axis.")]
        [SerializeField] private bool rotateX = true;
        [Tooltip("Sets the minor angle of rotation on the X-Axis.")]
        [SerializeField] [Range(-359f,359f)] private int minDegreesRotationX = 1;
        [Tooltip("Sets the major angle of rotation on the X-axis.")]
        [SerializeField] [Range(-359f,359f)] private int maxDegreesRotationX = 359;
        [Space(15)]
        
        [Tooltip("Enables or Disables rotation on the Y-Axis.")]
        [SerializeField] private bool rotateY;
        [Tooltip("Sets the minor angle of rotation on the Y-Axis.")]
        [SerializeField] [Range(-359f,359f)] private int minDegreesRotationY = 1;
        [Tooltip("Sets the major angle of rotation on the Y-axis.")]
        [SerializeField] [Range(-359f,359f)] private int maxDegreesRotationY = 359;
        [Space(15)]
        
        [Tooltip("Enables or Disables rotation on the X-Axis.")]
        [SerializeField] private bool rotateZ;
        [Tooltip("Sets the minor angle of rotation on the Z-Axis.")]
        [SerializeField] [Range(-359f,359f)] private int minDegreesRotationZ = 1;
        [Tooltip("Sets the major angle of rotation on the Z-axis.")]
        [SerializeField] [Range(-359f,359f)] private int maxDegreesRotationZ = 359;
        [Space(15)]
        
        [Header("Interaction Settings")] [Space(5)]
        [Tooltip("Place here the object to be interacted with.")]
        [SerializeField] private GameObject objectToInteract;
        [Tooltip("Place here the UI to interact with.")]
        [SerializeField] private GameObject uiToInteract;
        [Space(15)]
        
        [Header("Interaction UI Settings")] [Space(5)]
        [Tooltip("Place here the parameter that will modify in the sample images the rotation of the object in the X" +
                 " axis ( 0.Movement X-Axis / 1. Movement Y-Axis / 2. Light / 3. BlurA / 4. Blur B)")]
        [SerializeField] [Range(0,4)] private int xAxisValue;
        [Tooltip("Place here the parameter that will modify in the sample images the rotation of the object in the Y" +
                 " axis ( 0.Movement X-Axis / 1. Movement Y-Axis / 2. Light / 3. BlurA / 4. Blur B)")]
        [SerializeField] [Range(0,4)] private int yAxisValue = 1;
        [Tooltip("Place here the parameter that will modify in the sample images the rotation of the object in the Z" +
                 " axis ( 0.Movement X-Axis / 1. Movement Y-Axis / 2. Light / 3. BlurA / 4. Blur B)")]
        [SerializeField] [Range(0,4)] private int zAxisValue = 1;
        
        // They are used to store the value of the rotation in order to send it to the moving parts.
        private float _angleX;
        private float _angleY;
        private float _angleZ;
        
        // It is used to allow or disallow rotation.
        private bool _canRotate;

        public void RotationLock(bool canRotate) { _canRotate = canRotate; }
        
        // It is used to rotate the parts when they detect the mouse position.
        private void OnMouseDrag()
        {
            if (!_canRotate) return;
            if(!BiologyStepManager.instance.MicroscopeOn) return;
            
            var rotX = Input.GetAxis("Mouse X") * rotationSpeed * Mathf.Deg2Rad;
            var rotY = Input.GetAxis("Mouse Y") * rotationSpeed * Mathf.Deg2Rad;
            var rotZ = Input.GetAxis("Mouse Y") * rotationSpeed * Mathf.Deg2Rad;

            if (rotateX)
            {
                _angleX += rotX;
                
                if (_angleX < minDegreesRotationX)
                {
                    _angleX = minDegreesRotationX;
                }
                else if (_angleX > maxDegreesRotationX)
                {
                    _angleX = maxDegreesRotationX;
                }

                if (_angleX > minDegreesRotationX && _angleX < maxDegreesRotationX)
                {
                    transform.Rotate(Vector3.up, -rotX);

                    if (objectToInteract != null && objectToInteract.GetComponent<IInteractablePart>() != null)
                    {
                        objectToInteract.GetComponent<IInteractablePart>().InteractivePart(_angleX);
                    }
                    
                    if (uiToInteract != null && uiToInteract.GetComponent<IInteractableUI>() != null)
                    {
                        uiToInteract.GetComponent<IInteractableUI>().InteractUI(xAxisValue, _angleX);
                    }
                }
            }
            else if (rotateY)
            {
                _angleY += rotY;
                
                if (_angleY < minDegreesRotationY)
                {
                    _angleY = minDegreesRotationY;
                }
                else if (_angleY > maxDegreesRotationY)
                {
                    _angleY = maxDegreesRotationY;
                }

                if (_angleY > minDegreesRotationY && _angleY < maxDegreesRotationY)
                {
                    transform.Rotate(Vector3.right, rotY);
                    
                    if (objectToInteract != null && objectToInteract.GetComponent<IInteractablePart>() != null)
                    {
                        objectToInteract.GetComponent<IInteractablePart>().InteractivePart(_angleY);
                    }
                    
                    if (uiToInteract != null && uiToInteract.GetComponent<IInteractableUI>() != null)
                    {
                        uiToInteract.GetComponent<IInteractableUI>().InteractUI(yAxisValue, _angleY);
                    }
                }
            }
            else if (rotateZ)
            {
                _angleZ += rotZ;
                
                if (_angleZ < minDegreesRotationZ)
                {
                    _angleZ = minDegreesRotationZ;
                }
                else if (_angleZ > maxDegreesRotationZ)
                {
                    _angleZ = maxDegreesRotationZ;
                }

                if (_angleZ > minDegreesRotationZ && _angleZ < maxDegreesRotationZ)
                {
                    transform.Rotate(Vector3.forward, rotZ);
                    
                    if (objectToInteract != null && objectToInteract.GetComponent<IInteractablePart>() != null)
                    {
                        objectToInteract.GetComponent<IInteractablePart>().InteractivePart(_angleZ);
                    }
                    
                    if (uiToInteract != null && uiToInteract.GetComponent<IInteractableUI>() != null)
                    {
                        uiToInteract.GetComponent<IInteractableUI>().InteractUI(zAxisValue, _angleZ);
                    }
                }
            }
        }
        
    }
}
