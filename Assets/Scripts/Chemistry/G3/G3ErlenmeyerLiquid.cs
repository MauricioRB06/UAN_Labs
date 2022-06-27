
// Copyright (c) 2022 MauricioRB06 <https://github.com/MauricioRB06>
// Collaborator: ShiroKame <https://github.com/ShiroKame>
// Supervisory Teacher: herrera78 <https://github.com/herrera78>
//
// The Purpose Of This Script Is:
//
// Set the behavior of the Erlenmeyer Liquid.
//
// Documentation and References:
//
// UniRx: https://github.com/neuecc/UniRx
// UniRx: https://programmerclick.com/article/74731581924/ [Spanish]
//
// Last Update: 23.06.2022 By MauricioRB06

using Managers;
using UniRx;
using UnityEngine;

namespace Chemistry.G3
{
    // Components required for this Script to work.
    [RequireComponent(typeof(Animator))]
    
    public class G3ErlenmeyerLiquid : MonoBehaviour
    {
        
        [Space(2)]
        [Header("Step Settings")]
        [Space(5)]
        [Tooltip("Set the step at which the beaker will move to its initial position.")]
        [SerializeField] private int stepStart;
        [Space(15)]
        [Header("Color Settings")]
        [Space(5)]
        [Tooltip("Set the step where the color change will be performed.")]
        [SerializeField] private int stepChangeColor;
        [Tooltip("Set the initial color of the liquid.")]
        [SerializeField] private Color liquidColor1;
        [Tooltip("Set the new color of the liquid.")]
        [SerializeField] private Color liquidColor2;
        
        // Observer subscriptions (Awake is executed when the object is created).
        private void Awake()
        {
            StepManager.instance.Counter
                .Where(stepTrigger => stepTrigger > stepChangeColor)
                .Subscribe(_ => ChangeColor());
        }
        
        // Observer subscriptions (Start - Runs on the first frame of the game).
        private void Start()
        {
            StepManager.instance.Counter
                .Where(stepTrigger => stepTrigger == stepStart)
                .Subscribe(_ => { gameObject.GetComponent<Animator>().Play("Erlenmeyer Liquid Start"); });
        }
        
        // Function that changes the color based on the step in the program.
        private void ChangeColor()
        {
            gameObject.GetComponent<Renderer>().material.color =
                StepManager.instance.Counter.Value < stepChangeColor ? liquidColor1 : liquidColor2;
        }
        
        // Function that triggers the liquid filling animation.
        public void AnimationFillTrigger()
        {
            gameObject.GetComponent<Animator>().Play("Erlenmeyer Liquid Fill");
        }
        
    }
}
