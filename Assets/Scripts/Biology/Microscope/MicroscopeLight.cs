
// Copyright (c) 2022 MauricioRB06 <https://github.com/MauricioRB06>
// Supervisory Teacher: herrera78 <https://github.com/herrera78>
//
// The Purpose Of This Script Is:
//
// Sets the behavior of the microscope light.
//
// Last Update: 11.12.2022 By MauricioRB06

using Interfaces;
using UnityEngine;

namespace Biology.Microscope
{
    // Components required for this script work.
    [RequireComponent(typeof(Light))]
    public class MicroscopeLight : MonoBehaviour, IInteractablePart
    {
        // It is used to store the reference to light.
        private Light _light;
        
        private void Awake()
        {
            _light = GetComponent<Light>();
            _light.intensity = 0;
        }
        
        // InteractablePart Interface Implementation.
        public void InteractivePart(float movementRange)
        {
            _light.intensity = _light.intensity switch
            {
                > 0.5f => 0.5f,
                < 0.1f => 0f,
                _ => movementRange * 0.005f
            };
        }
        
    }
}
