
// Copyright (c) 2022 MauricioRB06 <https://github.com/MauricioRB06>
// Collaborator: ShiroKame <https://github.com/ShiroKame>
// Supervisory Teacher: herrera78 <https://github.com/herrera78>
//
// The Purpose Of This Script Is:
//
// Set the behavior of the Dropper Bottle.
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
    public class G2DropperBottle : MonoBehaviour, IInteractable
    {
        
        [Space(2)]
        [Header("Step Settings")]
        [Space(5)]
        [Tooltip("Set the step at which the dropper bottle will be move to start position.")]
        [SerializeField] private int stepToIn;
        [Tooltip("Set the step at which the dropper bottle will be move to interact position.")]
        [SerializeField] private int stepToPosition;
        [Tooltip("Set the step at which the dropper bottle will be enabled to interact.")]
        [SerializeField] private int stepToInteract;
        [Tooltip("Set the steps at which the dropper bottle will be enabled.")]
        [SerializeField] private List<int> stepsToEnable;
        
        [Header("Dropper Settings")]
        [Space(5)]
        [Tooltip("Place here the light that will interact with the dropper bottle.")]
        [SerializeField] private GameObject objectLight;
        [Tooltip("Place the dropper bottle liquid object here.")]
        [SerializeField] private GameObject h2OLiquid;
        
        // This variable will be used to store the reference to the object's collider.
        private BoxCollider _objectCollider;
        
        // Observer subscriptions (Awake is executed when the object is created).
        private void Awake()
        {
            _objectCollider = gameObject.GetComponent<BoxCollider>();
            _objectCollider.enabled = false;
            
            StepManager.instance.Counter
                .Where(stepTrigger => stepTrigger == stepToIn)
                .Subscribe(_ => { gameObject.GetComponent<Animator>().Play("Dropper Bottle Start"); });
            
            StepManager.instance.Counter
                .Where(stepTrigger => stepTrigger == stepToPosition)
                .Subscribe(_ => { gameObject.GetComponent<Animator>().Play("Dropper Bottle Position"); });
            
            StepManager.instance.Counter
                .Where(stepTrigger => stepTrigger == stepToInteract)
                .Subscribe(_ => { gameObject.GetComponent<Animator>().Play("Dropper Bottle Interact"); });
            
            StepManager.instance.Counter
                .Where(stepTrigger => stepsToEnable.Contains(stepTrigger))
                .Subscribe(_ =>
                {
                    objectLight.SetActive(true);
                    _objectCollider.enabled = true;
                });
        }
        
        // This functions are called within the animator as trigger.
        
        // ReSharper disable once UnusedMember.Local  ( Jetbrains Rider Notation ).
        private void AnimationTriggerA() { StepManager.instance.SwitchCamera(); }
        
        // ReSharper disable once UnusedMember.Local  ( Jetbrains Rider Notation ).
        private void AnimationTriggerB() { h2OLiquid.GetComponent<G2H2OLiquid>().EmptyBeaker2(); }
        
        // ReSharper disable once UnusedMember.Local  ( Jetbrains Rider Notation )
        // This function is called within the animator as a trigger.
        private void AnimationFinishTrigger() { StepManager.instance.UpdateCounter(); }
        
        // Function that is called when the object is clicked (interface implementation).
        public void Interaction()
        {
            _objectCollider.enabled = false;
            objectLight.SetActive(false);
            StepManager.instance.UpdateCounter();
        }
        
    }
}
