
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
// Last Update: 22.06.2022 By MauricioRB06

using System.Collections.Generic;
using Interfaces;
using Managers;
using UniRx;
using UnityEngine;

namespace Chemistry.G2
{
    // Components required for this Script to work.
    [RequireComponent(typeof(Animator))]
    
    public class G2BeakerNaCl : MonoBehaviour, IInteractable
    {
        
        [Space(2)]
        [Header("Beaker NaCl Settings")]
        [Space(5)]
        [Tooltip("Set the step where the Beaker NaCl will move to its initial position.")]
        [SerializeField] private int stepToIn;
        [Tooltip("Set the step where the Beaker NaCl will move to its initial interact position.")]
        [SerializeField] private int stepToInteractIn;
        [Tooltip("Set the step where the Beaker NaCl will move to its final interact position.")]
        [SerializeField] private int stepToInteractOut;
        [Tooltip("Set the step at which the NaCl Beaker will perform its mixing animation.")]
        [SerializeField] private int stepToMix;
        [Tooltip("Set the step where the Beaker NaCl will move to its final position.")]
        [SerializeField] private int stepToOut;
        [Tooltip("Set the steps where the Beaker NaCl will be enable.")]
        [SerializeField] private List<int> stepsToEnable = new();
        
        [Header("NaCl Settings")]
        [Space(5)]
        [Tooltip("Place here the light that will interact with the beaker.")]
        [SerializeField] private GameObject objectLight;
        [Tooltip("Place here NaCL Material.")]
        [SerializeField] private GameObject naClMaterial;
        [Tooltip("Place here NaCl Liquid GameObject")]
        [SerializeField] private GameObject naClLiquid;
        
        // This variable will be used to store the reference to the object's collider.
        private BoxCollider _objectCollider;
        
        // Observer subscriptions (Awake is executed when the object is created)
        private void Awake()
        {
            _objectCollider = gameObject.GetComponent<BoxCollider>();
            _objectCollider.enabled = false;
            
            StepManager.Instance.Counter
                .Where(stepTrigger => stepTrigger == stepToIn)
                .Subscribe(_ =>
                {
                    gameObject.GetComponent<Animator>().Play("Start Beaker NaCl");
                });
            
            StepManager.Instance.Counter
                .Where(stepTrigger => stepTrigger == stepToInteractIn)
                .Subscribe(_ =>
                {
                    gameObject.GetComponent<Animator>().Play("Interact In Beaker NaCl");
                });
            
            StepManager.Instance.Counter
                .Where(stepTrigger => stepTrigger == stepToInteractOut)
                .Subscribe(_ =>
                {
                    gameObject.GetComponent<Animator>().Play("Interact Out Beaker NaCl");
                });
            
            StepManager.Instance.Counter
                .Where(stepTrigger => stepTrigger == stepToMix)
                .Subscribe(_ =>
                {
                    gameObject.GetComponent<Animator>().Play("Mix Beaker NaCl");
                });
            
            StepManager.Instance.Counter
                .Where(stepTrigger => stepTrigger == stepToOut)
                .Subscribe(_ =>
                {
                    gameObject.GetComponent<Animator>().Play("End Beaker NaCl");
                });
            
            StepManager.Instance.Counter
                .Where(stepTrigger => stepsToEnable.Contains(stepTrigger))
                .Subscribe(_ =>
                {
                    objectLight.SetActive(true);
                    _objectCollider.enabled = true;
                });
        }
        
        // ReSharper disable once UnusedMember.Local  ( Jetbrains Rider Notation )
        // This function is called within the animator as a trigger.
        private void AnimationNaClMat() { naClMaterial.GetComponent<G2NaClMat>().NaClFillMaterial(); }
        
        // ReSharper disable once UnusedMember.Local  ( Jetbrains Rider Notation )
        // This function is called within the animator as a trigger.
        private void AnimationEmptyBeaker() { naClLiquid.GetComponent<G2NaClLiquid>().EmptyNaCl(); }
        
        // ReSharper disable once UnusedMember.Local  ( Jetbrains Rider Notation )
        // This function is called within the animator as a trigger.
        private void AnimationFinishTrigger() { StepManager.Instance.UpdateCounter(); }
        
        // Function that is called when the object is clicked (interface implementation).
        public void Interaction()
        {
            _objectCollider.enabled = false;
            objectLight.SetActive(false);
            StepManager.Instance.UpdateCounter();
        }
        
    }
}
