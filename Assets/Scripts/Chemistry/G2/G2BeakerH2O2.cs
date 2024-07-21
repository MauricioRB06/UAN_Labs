
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

using Interfaces;
using Managers;
using UniRx;
using UnityEngine;

namespace Chemistry.G2
{
    public class G2BeakerH2O2 : MonoBehaviour, IInteractable
    {
        [Space(2)]
        [Header("Step Settings")]
        [Space(5)]
        [Tooltip("Set the step where the Beaker H2O2 will move to its initial position.")]
        [SerializeField] private int stepToIn;
        [Tooltip("Set the step where the Beaker H2O2 will move to its end position.")]
        [SerializeField] private int stepToOut;
        [Tooltip("Set the step where the Beaker H2O2 will be enable.")]
        [SerializeField] private int stepToEnable;
        [Tooltip("Set the step where the Beaker H2O2 will be fill.")]
        [SerializeField] private int stepToFill;
        [Tooltip("Enter here the H2O Liquid GameObject.")]
        [SerializeField] private GameObject h2OLiquid;
        
        [Header("Beaker Settings")]
        [Space(5)]
        [Tooltip("Place here the light that will interact with the beaker.")]
        [SerializeField] private GameObject objectLight;
        
        // This variable will be used to store the reference to the object's collider.
        private BoxCollider _objectCollider;
        
        // Observer subscriptions (Awake is executed when the object is created).
        private void Awake()
        {
            _objectCollider = gameObject.GetComponent<BoxCollider>();
            _objectCollider.enabled = false;
            
            StepManager.Instance.Counter
                .Where(stepTrigger => stepTrigger == stepToIn)
                .Subscribe(_ => { gameObject.GetComponent<Animator>().Play("Beaker H2O 2 Start"); });
            
            StepManager.Instance.Counter
                .Where(stepTrigger => stepTrigger == stepToFill)
                .Subscribe(_ => { gameObject.GetComponent<Animator>().Play("Beaker H2O 2 Fill"); });
            
            StepManager.Instance.Counter
                .Where(stepTrigger => stepTrigger == stepToOut)
                .Subscribe(_ => { gameObject.GetComponent<Animator>().Play("Beaker H2O 2 End"); });
            
            StepManager.Instance.Counter
                .Where(stepTrigger => stepTrigger == stepToEnable)
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
        private void AnimationTriggerB() { h2OLiquid.GetComponent<G2H2OLiquid>().EmptyBeaker1(); }
        
        // ReSharper disable once UnusedMember.Local  ( Jetbrains Rider Notation ).
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
