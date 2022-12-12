
// Copyright (c) 2022 MauricioRB06 <https://github.com/MauricioRB06>
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
// Last Update: 11.12.2022 By MauricioRB06

using UnityEngine;

namespace Managers
{
        public class BiologyCursorManager : MonoBehaviour
        {
                
                // Object where the single instance of the class will be stored.
                public static BiologyCursorManager Instance { get; private set; }

                [Header("Cursor Settings")] [Space(5)]
                [Tooltip("Cursor in its default form")]
                [SerializeField] private Texture2D normalCursor;
                [Tooltip("Cursor in its clicked form")]
                [SerializeField] private Texture2D clickedCursor;
                [Tooltip("Cursor in its hover form")]
                [SerializeField] private Texture2D hoverCursor;

                // Default settings are set when the cursor gesture is created.
                private void Awake()
                {
                        if (Instance == null)
                        { 
                                Instance = this;
                        }
                        else
                        {
                                Destroy(gameObject);
                        }
                        
                        EnableCursor();
                        ChangeCursor(normalCursor);
                        Cursor.lockState = CursorLockMode.Confined;
                }

                // Functions to change the state of the cursor.
                public void EnableCursor() { Cursor.lockState = CursorLockMode.Confined; Cursor.visible = true; }
                public void DisableCursor() { Cursor.lockState = CursorLockMode.Locked; Cursor.visible = false; }
                
                // Functions to change the cursor aesthetics.
                public void CursorNormal() => ChangeCursor(normalCursor);
                public void CursorHover() => ChangeCursor(hoverCursor);
                public void CursorClick() { ChangeCursor(clickedCursor); }

                // Function to modify the aesthetics of the cursor.
                private static void ChangeCursor(Texture2D cursorType)
                {
                        Cursor.SetCursor(cursorType, Vector2.zero, CursorMode.Auto);
                }
                
        }
}
