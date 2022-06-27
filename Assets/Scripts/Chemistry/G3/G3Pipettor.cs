
// Copyright (c) 2022 MauricioRB06 <https://github.com/MauricioRB06>
// Collaborator: ShiroKame <https://github.com/ShiroKame>
// Supervisory Teacher: herrera78 <https://github.com/herrera78>
//
// The Purpose Of This Script Is:
//
// Set the behavior of the Pipettor.
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
using UnityEngine.Serialization;

namespace Chemistry.G3
{
    // Components required for this Script to work.
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(BoxCollider))]
    
    public class G3Pipettor : MonoBehaviour, IInteractable
    {
        [Space(2)]
        [Header("Step Settings")]
        [Space(5)]
        [Tooltip("Set the step at which the Pipettor will be enabled.")]
        [SerializeField] private List<int> stepsToEnable = new();
        [Tooltip("Set the step at which the Pipettor will move to its initial position.")]
        [SerializeField] private int stepStart;
        [Tooltip("Set the step in which the Pipettor will be interactive.")]
        [SerializeField] private int stepFill;
        [SerializeField] private int stepEmpty;
        [SerializeField] private int stepEnd;
        [Space(15)]
        
        [Header("Beaker Settings")]
        [Space(5)]
        [Tooltip("Place here the light that will interact with the Pipettor.")]
        [SerializeField] private GameObject objectLight;
        [FormerlySerializedAs("PipettorLiquid")]
        [Tooltip("Place here the object that will be the liquid in the Pipettor.")]
        [SerializeField] private GameObject pipettorLiquid;
        [SerializeField] private GameObject erlenmeyerLiquid;

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
            StepManager.instance.Counter
                .Where(stepTrigger => stepTrigger == stepStart)
                .Subscribe(_ =>
                {
                    gameObject.GetComponent<Animator>().Play("Pipettor Start");
                    StepManager.instance.ChangeButtonNext(false);
                });
            
            StepManager.instance.Counter
                .Where(stepTrigger => stepTrigger == stepFill)
                .Subscribe(_ => { gameObject.GetComponent<Animator>().Play("Pipettor Fill");});
            
            StepManager.instance.Counter
                .Where(stepTrigger => stepTrigger == stepEmpty)
                .Subscribe(_ => { gameObject.GetComponent<Animator>().Play("Pipettor Empty");});
            
            StepManager.instance.Counter
                .Where(stepTrigger => stepTrigger == stepEnd)
                .Subscribe(_ => { gameObject.GetComponent<Animator>().Play("Pipettor End");});
            
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
        private void AnimationFinishTrigger() { StepManager.instance.UpdateCounter(); }
        
        // ReSharper disable once UnusedMember.Local  ( Jetbrains Rider Notation ).
        private void AnimationTriggerA() { pipettorLiquid.GetComponent<Animator>().Play("Pipettor Liquid Fill"); }
        
        // ReSharper disable once UnusedMember.Local  ( Jetbrains Rider Notation ).
        private void AnimationTriggerB() { pipettorLiquid.GetComponent<Animator>().Play("Pipettor Liquid Empty"); }
        
        // This functions are called within the animator as trigger.
        // ReSharper disable once UnusedMember.Local  ( Jetbrains Rider Notation ).
        private void AnimationTriggerC()
        {
            erlenmeyerLiquid.GetComponent<G3ErlenmeyerLiquid>().AnimationFillTrigger();
        }
        
        // Function that is called when the object is clicked (interface implementation).
        public void Interaction()
        {
            _objectCollider.enabled = false;
            objectLight.SetActive(false);
            StepManager.instance.UpdateCounter();
        }
        
    }
}
