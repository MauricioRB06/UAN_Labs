
// Copyright (c) 2022 MauricioRB06 <https://github.com/MauricioRB06>
// Supervisory Teacher: herrera78 <https://github.com/herrera78>
//
// The Purpose Of This Script Is:
//
// Establish the behavior of the parts that can rotate.
//
// Last Update: 11.12.2022 By MauricioRB06

using System.Collections;
using System.Collections.Generic;
using Biology.G1;
using Interfaces;
using Managers;
using UniRx;
using UnityEngine;

namespace Biology.Microscope
{
    public class RotatingPart : MonoBehaviour
    {
        
        [Header("Rotation Settings")] [Space(5)]
        [Tooltip("The speed at which the part rotates.")]
        [SerializeField] [Range(1f,20f)] protected float rotationSpeed = 8f;
        [Space(15)]
        
        [Tooltip("Enables or Disables rotation on the X-Axis.")]
        [SerializeField] protected bool rotateX = true;
        [Tooltip("Sets the minor angle of rotation on the X-Axis.")]
        [SerializeField] [Range(-359f,359f)] protected int minDegreesRotationX = 1;
        [Tooltip("Sets the major angle of rotation on the X-axis.")]
        [SerializeField] [Range(-359f,359f)] protected int maxDegreesRotationX = 359;
        [Space(15)]
        
        [Tooltip("Enables or Disables rotation on the Y-Axis.")]
        [SerializeField] protected bool rotateY;
        [Tooltip("Sets the minor angle of rotation on the Y-Axis.")]
        [SerializeField] [Range(-359f,359f)] protected int minDegreesRotationY = 1;
        [Tooltip("Sets the major angle of rotation on the Y-axis.")]
        [SerializeField] [Range(-359f,359f)] protected int maxDegreesRotationY = 359;
        [Space(15)]
        
        [Tooltip("Enables or Disables rotation on the X-Axis.")]
        [SerializeField] protected bool rotateZ;
        [Tooltip("Sets the minor angle of rotation on the Z-Axis.")]
        [SerializeField] [Range(-359f,359f)] protected int minDegreesRotationZ = 1;
        [Tooltip("Sets the major angle of rotation on the Z-axis.")]
        [SerializeField] [Range(-359f,359f)] protected int maxDegreesRotationZ = 359;
        [Space(15)]
        
        [Header("Interaction Settings")] [Space(5)]
        [Tooltip("Place here the object to be interacted with.")]
        [SerializeField] protected GameObject objectToInteract;
        [Tooltip("Place here the UI to interact with.")]
        [SerializeField] protected GameObject uiToInteract;
        [Tooltip(".")]
        [SerializeField] protected bool uiIsUpdatable;
        [Tooltip(".")]
        [SerializeField] protected bool partIsUpdatable;
        [Space(15)]
        
        [Header("Interaction UI Settings")] [Space(5)]
        [Tooltip("Place here the parameter that will modify in the sample images the rotation of the object in the X" +
                 " axis ( 0.Movement X-Axis / 1. Movement Y-Axis / 2. Light / 3. BlurA / 4. Blur B)")]
        [SerializeField] [Range(0,4)] protected int xAxisValue;
        [Tooltip("Place here the parameter that will modify in the sample images the rotation of the object in the Y" +
                 " axis ( 0.Movement X-Axis / 1. Movement Y-Axis / 2. Light / 3. BlurA / 4. Blur B)")]
        [SerializeField] [Range(0,4)] protected int yAxisValue = 1;
        [Tooltip("Place here the parameter that will modify in the sample images the rotation of the object in the Z" +
                 " axis ( 0.Movement X-Axis / 1. Movement Y-Axis / 2. Light / 3. BlurA / 4. Blur B)")]
        [SerializeField] [Range(0,4)] protected int zAxisValue = 1;
        
        [Tooltip("Set the step at which the sample will be disable.")]
        [SerializeField] private List<int> stepsToRestorePart;
        [Tooltip("Set the step at which the sample will be disable.")]
        [SerializeField] private bool restorePart;
        
        // They are used to store the value of the rotation in order to send it to the moving parts.
        protected float AngleX;
        protected float AngleY;
        protected float AngleZ;
        
        // It is used to allow or disallow rotation.
        protected bool CanRotate;

        public void RotationLock(bool canRotate) { CanRotate = canRotate; }
        
        // 
        private void OnEnable()
        {
            if (uiIsUpdatable)G1Sample.SampleUIUpdate += UpdateUIToInteract;
            if (partIsUpdatable)G1Sample.SampleUIUpdate += UpdatePartToInteract;
        }

        private void OnDisable()
        {
            if (uiIsUpdatable) G1Sample.SampleUIUpdate -= UpdateUIToInteract;
            if (partIsUpdatable)G1Sample.SampleUIUpdate -= UpdatePartToInteract;
        }
        
        private void Awake()
        {
            BiologyStepManager.Instance.Counter
                .Where(stepTrigger => stepsToRestorePart.Contains(stepTrigger))
                .Subscribe(_ => { StartCoroutine(RestorePart()); });
        }

