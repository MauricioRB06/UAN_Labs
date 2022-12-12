
// Copyright (c) 2022 MauricioRB06 <https://github.com/MauricioRB06>
// Supervisory Teacher: herrera78 <https://github.com/herrera78>
//
// The Purpose Of This Script Is:
//
// Set the behavior of the interface that indicates the status of the microscope.
//
// Last Update: 11.12.2022 By MauricioRB06

using UnityEngine;
using UnityEngine.UI;

namespace Biology.Microscope
{
    public class UIMicroscopeState : MonoBehaviour
    {
        [Header("Image State Settings")][Space(5)]
        [Tooltip("Place here the image that represents the state of the microscope.")]
        [SerializeField] private Image imageComponent;
        [Tooltip("Place here the sprite that represents the microscope power on.")]
        [SerializeField] private Sprite microscopeOnSprite; 
        [Tooltip("Place here the sprite that represents the turning off of the microscope.")]
        [SerializeField] private Sprite microscopeOffSprite;
        [Tooltip("Place here the Object Reference to the microscope viewer interface.")]
        [SerializeField] private GameObject microscopeLensUI; 
        
        // It is used to know the status of the microscope.
        private bool _microscopeState;

        private void OnEnable() { MicroscopeSwitch.OnMicroscopeSwitch += MicroscopeSwitchButton; }
        private void OnDisable() { MicroscopeSwitch.OnMicroscopeSwitch -= MicroscopeSwitchButton; }
        private void Awake() { microscopeLensUI.SetActive(false); }
        
        private void MicroscopeSwitchButton()
        {
            if (_microscopeState)
            {
                imageComponent.sprite = microscopeOffSprite;
                _microscopeState = false;
            }
            else
            {
                imageComponent.sprite = microscopeOnSprite;
                _microscopeState = true;
                microscopeLensUI.SetActive(true);
            }
        }
        
    }
}
