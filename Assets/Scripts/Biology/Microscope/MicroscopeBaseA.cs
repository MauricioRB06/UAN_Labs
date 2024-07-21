
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
    public class MicroscopeBaseA : MonoBehaviour, IInteractablePart
    {
        [Header("Base A Settings")][Space(5)]
        [Tooltip("Place here the object that will be the base A.")]
        [SerializeField] private Transform baseA;
        [Tooltip("Place here the object that represents the initial position of the base A.")]
        [SerializeField] private Transform startPosition;
        [Tooltip("Place here the object that represents the final position of base A.")]
        [SerializeField] private Transform endPosition;
        [Space(15)]
        
        [Header("Movement Range Settings")][Space(5)]
        [Tooltip("Set here the scale of movement to be had when the base is away from the target position.")]
        [SerializeField] private float scaleLongMovement = 0.001f;
        [Tooltip("Set here the scale of movement to be had when the the base is close to the target position.")]
        [SerializeField] private float scaleShortMovement = 0.01f;
        
        [Header("Movement Range Settings")][Space(5)]
        [Tooltip("Set here the scale of movement to be had when the base is away from the target position.")]
        [SerializeField] private GameObject warningMessage;
        
        // It is used to store the value of the last movement and to be able to compare which way the base should move.
        private float _lastMovementValue;

        private float _startPositionX;
        private float _startPositionY;
        private float _startPositionZ;

        private void Start()
        {
            var position = baseA.transform.localPosition;
            _startPositionX = position.x;
            _startPositionY = position.y;
            _startPositionZ = position.z;
        }

        public void InteractivePart(float movementRange, bool enableX100Lens) { }

        public void CheckPositionToReset()
        {
            baseA.transform.localPosition = new Vector3(_startPositionX, _startPositionY, _startPositionZ);
        }
        
        // Interactable Interface Implementation
        public void InteractivePart(float movementRange)
        {
            if (Vector3.Distance(baseA.position, endPosition.position) > 0.08f)
            {
                warningMessage.SetActive(false); 
            }
            else
            {
                warningMessage.SetActive(true);
            }
            
            if (_lastMovementValue < movementRange)
            {
                //Debug.Log("Arriba: " + Vector3.Distance(baseA.position, endPosition.position) );
                _lastMovementValue = movementRange;

                if (Vector3.Distance(baseA.position, endPosition.position) > 0.08f)
                {
                    baseA.transform.position = Vector3.Lerp(baseA.position, endPosition.position, scaleLongMovement);
                }
                else
                {
                    baseA.transform.position = Vector3.Lerp(baseA.position, endPosition.position, scaleShortMovement);
                }
            }
            else if (_lastMovementValue > movementRange)
            {
                //Debug.Log("Abajo: " + Vector3.Distance(baseA.position, startPosition.position) );
                _lastMovementValue = movementRange;
                
                if (Vector3.Distance(baseA.position, startPosition.position) > 0.08f)
                {
                    baseA.transform.position = Vector3.Lerp(baseA.position, startPosition.position, scaleLongMovement);
                }
                else
                {
                    baseA.transform.position = Vector3.Lerp(baseA.position, startPosition.position, scaleShortMovement);
                }
            }
        }

    }
}
