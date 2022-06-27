
// Copyright (c) 2022 MauricioRB06 <https://github.com/MauricioRB06>
// Collaborator: ShiroKame <https://github.com/ShiroKame>
// Supervisory Teacher: herrera78 <https://github.com/herrera78>
//
// The Purpose Of This Script Is:
//
// Set the behavior of the Volumetric Ball.
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
    public class G2VolumetricBall : MonoBehaviour, IInteractable
    {
        
        [Space(2)]
        [Header("Step Settings")]
        [Space(5)]
        [Tooltip("Set the step at which the beaker will be move to start position.")]
        [SerializeField] private int stepToIn;
        [Tooltip("Set the step at which the beaker will be move to interact position.")]
        [SerializeField] private int stepToPosition;
        
        [Header("Volumetric Ball Settings")]
        [Space(5)]
        [Tooltip("Place here the light that will interact with the Volumetric Ball.")]
        [SerializeField] private GameObject objectLight;
        
        // This variable will be used to store the reference to the object's collider.
        private BoxCollider _objectCollider;
        
        // Observer subscriptions (Awake is executed when the object is created).
        private void Awake()
        {
            _objectCollider = gameObject.GetComponent<BoxCollider>();
            _objectCollider.enabled = false;
            
            StepManager.instance.Counter
                .Where(stepTrigger => stepTrigger == stepToIn)
                .Subscribe(_ => { gameObject.GetComponent<Animator>().Play("VolumetricBall Start"); });
            
            StepManager.instance.Counter
                .Where(stepTrigger => stepTrigger == stepToPosition)
                .Subscribe(_ => { gameObject.GetComponent<Animator>().Play("VolumetricBall Position"); });
        }
        
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
