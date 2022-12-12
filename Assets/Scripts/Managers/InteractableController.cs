
// Copyright (c) 2022 MauricioRB06 <https://github.com/MauricioRB06>
// Supervisory Teacher: herrera78 <https://github.com/herrera78>
//
// The Purpose Of This Script Is:
//
// Set the behavior of the controls that allow you to drag objects.
//
// Last Update: 11.12.2022 By MauricioRB06

using System.Collections;
using Interfaces;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Managers
{
    public class InteractableController: MonoBehaviour
    {
        [Header("Input Action Settings")][Space(5)]
        [Tooltip("Set the action that will trigger the Drag on the objects.")]
        [SerializeField] private InputAction click;
        [Space(15)]
        
        [Header("Drag Settings")][Space(5)]
        [Tooltip("Set the update rate of the physics in drag.")]
        [SerializeField] private float mouseDragPhysicsSpeed = 10f;
        [Tooltip("Set the drag delay.")]
        [SerializeField] private float mouseDragSpeed = 0.1f;
        [Space(15)]
        
        [Header("Camera Settings")][Space(5)]
        [Tooltip("Place here the camera from where the Drag position will be detected.")]
        [SerializeField] private Camera mainCamera;
        
        private Vector3 _velocity = Vector3.zero;
        
        private void OnEnable()
        {
            click.Enable();
            click.performed += MousePressed;
            
        }
        
        private void OnDisable()
        {
            click.performed -= MousePressed;
            click.Disable();
        }

        private void MousePressed(InputAction.CallbackContext context)
        {
            var ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
            
            if (Physics.Raycast(ray, out var hit, 1000f,LayerMask.GetMask("Draggable")))
            {
                if (hit.collider != null && hit.collider.gameObject.GetComponent<IDraggable>() != null)
                {
                    StartCoroutine(DragUpdatedEvent(hit.collider.gameObject));
                }
            }
            else if(Physics.Raycast(ray, out var hit2, 1000f,LayerMask.GetMask("Interactable")))
            {
                if (hit2.collider != null && hit2.collider.gameObject.GetComponent<IInteractable>() != null)
                {
                    hit2.collider.gameObject.GetComponent<IInteractable>().Interaction();
                }
            }
        }

        private IEnumerator DragUpdatedEvent(GameObject draggedObject)
        {
            var initialDistance = Vector3.Distance(draggedObject.transform.position, mainCamera.transform.position);
            draggedObject.TryGetComponent<Rigidbody>(out var rigidbodyObject);
            draggedObject.TryGetComponent<IDraggable>(out var iDragComponent);
            
            iDragComponent?.OnStartDrag();
            
            while (click.ReadValue<float>() != 0)
            {
                var ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

                if (rigidbodyObject != null)
                {
                    var direction = ray.GetPoint(initialDistance) - draggedObject.transform.position;
                    rigidbodyObject.velocity = direction * mouseDragPhysicsSpeed;
                    yield return new WaitForFixedUpdate();
                }
                else
                {
                    draggedObject.transform.position = Vector3.SmoothDamp(draggedObject.transform.position,
                        ray.GetPoint(initialDistance), ref _velocity, mouseDragSpeed);
                    yield return null;
                }
            }
            iDragComponent?.OnEndDrag();
        }
        
    }
}
