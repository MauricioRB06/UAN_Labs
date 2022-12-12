
// Copyright (c) 2022 MauricioRB06 <https://github.com/MauricioRB06>
// Supervisory Teacher: herrera78 <https://github.com/herrera78>
//
// The Purpose Of This Script Is:
//
// Establish the behavior of the Screw Platform.
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
    public class MicroscopeScrewPlatform : MonoBehaviour, IInteractablePart
    {
        
        [Header("Screw Platform Settings")][Space(5)]
        [Tooltip("Place here the object that will be the Screw Platform.")]
        [SerializeField] private Transform screwPlatform;
        [Tooltip("Place here the object that represents the initial position of the Screw Platform.")]
        [SerializeField] private Transform startPosition;
        [Tooltip("Place here the object that represents the final position of Screw Platform.")]
        [SerializeField] private Transform endPosition;
        
        [Header("Movement Range Settings")][Space(5)]
        [Tooltip("Set here the scale of movement to be had when the platform is away from the target position.")]
        [SerializeField] private float scaleLongMovement = 0.001f;
        [Tooltip("Set here the scale of movement to be had when the the platform is close to the target position.")]
        [SerializeField] private float scaleShortMovement = 0.01f;
        
        // It is used to store the value of the last movement and to be able to compare which way the platform should move.
        private float _lastMovementValue;
        
        // Interactable Interface Implementation
        public void InteractivePart(float movementRange)
        {
            if (_lastMovementValue < movementRange)
            {
                //Debug.Log("Izquierda: " + Vector3.Distance(screwPlatform.position, endPosition.position) );
                _lastMovementValue = movementRange;

                if (Vector3.Distance(screwPlatform.position, endPosition.position) > 0.07f)
                {
                    screwPlatform.transform.position = Vector3.Lerp(screwPlatform.position, endPosition.position, scaleLongMovement);
                }
                else
                {
                    screwPlatform.transform.position = Vector3.Lerp(screwPlatform.position, endPosition.position, scaleShortMovement);
                }
            }
            else if (_lastMovementValue > movementRange)
            {
                //Debug.Log("Derecha: " + Vector3.Distance(screwPlatform.position, startPosition.position) );
                _lastMovementValue = movementRange;
                
                if (Vector3.Distance(screwPlatform.position, startPosition.position) > 0.07f)
                {
                    screwPlatform.transform.position = Vector3.Lerp(screwPlatform.position, startPosition.position, scaleLongMovement);
                }
                else
                {
                    screwPlatform.transform.position = Vector3.Lerp(screwPlatform.position, startPosition.position, scaleShortMovement);
                }
            }
        }
        
    }
}
