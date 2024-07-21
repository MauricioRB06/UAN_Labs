
// Copyright (c) 2022 MauricioRB06 <https://github.com/MauricioRB06>
// Collaborator: ShiroKame <https://github.com/ShiroKame>
// Supervisory Teacher: herrera78 <https://github.com/herrera78>
//
// The Purpose Of This Script Is:
//
// Establishes the behavior of the chemical NaCl.
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

namespace Chemistry.G2
{
    public class G2NaCl : MonoBehaviour, IInteractable
    {
        [Space(2)]
        [Header("Step Settings")]
        [Space(5)]
        [Tooltip("Set the step at which the NaCl will be move to start position.")]
        [SerializeField] private int stepToIn;
        [Tooltip("Set the step at which the NaCl will be move to interact position.")]
        [SerializeField] private List<int> stepToInteract;
        [Tooltip("Set the step at which the NaCl will be enabled.")]
        [SerializeField] private List<int> stepsToEnable;
        [Tooltip("Set the step at which the NaCl will be move to end position.")]
        [SerializeField] private int stepToEnd;
        
        [Header("NaCl Settings")]
        [Space(5)]
        [Tooltip("Place here the light that will interact with the NaCl.")]
        [SerializeField] private GameObject objectLight;
        [Tooltip("Place the microspatula object here.")]
        [SerializeField] private GameObject microspatula;
        [Tooltip("Place the NaCl material here.")]
        [SerializeField] private GameObject naClMaterial;
        
        // This variable will be used to store the reference to the object's collider.
        private BoxCollider _objectCollider;
        
        // Observer subscriptions (Awake is executed when the object is created).
        private void Awake()
        {
            _objectCollider = gameObject.GetComponent<BoxCollider>();
            _objectCollider.enabled = false;
            
            StepManager.Instance.Counter
                .Where(stepTrigger => stepTrigger == stepToIn)
                .Subscribe(_ => { gameObject.GetComponent<Animator>().Play("NaCl Start"); });
            
            StepManager.Instance.Counter
                .Where(stepTrigger => stepToInteract.Contains(stepTrigger))
                .Subscribe(_ => { gameObject.GetComponent<Animator>().Play("NaCl Interact"); });
            
            StepManager.Instance.Counter
                .Where(stepTrigger => stepTrigger == stepToEnd)
                .Subscribe(_ => { gameObject.GetComponent<Animator>().Play("NaCl End"); });
            
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
        private void AnimationFinishTrigger() { StepManager.Instance.UpdateCounter(); }
        
        // ReSharper disable once UnusedMember.Local  ( Jetbrains Rider Notation )
        private void AnimationMicrospatula() { microspatula.GetComponent<G2Microspatula>().Interaction(); }
        
        // ReSharper disable once UnusedMember.Local  ( Jetbrains Rider Notation )
        private void AnimationNaClMaterial() { naClMaterial.GetComponent<G2NaClMat>().NaClFillMaterial(); }
        
        // Function that is called when the object is clicked (interface implementation).
        public void Interaction()
        {
            _objectCollider.enabled = false;
            objectLight.SetActive(false);
            StepManager.Instance.UpdateCounter();
        }
        
    }
}
