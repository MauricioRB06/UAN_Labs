
// Copyright (c) 2022 MauricioRB06 <https://github.com/MauricioRB06>
// Collaborator: ShiroKame <https://github.com/ShiroKame>
// Supervisory Teacher: herrera78 <https://github.com/herrera78>
//
// The Purpose Of This Script Is:
//
// Define the behavior of the pH meter.
//
// Documentation and References:
//
// UniRx: https://github.com/neuecc/UniRx
// UniRx: https://programmerclick.com/article/74731581924/ [Spanish]
// TMPro: https://docs.unity3d.com/Packages/com.unity.textmeshpro@3.0/manual/index.html
//
// Last Update: 23.06.2022 By MauricioRB06

using System.Collections.Generic;
using Interfaces;
using Managers;
using TMPro;
using UnityEngine;
using UniRx;

namespace Chemistry.G4
{
    // Components required for this Script to work.
    [RequireComponent(typeof(BoxCollider))]
    [RequireComponent(typeof(Animator))]
    
    public class G4PHmeter : MonoBehaviour,IInteractable
    {
        [Space(2)]
        [Header("pHmeter Settings")]
        [Space(5)]
        [Tooltip("Add here the light that will interact with the pHmeter.")]
        [SerializeField] private GameObject objectLight;
        [Tooltip("Add here the text to be displayed on the pHmeter.")]
        [SerializeField] private GameObject textPHmeter;
        [Space(15)]
        
        [Header("Step Settings")]
        [Space(5)]
        [Tooltip("Define the step where the pHmeter will be placed in the initial position.")]
        [SerializeField] private List<int> stepsToStart = new();
        [Tooltip("Define the steps where the pHmeter will be positioned inside the beaker.")]
        [SerializeField] private List<int> stepsToPosition = new();
        [Tooltip("Define the steps where the pHmeter will return to the initial position.")]
        [SerializeField] private List<int> stepsToReturn = new();
        [Tooltip("Define the steps where the pHmeter will be interacted with.")]
        [SerializeField] private List<int> stepsToEnable = new();
        [Tooltip("Define the steps where the pHmeter text will be visible.")]
        [SerializeField] private List<int> stepsEnableText = new();
        [Tooltip("Define the steps where the pHmeter text will be modified to update its value.")]
        [SerializeField] private List<int> stepsToSetText = new();
        
        // Variable to reference the object's collider.
        private BoxCollider _objectCollider;

        // Variable initialization (Awake is executed when the object is created)
        private void Awake()
        {
            _objectCollider = gameObject.GetComponent<BoxCollider>();
            _objectCollider.enabled = false;
        }
        
        // Observer subscriptions (Start - Runs on the first frame of the game)
        private void Start()
        {
            StepManager.instance.Counter
                .Where(stepTrigger => stepsToStart.Contains(stepTrigger))
                .Subscribe(_ => { gameObject.GetComponent<Animator>().Play("pHmeter Start"); });
            
            StepManager.instance.Counter
                .Where(stepTrigger => stepsToPosition.Contains(stepTrigger))
                .Subscribe(_ => { gameObject.GetComponent<Animator>().Play("pHmeter Position"); });
            
            StepManager.instance.Counter
                .Where(stepTrigger => stepsToReturn.Contains(stepTrigger))
                .Subscribe(_ =>
                {
                    gameObject.GetComponent<Animator>().Play("pHmeter Return");
                    textPHmeter.SetActive(false);
                });
            
            StepManager.instance.Counter
                .Where(stepTrigger => stepsToEnable.Contains(stepTrigger))
                .Subscribe(_ =>
                {
                    objectLight.SetActive(true);
                    _objectCollider.enabled = true;
                });
            
            StepManager.instance.Counter
                .Where(stepTrigger => stepsEnableText.Contains(stepTrigger))
                .Subscribe(_ =>
                {
                    textPHmeter.GetComponent<TextMeshPro>().SetText("----");
                    textPHmeter.SetActive(true);
                });
            
            StepManager.instance.Counter
                .Where(stepTrigger => stepsToSetText.Contains(stepTrigger))
                .Subscribe(_ =>
                {
                    switch (StepManager.instance.Counter.Value)
                    {
                        case 15: textPHmeter.GetComponent<TextMeshPro>().SetText("7,23");
                            break;
                        case 17: textPHmeter.GetComponent<TextMeshPro>().SetText("7,20");
                            break;
                        case 19: textPHmeter.GetComponent<TextMeshPro>().SetText("7,15");
                            break;
                        case 21: textPHmeter.GetComponent<TextMeshPro>().SetText("7,10");
                            break;
                        case 23: textPHmeter.GetComponent<TextMeshPro>().SetText("7,05");
                            break;
                        case 25: textPHmeter.GetComponent<TextMeshPro>().SetText("7,00");
                            break;
                        case 27: textPHmeter.GetComponent<TextMeshPro>().SetText("6,80");
                            break;
                        case 39: textPHmeter.GetComponent<TextMeshPro>().SetText("6,20");
                            break;
                        case 31: textPHmeter.GetComponent<TextMeshPro>().SetText("5,30");
                            break;
                        case 33: textPHmeter.GetComponent<TextMeshPro>().SetText("4,00");
                            break;
                        case 35: textPHmeter.GetComponent<TextMeshPro>().SetText("2,50");
                            break;
                        case 45: textPHmeter.GetComponent<TextMeshPro>().SetText("7.23");
                            break;
                        case 47: textPHmeter.GetComponent<TextMeshPro>().SetText("7.30");
                            break;
                        case 49: textPHmeter.GetComponent<TextMeshPro>().SetText("7.55");
                            break;
                        case 51: textPHmeter.GetComponent<TextMeshPro>().SetText("7,90");
                            break;
                        case 53: textPHmeter.GetComponent<TextMeshPro>().SetText("8,10");
                            break;
                        case 55: textPHmeter.GetComponent<TextMeshPro>().SetText("8,50");
                            break;
                        case 57: textPHmeter.GetComponent<TextMeshPro>().SetText("9,00");
                            break;
                        case 59: textPHmeter.GetComponent<TextMeshPro>().SetText("10,20");
                            break;
                        case 61: textPHmeter.GetComponent<TextMeshPro>().SetText("10,50");
                            break;
                        case 63: textPHmeter.GetComponent<TextMeshPro>().SetText("12,60");
                            break;
                        case 65: textPHmeter.GetComponent<TextMeshPro>().SetText("14,00");
                            break;
                    }
                });
        }
        
        // ReSharper disable once UnusedMember.Local  ( Jetbrains Rider Notation )
        // This function is called within the animator as a trigger. 
        private void AnimationFinishTrigger()
        {
            StepManager.instance.UpdateCounter();
        }
        
        // Function that is called when the object is clicked (interface implementation)
        public void Interaction()
        {
            objectLight.SetActive(false);
            _objectCollider.enabled = false;
            StepManager.instance.UpdateCounter();
        }
        
    }
}
