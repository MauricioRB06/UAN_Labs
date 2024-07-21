
// Copyright (c) 2022 MauricioRB06 <https://github.com/MauricioRB06>
// Supervisory Teacher: herrera78 <https://github.com/herrera78>
//
// The Purpose Of This Script Is:
//
// Set the behavior of the microscope Switch.
//
// Documentation and References:
//
// C# Interfaces: https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/interfaces/
//
// Last Update: 11.12.2022 By MauricioRB06

using System;
using System.Collections;
using Interfaces;
using Managers;
using UnityEngine;

namespace Biology.Microscope
{
    public class MicroscopeSwitch : MonoBehaviour, IInteractable
    {
        // Delegate to notify about Microscope Switch State.
        public static event Action OnMicroscopeSwitch;
        
        [Header("Microscope Switch Sfx")][Space(5)]
        [Tooltip("Place here the microscope switch sound effect.")]
        [SerializeField] private AudioClip switchSound;

        private static void MicroscopeSwitchButton() { BiologyStepManager.Instance.SwitchMicroscope(); }
        
        // Interactable Interface Implementation.
        public void Interaction()
        {
            gameObject.transform.GetComponent<BoxCollider>().enabled = false;
            MicroscopeSwitchButton();
            OnMicroscopeSwitch?.Invoke();
            transform.Rotate(Vector3.right, -20);
            AudioManager.Instance.PlaySfx(switchSound);
            BiologyStepManager.Instance.UpdateCounter();
        }
        
        private IEnumerator RestartSwitch()
        {
            yield return new WaitForSeconds(2);
            gameObject.transform.GetComponent<BoxCollider>().enabled = true;
        }
        
    }
}
