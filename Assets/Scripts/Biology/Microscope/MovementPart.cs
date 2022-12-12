
// Copyright (c) 2022 MauricioRB06 <https://github.com/MauricioRB06>
// Supervisory Teacher: herrera78 <https://github.com/herrera78>
//
// The Purpose Of This Script Is:
//
// Establish the behavior of parts that move in consequence of the movement of another part.
//
// Documentation and References:
//
// C# Interfaces: https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/interfaces/
//
// Last Update: 11.12.2022 By MauricioRB06

using Interfaces;
using UnityEngine;

namespace Biology.Microscope
{
    public class MovementPart : MonoBehaviour, IInteractablePart
    {
        [Header("Part Settings")][Space(5)]
        [Tooltip("Place here the object that will be the Part.")]
        [SerializeField] private Transform objectToMove;
        [Tooltip("Place here the object that represents the initial position of the Part.")]
        [SerializeField] private Transform startPosition;
        [Tooltip("Place here the object that represents the final position of Part")]
        [SerializeField] private Transform endPosition;
        [Space(15)]
        
        [Header("Movement Range Settings")][Space(5)]
        [Tooltip("Set here the scale of movement to be had when the Part is away from the target position.")]
        [SerializeField] private float scaleLongMovement = 0.001f;
        [Tooltip("Set here the scale of movement to be had when the the Part is close to the target position.")]
        [SerializeField] private float scaleShortMovement = 0.01f;

        // It is used to store the value of the last movement and to be able to compare which way the part should move.
        private float _lastMovementValue;
        
        // Interactable Interface Implementation
        public void InteractivePart(float movementRange)
        {
            if (_lastMovementValue < movementRange)
            {
                _lastMovementValue = movementRange;

                if (Vector3.Distance(objectToMove.position, endPosition.position) > 0.2f)
                {
                    objectToMove.transform.position = Vector3.Lerp(objectToMove.position, endPosition.position, scaleLongMovement);
                }
                else
                {
                    objectToMove.transform.position = Vector3.Lerp(objectToMove.position, endPosition.position, scaleShortMovement);
                }
            }
            else if (_lastMovementValue > movementRange)
            {
                _lastMovementValue = movementRange;
                
                if (Vector3.Distance(objectToMove.position, startPosition.position) > 0.2f)
                {
                    objectToMove.transform.position = Vector3.Lerp(objectToMove.position, startPosition.position, scaleLongMovement);
                }
                else
                {
                    objectToMove.transform.position = Vector3.Lerp(objectToMove.position, startPosition.position, scaleShortMovement);
                }
            }
        }
        
    }
}
