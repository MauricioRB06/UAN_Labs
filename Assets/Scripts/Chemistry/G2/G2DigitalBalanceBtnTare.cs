
// Copyright (c) 2022 MauricioRB06 <https://github.com/MauricioRB06>
// Collaborator: ShiroKame <https://github.com/ShiroKame>
// Supervisory Teacher: herrera78 <https://github.com/herrera78>
//
// The Purpose Of This Script Is:
//
// Set the behavior of the tare button of the digital balance.
//
// Documentation and References:
//
// UniRx: https://github.com/neuecc/UniRx
// UniRx: https://programmerclick.com/article/74731581924/ [Spanish]
//
// Last Update: 23.06.2022 By MauricioRB06

using System.Collections.Generic;
using Interfaces;
using Managers;
using UniRx;
using UnityEngine;

namespace Chemistry.G2
{
    public class G2DigitalBalanceBtnTare : MonoBehaviour, IInteractable
    {
        [Tooltip("Set the step at which the button will be enabled.")]
        [SerializeField] private List<int> stepsToEnable = new();
        [Header("Button Settings")]
        [Space(5)]
        [Tooltip("Place here the light that will interact with the button.")]
        [SerializeField] private GameObject objectLight;
        
        // This variable will be used to store the reference to the object's collider.
        private BoxCollider _objectCollider;
        
        // Observer subscriptions (Awake is executed when the object is created).
        private void Awake()
        {
            _objectCollider = gameObject.GetComponent<BoxCollider>();
            _objectCollider.enabled = false;
            
            StepManager.Instance.Counter
                .Where(stepTrigger => stepsToEnable.Contains(stepTrigger))
                .Subscribe(_ =>
                {
                    objectLight.SetActive(true);
                    _objectCollider.enabled = true;
                });
        }
        
        // Function that is called when the object is clicked (interface implementation).
        public void Interaction()
        {
            _objectCollider.enabled = false;
            objectLight.SetActive(false);
            StepManager.Instance.UpdateCounter();
        }
        
    }
}
