
// Copyright (c) 2022 MauricioRB06 <https://github.com/MauricioRB06>
// Collaborator: ShiroKame <https://github.com/ShiroKame>
// Supervisory Teacher: herrera78 <https://github.com/herrera78>
//
// The Purpose Of This Script Is:
//
// Set the behavior of the Fenolftaleina
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

namespace Chemistry.G3
{
    // Components required for this Script to work.
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(BoxCollider))]
    
    public class G3Fenolftaleina : MonoBehaviour, IInteractable
    {
        [Space(2)]
        [Header("Step Settings")]
        [Space(5)]
        [Tooltip("Set the step at which the Fenolftaleina will be enabled.")]
        [SerializeField] private List<int> stepsToEnable = new();
        [Tooltip("Set the step at which the Fenolftaleina will move to its initial position.")]
        [SerializeField] private int stepStart;
        [Tooltip("Set the step in which the Fenolftaleina will be interactive.")]
        [SerializeField] private int stepInteract;
        [Space(15)]
        
        [Header("Fenolftaleina Settings")]
        [Space(5)]
        [Tooltip("Place here the light that will interact with the Fenolftaleina.")]
        [SerializeField] private GameObject objectLight;

        // This variable will be used to store the reference to the object's collider.
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
                .Where(stepTrigger => stepTrigger == stepStart)
                .Subscribe(_ => { gameObject.GetComponent<Animator>().Play("Dropper Start"); });
            
            StepManager.Instance.Counter
                .Where(stepTrigger => stepTrigger == stepInteract)
                .Subscribe(_ => { gameObject.GetComponent<Animator>().Play("Dropper Interact");});
            
            StepManager.Instance.Counter
                .Where(stepTrigger => stepsToEnable.Contains(stepTrigger))
                .Subscribe(_ =>
                {
                    objectLight.SetActive(true);
                    _objectCollider.enabled = true;
                });
        }
        
        // This functions are called within the animator as trigger.
        // ReSharper disable once UnusedMember.Local  ( Jetbrains Rider Notation ).
        private void AnimationFinishTrigger() { StepManager.Instance.UpdateCounter(); }

        public void Interaction()
        {
            _objectCollider.enabled = false;
            objectLight.SetActive(false);
            StepManager.Instance.UpdateCounter();
        }
        
    }
}
