
// Copyright (c) 2022 MauricioRB06 <https://github.com/MauricioRB06>
// Collaborator: ShiroKame <https://github.com/ShiroKame>
// Supervisory Teacher: herrera78 <https://github.com/herrera78>
//
// The Purpose Of This Script Is:
//
// Define the behavior of the button to turn on the magnetic stirrer.
//
// Documentation and References:
//
// UniRx: https://github.com/neuecc/UniRx
// UniRx: https://programmerclick.com/article/74731581924/ [Spanish]
// C# Interfaces: https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/interface
//
// Last Update: 23.06.2022 By MauricioRB06

using System.Collections.Generic;
using Interfaces;
using Managers;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Chemistry.G4
{
    // Components required for this Script to work.
    [RequireComponent(typeof(BoxCollider))]
    
    public class G4BtnStirrer : MonoBehaviour, IInteractable
    {
        
        [Space(2)]
        [Header("Button Settings")]
        [Space(5)]
        [Tooltip("Set the steps where the button will be interactive.")]
        [SerializeField] private List<int> stepsEnable = new();
        [Tooltip("Place here the light that will interact with the button.")]
        [SerializeField] private GameObject objectLight;
        
        // Variable to store the reference to the object's collider.
        private BoxCollider _objectCollider;
        
        // Variable initialization (Awake is executed when the object is created).
        private void Awake()
        {
            _objectCollider = gameObject.GetComponent<BoxCollider>();
            _objectCollider.enabled = false;
        }
        
        // Observer subscriptions (Start - Runs on the first frame of the game).
        private void Start()
        {
            StepManager.Instance.Counter
                .Where(stepTrigger => stepsEnable.Contains(stepTrigger))
                .Subscribe(_ =>
                {
                    objectLight.SetActive(true);
                    _objectCollider.enabled = true;
                });
        }

        // Function that is called when the object is clicked (interface implementation).
        public void Interaction()
        {
            if ( StepManager.Instance.Counter.Value == 18 && SceneManager.GetActiveScene().name == "Chemistry_G3")
            {
                StepManager.Instance.SwitchCamera();
            }
            
            _objectCollider.enabled = false;
            objectLight.SetActive(false);
            StepManager.Instance.UpdateCounter();
        }
        
    }
}