        // It is used to rotate the parts when they detect the mouse position.
        private void OnMouseDrag()
        {
            if (!CanRotate) return;
            if(!BiologyStepManager.Instance.MicroscopeOn) return;
            
            var rotX = Input.GetAxis("Mouse X") * rotationSpeed * Mathf.Deg2Rad;
            var rotY = Input.GetAxis("Mouse Y") * rotationSpeed * Mathf.Deg2Rad;
            var rotZ = Input.GetAxis("Mouse Y") * rotationSpeed * Mathf.Deg2Rad;

            if (rotateX)
            {
                AngleX += rotX;
                
                if (AngleX < minDegreesRotationX)
                {
                    AngleX = minDegreesRotationX;
                }
                else if (AngleX > maxDegreesRotationX)
                {
                    AngleX = maxDegreesRotationX;
                }

                if (AngleX > minDegreesRotationX && AngleX < maxDegreesRotationX)
                {
                    transform.Rotate(Vector3.up, -rotX);

                    if (objectToInteract != null && objectToInteract.GetComponent<IInteractablePart>() != null)
                    {
                        objectToInteract.GetComponent<IInteractablePart>().InteractivePart(AngleX);
                    }
                    
                    if (uiToInteract != null && uiToInteract.GetComponent<IInteractableUI>() != null)
                    {
                        uiToInteract.GetComponent<IInteractableUI>().InteractUI(xAxisValue, AngleX);
                    }
                }
            }
            else if (rotateY)
            {
                AngleY += rotY;
                
                if (AngleY < minDegreesRotationY)
                {
                    AngleY = minDegreesRotationY;
                }
                else if (AngleY > maxDegreesRotationY)
                {
                    AngleY = maxDegreesRotationY;
                }

                if (AngleY > minDegreesRotationY && AngleY < maxDegreesRotationY)
                {
                    transform.Rotate(Vector3.right, rotY);
                    
                    if (objectToInteract != null && objectToInteract.GetComponent<IInteractablePart>() != null)
                    {
                        objectToInteract.GetComponent<IInteractablePart>().InteractivePart(AngleY);
                    }
                    
                    if (uiToInteract != null && uiToInteract.GetComponent<IInteractableUI>() != null)
                    {
                        uiToInteract.GetComponent<IInteractableUI>().InteractUI(yAxisValue, AngleY);
                    }
                }
            }
            else if (rotateZ)
            {
                AngleZ += rotZ;
                
                if (AngleZ < minDegreesRotationZ)
                {
                    AngleZ = minDegreesRotationZ;
                }
                else if (AngleZ > maxDegreesRotationZ)
                {
                    AngleZ = maxDegreesRotationZ;
                }

                if (AngleZ > minDegreesRotationZ && AngleZ < maxDegreesRotationZ)
                {
                    transform.Rotate(Vector3.forward, rotZ);
                    
                    if (objectToInteract != null && objectToInteract.GetComponent<IInteractablePart>() != null)
                    {
                        objectToInteract.GetComponent<IInteractablePart>().InteractivePart(AngleZ);
                    }
                    
                    if (uiToInteract != null && uiToInteract.GetComponent<IInteractableUI>() != null)
                    {
                        uiToInteract.GetComponent<IInteractableUI>().InteractUI(zAxisValue, AngleZ);
                    }
                }
            }
        }

        private void UpdateUIToInteract(GameObject newUI) => uiToInteract = newUI;
        private void UpdatePartToInteract(GameObject newPart) => objectToInteract = newPart;
        
        private IEnumerator RestorePart()
        {
            if (restorePart)
            {
                if (rotateX)
                {
                    for (var i = AngleX; i > 1; i -= 1)
                    {
                        transform.Rotate(Vector3.up, 1f);
                        AngleX -= 1f;
                    
                        if (objectToInteract != null && objectToInteract.GetComponent<IInteractablePart>() != null)
                        {
                            objectToInteract.GetComponent<IInteractablePart>().InteractivePart(AngleX);
                        }
                    
                        yield return new WaitForSeconds(0.001f);
                    }
                }
                else if (rotateY)
                {
                    for (var i = AngleY; i < 1; i -= 1)
                    {
                        transform.Rotate(Vector3.right, 1f);
                        AngleY -= 1f;
                    
                        if (objectToInteract != null && objectToInteract.GetComponent<IInteractablePart>() != null)
                        {
                            objectToInteract.GetComponent<IInteractablePart>().InteractivePart(AngleY);
                        }
                    
                        yield return new WaitForSeconds(0.001f);
                    }
                }
                else if (rotateZ)
                {
                    for (var i = AngleZ; i > minDegreesRotationZ; i -= 1)
                    {
                        transform.Rotate(Vector3.forward, -1f);
                        AngleZ -= 1f;
                    
                        if (objectToInteract != null && objectToInteract.GetComponent<IInteractablePart>() != null)
                        {
                            objectToInteract.GetComponent<IInteractablePart>().InteractivePart(AngleZ);
                        }
                    
                        yield return new WaitForSeconds(0.001f);
                    }
                }
                
                if (objectToInteract != null && objectToInteract.GetComponent<IInteractablePart>() != null)
                {
                    objectToInteract.GetComponent<IInteractablePart>().CheckPositionToReset();
                }
            }
        }
    }
}