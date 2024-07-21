
// Copyright (c) 2022 MauricioRB06 <https://github.com/MauricioRB06>
// Collaborator: ShiroKame <https://github.com/ShiroKame>
// Supervisory Teacher: herrera78 <https://github.com/herrera78>
//
// The Purpose Of This Script Is:
//
// Establish the behavior of the beaker containing chemicals.
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
using Liquid;
using Managers;
using UniRx;
using UnityEngine;

namespace Chemistry.G4
{
    
    // Components required for this Script to work.
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(BoxCollider))]
    
    public class G4BeakerComponent : MonoBehaviour, IInteractable
    {
        
        [Space(2)]
        [Header("Step Settings")]
        [Space(5)]
        [Tooltip("Set the step at which the beaker will be enabled.")]
        [SerializeField] private List<int> stepsToEnable = new();
        [Tooltip("Set the step at which the beaker will move to its initial position.")]
        [SerializeField] private int stepStart;
        [Tooltip("Set the step in which the beaker will be interactive.")]
        [SerializeField] private int stepInteract;
        [Space(15)]
        
        [Header("Beaker Settings")]
        [Space(5)]
        [Tooltip("Place here the light that will interact with the beaker.")]
        [SerializeField] private GameObject objectLight;
        [Tooltip("Place here the object that will be the liquid in the burette.")]
        [SerializeField] private GameObject buretteLiquid;
        [Tooltip("Place here the liquid generator that will be used for this beaker.")]
        [SerializeField] private GameObject liquidGenerator;
        [Tooltip("Place here the object that will be the liquid contained in the beaker.")]
        [SerializeField] private GameObject liquidComponent;
        
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
                .Subscribe(_ => { gameObject.GetComponent<Animator>().Play("Beaker Component Start"); });
            
            StepManager.Instance.Counter
                .Where(stepTrigger => stepTrigger == stepInteract)
                .Subscribe(_ => { gameObject.GetComponent<Animator>().Play("Beaker Component Interact");});
            
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
        private void AnimationTriggerA() { StepManager.Instance.SwitchCamera(); }
        // ReSharper disable once UnusedMember.Local  ( Jetbrains Rider Notation ).
        private void AnimationTriggerB() {liquidComponent.GetComponent<LiquidCompoment>().EmptyBeaker(); }
        // ReSharper disable once UnusedMember.Local  ( Jetbrains Rider Notation ).
        private void AnimationFinishTrigger() { StepManager.Instance.UpdateCounter(); }
        // ReSharper disable once UnusedMember.Local  ( Jetbrains Rider Notation ).
        private void AnimationBuretteTrigger() { buretteLiquid.GetComponent<BuretteLiquid>().FillBurette(); }
        // ReSharper disable once UnusedMember.Local  ( Jetbrains Rider Notation ).
        private void AnimationLiquidTrigger() { liquidGenerator.GetComponent<LiquidGenerator>().LiquidIn(); }
        
        // Function that is called when the object is clicked (interface implementation).
        public void Interaction()
        {
            _objectCollider.enabled = false;
            objectLight.SetActive(false);
            StepManager.Instance.UpdateCounter();
        }
        
    }
}
