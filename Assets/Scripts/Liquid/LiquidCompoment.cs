
// Copyright (c) 2022 MauricioRB06 <https://github.com/MauricioRB06>
// Collaborator: ShiroKame <https://github.com/ShiroKame>
// Supervisory Teacher: herrera78 <https://github.com/herrera78>
//
// The Purpose Of This Script Is:
//
// Define the liquid behavior of beakers containing chemicals.
//
// Last Update: 22.06.2022 By MauricioRB06

using UnityEngine;

namespace Liquid
{
    // Components required for this Script to work.
    [RequireComponent(typeof(Animator))]
    
    public class LiquidCompoment : MonoBehaviour
    {
        
        // Plays the animation that empties the liquid from the beakers to be emptied into the burette.
        public void EmptyBeaker() { gameObject.GetComponent<Animator>().Play("Component Liquid Empty"); }
        
    }
}
