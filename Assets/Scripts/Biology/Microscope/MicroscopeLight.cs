
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

        private const float StartPositionX = 0;
        private const float StartPositionY = 0;
        private const float StartPositionZ = 0;

        public void InteractivePart(float movementRange, bool enableX100Lens) { }

        public void CheckPositionToReset()
        {
            gameObject.transform.rotation = new Quaternion(StartPositionX, StartPositionY, StartPositionZ, 0);
        }
        
        private void OnEnable()
        {
            MicroscopeSwitch.OnMicroscopeSwitch += LightOn;
        }

        private void OnDisable()
        {
            MicroscopeSwitch.OnMicroscopeSwitch -= LightOn;
        }

        private void Awake()
        {
            _light = GetComponent<Light>();
            _light.intensity = 0;
        }
        
        private void LightOn()
        {
            _light.intensity = 0.1f;
        }
        
        // InteractablePart Interface Implementation.
        public void InteractivePart(float movementRange)
        {
            _light.intensity = movementRange * 0.008f;
            
            if (_light.intensity > 0.8f)
            {
                _light.intensity = 0.8f;
            }
            else if (_light.intensity < 0.1f)
            {
                _light.intensity = 0.1f;
            }
        }
    }
}