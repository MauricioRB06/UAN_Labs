
// Copyright (c) 2022 MauricioRB06 <https://github.com/MauricioRB06>
// Collaborator: ShiroKame <https://github.com/ShiroKame>
// Supervisory Teacher: herrera78 <https://github.com/herrera78>
//
// The Purpose Of This Script Is:
//
// Define the behavior and configuration of the cursor, allowing it to identify clickable objects,
// trigger events and functions to modify the state of the cursor.
//
// Documentation and References:
//
// UniRx: https://github.com/neuecc/UniRx
// UniRx: https://programmerclick.com/article/74731581924/ [Spanish] 
//
// Last Update: 23.06.2022 By MauricioRB06

using Interfaces;
using UnityEngine;

namespace Managers
{ 
        public class CursorManager : MonoBehaviour
        {
                
                // Object where the single instance of the class will be stored.
                public static CursorManager instance;
                
                [Space(8)]
                [Header("Main Camera")] [Space(5)]
                [Tooltip("Please add the Scene Main Camera Here.")]
                [SerializeField] private Camera mainCamera;
                [Space(15)]
                
                [Header("Cursor Settings")] [Space(5)]
                [Tooltip("Cursor in its default form")]
                [SerializeField] private Texture2D normalCursor;
                [Tooltip("Cursor in its clicked form")]
                [SerializeField] private Texture2D clickedCursor;
                [Tooltip("Cursor in its hover form")]
                [SerializeField] private Texture2D hoverCursor;

                // Variable to store the input controller.
                private CursorControls _controls;

                private Ray _cursorObjectsRay;
                        
                // Class Constructor
                public CursorManager()
                {
                        if (instance == null)
                        { 
                                instance = this;
                        }
                        else
                        {
                                Destroy(gameObject);
                        }
                }
                
                // Default settings are set when the cursor gesture is created.
                private void Awake()
                {
                        if (mainCamera == null)
                        {
                                Debug.LogError("Please add MainCamera to Cursor Manager.");
                        }
                        else
                        {
                                _controls = new CursorControls();
                                ChangeCursor(normalCursor);
                                Cursor.lockState = CursorLockMode.Confined;   
                        }
                }
                
                // Enables or disables the functionality of the controls, when the cursor is activated and deactivated.
                private void OnEnable() { _controls.Enable(); }
                private void OnDisable() { _controls.Disable(); }
                
                // Establish the functions that will be called by the controls already defined.
                private void Start()
                {
                        _controls.Mouse.Click.started += _ => StarterClick();
                        _controls.Mouse.Click.performed += _ => EndedClick();
                }
                
                // Functions to change the state of the cursor.
                public  void EnableCursor() { Cursor.lockState = CursorLockMode.Confined; Cursor.visible = true; }
                public  void DisableCursor() { Cursor.lockState = CursorLockMode.Locked; Cursor.visible = false; }
                
                // Functions to change the cursor aesthetics.
                public void DefaultCursor() => ChangeCursor(normalCursor);
                public void HoverCursor() => ChangeCursor(hoverCursor);
                private void StarterClick() { ChangeCursor(clickedCursor); }
                private void EndedClick() { ChangeCursor(normalCursor); DetectObject(); }
                
                // Function to detect 2D and 3D objects using Raycast.
                private void DetectObject()
                {
                        // The raycast is launched from the mouse position, in the direction in which the camera is pointing.
                        var ray = mainCamera.ScreenPointToRay(_controls.Mouse.Position.ReadValue<Vector2>());
                        
                        // Detection of 3D objects.
                        if (Physics.Raycast(ray, out var hit3D))
                        { 
                                if (hit3D.collider != null)
                                {
                                        var click = hit3D.collider.gameObject.GetComponent<IInteractable>();
                                        click?.Interaction();

                                        //Debug.Log("3D Hit: " + hit3D.collider.gameObject.name);
                                }
                        }
                        
                        // Detection of 2D objects.
                        var hit2D = Physics2D.GetRayIntersection(ray);
                        if (hit2D.collider == null) return;
                        {
                                var click = hit2D.collider.gameObject.GetComponent<IInteractable>();
                                click?.Interaction();
                                
                                //Debug.Log("2D Hit: " + hit2D.collider.gameObject.name);
                        }
                }
                
                // Function to modify the aesthetics of the cursor.
                private static void ChangeCursor(Texture2D cursorType)
                {
                        Cursor.SetCursor(cursorType, Vector2.zero, CursorMode.Auto);
                }
                
        }
}
