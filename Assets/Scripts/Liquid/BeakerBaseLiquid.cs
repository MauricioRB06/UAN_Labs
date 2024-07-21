
// Copyright (c) 2022 MauricioRB06 <https://github.com/MauricioRB06>
// Collaborator: ShiroKame <https://github.com/ShiroKame>
// Supervisory Teacher: herrera78 <https://github.com/herrera78>
//
// The Purpose Of This Script Is:
//
// Define the behavior of the liquid inside the beakers.
//
// Documentation and References:
//
// UniRx: https://github.com/neuecc/UniRx
// UniRx: https://programmerclick.com/article/74731581924/ [Spanish]
//
// Last Update: 23.06.2022 By MauricioRB06

using System.Collections.Generic;
using Managers;
using UnityEngine;
using UniRx;

namespace Liquid
{
    // Components required for this Script to work.
    [RequireComponent(typeof(Animator))]
    
    public class BeakerBaseLiquid : MonoBehaviour
    {
        
        [Space(2)]
        [Header("Beaker ID")]
        [Space(5)]
        [Tooltip("Sets the identifier number for the Beaker, this will be used to identify" +
                 " the animations corresponding to the beaker.")]
        [Range(1f,10f)]
        [SerializeField] private int beakerNumber = 1;
        
        // +0.04 Value Changed on Animation by Default.
        [SerializeField] private List<int> stepsToChangeLiquidScale = new();
        
        // Observer subscriptions (Awake is executed when the object is created).
        private void Awake()
        {
            StepManager.Instance.Counter
                .Where(stepTrigger => stepsToChangeLiquidScale.Contains(stepTrigger))
                .Subscribe(ChangeLiquidScale);
        }
        
        // This function uses the identifier to search for the animation corresponding
        // to the beaker and the step in which the program is located.
        private void ChangeLiquidScale(int animationNumber)
        {
            gameObject.GetComponent<Animator>().Play("Beaker Base " +
                                                     beakerNumber + " Liquid Scale " + animationNumber);
        }
        
    }
}
