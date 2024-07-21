
// Copyright (c) 2022 MauricioRB06 <https://github.com/MauricioRB06>
// Collaborator: ShiroKame <https://github.com/ShiroKame>
// Supervisory Teacher: herrera78 <https://github.com/herrera78>
//
// The Purpose Of This Script Is:
//
// Generate the behavior of the Beaker with the solution for the simulations.
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

namespace Chemistry.G4
{
    // Components required for this Script to work.
    [RequireComponent(typeof(BoxCollider))]
    [RequireComponent(typeof(Animator))]
    
    public class G4BeakerBase : MonoBehaviour, IInteractable
    {
        
        [Space(2)] [Header("Interactable Light")] [Space(5)]
        [Tooltip("Place here the light that will interact with this object, to make it glow when it is active.")]
        [SerializeField] private GameObject interactableLight;
        [Space(15)]
        
        [Header("Animation Triggers")] [Space(5)]
        [Tooltip("Define the step at which this animation will be activated.")]
        [SerializeField] private int animationStart = 1;
        [Tooltip("Define the step at which this animation will be activated.")]
        [SerializeField] private int animationPosition = 1;
        [Tooltip("Define the step at which this animation will be activated.")]
        [SerializeField] private int animationEnd = 1;
        [Space(15)]
        
        [Header("Interaction trigger")] [Space(5)]
        [Tooltip("Put in the list, the number of the step in which you want the object to be enabled to " +
                 "interact with it, remember that once clicked, the step is incremented and the object is disabled.")]
        [SerializeField] private List<int> stepTriggerList = new();
        
        // Variable initialization (Awake is executed when the object is created).
        private void Awake()
        {
            if (interactableLight is null)
            {
                Debug.LogError("The interactive light cannot be empty, please add one.");
            }
            else
            {
                gameObject.GetComponent<BoxCollider>().enabled = false;
            }
        }
        
        // Observer subscriptions (Start - Runs on the first frame of the game).
        private void Start()
        {
            StepManager.Instance.Counter
                .Where(stepTrigger => stepTrigger == animationStart)
                .Subscribe(_ =>
                {
                    gameObject.GetComponent<Animator>().Play("Beaker Base Start");
                });
            
            StepManager.Instance.Counter
                .Where(stepTrigger => stepTrigger == animationPosition)
                .Subscribe(_ =>
                {
                    gameObject.GetComponent<Animator>().Play("Beaker Base Position");
                });

            StepManager.Instance.Counter
                .Where(stepTrigger => stepTrigger == animationEnd)
                .Subscribe(_ =>
                { 
                    StepManager.Instance.SwitchCamera();
                    gameObject.GetComponent<Animator>().Play("Beaker Base End");
                });

            StepManager.Instance.Counter
                .Where(step => stepTriggerList.Contains(step))
                .Subscribe(_ =>
                {
                    gameObject.GetComponent<BoxCollider>().enabled = true;
                    interactableLight.SetActive(true);
                });
        }
        
        // ReSharper disable once UnusedMember.Local  ( Jetbrains Rider Notation ).
        // This function is called within the animator as a trigger.
        private void AnimationFinishTrigger()
        {
            StepManager.Instance.UpdateCounter();
        }
        
        // Function that is called when the object is clicked (interface implementation).
        public void Interaction()
        {
            gameObject.GetComponent<BoxCollider>().enabled = false;
            interactableLight.SetActive(false);
            StepManager.Instance.UpdateCounter();
        }
        
    }
}
