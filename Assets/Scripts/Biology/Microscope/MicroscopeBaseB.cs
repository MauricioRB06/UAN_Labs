
// Copyright (c) 2022 MauricioRB06 <https://github.com/MauricioRB06>
// Supervisory Teacher: herrera78 <https://github.com/herrera78>
//
// The Purpose Of This Script Is:
//
// Set the behavior of the Type A microscope base.
//
// Documentation and References:
//
// C# Interfaces: https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/interface
//
// Last Update: 11.12.2022 By MauricioRB06

using Interfaces;
using UnityEngine;

namespace Biology.Microscope
{
    public class MicroscopeBaseB : MonoBehaviour, IInteractablePart
    {
        [Header("Base A Settings")][Space(5)]
        [Tooltip("Place here the object that will be the base B.")]
        [SerializeField] private Transform baseB;
        [Tooltip("Place here the object that represents the initial position of the base B.")]
        [SerializeField] private Transform startPosition;
        [Tooltip("Place here the object that represents the final position of base B.")]
        [SerializeField] private Transform endPosition;
        
        [Header("Movement Range Settings")][Space(5)]
        [Tooltip("Set here the scale of movement to be had when the base is away from the target position.")]
        [SerializeField] private float scaleLongMovement = 0.001f;
        [Tooltip("Set here the scale of movement to be had when the the base is close to the target position.")]
        [SerializeField] private float scaleShortMovement = 0.01f;
        
        // It is used to store the value of the last movement and to be able to compare which way the base should move.
        private float _lastMovementValue;
        
        // Interactable Interface Implementation
        public void InteractivePart(float movementRange)
        {
            if (_lastMovementValue < movementRange)
            {
                // Debug.Log("Alfrente: " + Vector3.Distance(baseB.position, endPosition.position) );
                _lastMovementValue = movementRange;

                if (Vector3.Distance(baseB.position, endPosition.position) > 0.11f)
                {
                    baseB.transform.position = Vector3.Lerp(baseB.position, endPosition.position, scaleLongMovement);
                }
                else
                {
                    baseB.transform.position = Vector3.Lerp(baseB.position, endPosition.position, scaleShortMovement);
                }
            }
            else if (_lastMovementValue > movementRange)
            {
                // Debug.Log("Atras: " + Vector3.Distance(baseB.position, startPosition.position) );
                _lastMovementValue = movementRange;
                
                if (Vector3.Distance(baseB.position, startPosition.position) > 0.11f)
                {
                    baseB.transform.position = Vector3.Lerp(baseB.position, startPosition.position, scaleLongMovement);
                }
                else
                {
                    baseB.transform.position = Vector3.Lerp(baseB.position, startPosition.position, scaleShortMovement);
                }
            }
        }
        
    }
}
