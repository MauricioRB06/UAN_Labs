
// Copyright (c) 2022 MauricioRB06 <https://github.com/MauricioRB06>
// Collaborator: ShiroKame <https://github.com/ShiroKame>
// Supervisory Teacher: herrera78 <https://github.com/herrera78>
//
// The Purpose Of This Script Is:
//
// Define the behavior that the burette valve will have.
//
// Documentation and References:
//
// UniRx: https://github.com/neuecc/UniRx
// UniRx: https://programmerclick.com/article/74731581924/ [Spanish]
// C# Interfaces: https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/interface
//
// Last Update: 23.06.2022 By MauricioRB06

using System.Collections;
using System.Collections.Generic;
using Interfaces;
using Liquid;
using Managers;
using UniRx;
using UnityEngine;

namespace Chemistry.G4
{
    // Components required for this Script to work.
    [RequireComponent(typeof(BoxCollider))]
    [RequireComponent(typeof(Animator))]
    
    public class G4BtnBurette : MonoBehaviour, IInteractable
    {
        
        [Space(2)]
        [Header("Valve Settings")]
        [Space(5)]
        [Tooltip("Set the steps in which the valve will be interactive.")]
        [SerializeField] private List<int> stepsEnable = new();
        [Tooltip("Place here the light that will interact with the valve.")]
        [SerializeField] private GameObject objectLight;
        [Tooltip("Place here the liquid generator that will be used for the simulation.")]
        [SerializeField] private GameObject liquidGenerator;
        [Tooltip("Place here the liquid that will simulate exiting the valve.")]
        [SerializeField] private GameObject liquid;
        
        // Variable to store the reference to the object's collider.
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
                .Where(stepTrigger => stepsEnable.Contains(stepTrigger))
                .Subscribe(_ =>
                {
                    objectLight.SetActive(true);
                    _objectCollider.enabled = true;
                });
        }
        
        private void LiquidOn() { liquid.GetComponent<ValveLiquid>().EnableLiquid(); }
        
        private void LiquidOff() { liquid.GetComponent<ValveLiquid>().DisableLiquid(); }

        // ReSharper disable once UnusedMember.Local
        private void AnimationCameraTrigger() { StepManager.instance.UpdateCounter(); }
        
        // A coroutine that activates the simulation of the liquid falling from the valve.
        private IEnumerator BtnBuretteAnim()
        {
            LiquidOn();
            liquidGenerator.GetComponent<LiquidGenerator>().LiquidOut();
            yield return new WaitForSeconds(2f);
            LiquidOff();
            StepManager.instance.UpdateCounter();
        }
        
        // Function that is called when the object is clicked (interface implementation).
        public void Interaction()
        {
            gameObject.GetComponent<Animator>().Play("Burette Valve");
            _objectCollider.enabled = false;
            objectLight.SetActive(false);
            StartCoroutine(BtnBuretteAnim());
        }
        
    }
}
