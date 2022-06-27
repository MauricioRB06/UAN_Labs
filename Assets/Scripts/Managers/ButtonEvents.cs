
// Copyright (c) 2022 MauricioRB06 <https://github.com/MauricioRB06>
// Collaborator: ShiroKame <https://github.com/ShiroKame>
// Supervisory Teacher: herrera78 <https://github.com/herrera78>
//
// The Purpose Of This Script Is:
//
// Define the behavior and configuration of the cursor, allowing it to identify clickable objects,
// trigger events and functions to modify the state of the cursor.
//
// Last Update: 23.06.2022 By MauricioRB06

using UnityEngine.EventSystems;

namespace Managers
{
    public class ButtonEvents : EventTrigger
    {
        
        // Reads the Pointer values from the cursor and when the cursor enters, modifies it.
        public override void OnPointerEnter(PointerEventData eventData)
        {
            CursorManager.instance.HoverCursor();
        }
        
        // Reads the values that Pointer reads from the cursor and when the cursor exits, modifies it.
        public override void OnPointerExit(PointerEventData eventData)
        {
            CursorManager.instance.DefaultCursor();
        }
        
    }
}
