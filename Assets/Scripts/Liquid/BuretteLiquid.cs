
// Copyright (c) 2022 MauricioRB06 <https://github.com/MauricioRB06>
// Collaborator: ShiroKame <https://github.com/ShiroKame>
// Supervisory Teacher: herrera78 <https://github.com/herrera78>
//
// The Purpose Of This Script Is:
//
// Define the behavior that the liquid in the burette will have.
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

namespace Liquid
{
    // Components required for this Script to work.
    [RequireComponent(typeof(Animator))]
    
    public class BuretteLiquid : MonoBehaviour
    {
        
        [Space(2)]
        [Header("Color change")]
        [Space(5)]
        [Tooltip("Define the step where the color change will be performed.")]
        [SerializeField] private int stepChangeColor;
        [Tooltip("Define the initial color of the liquid.")]
        [SerializeField] private Color liquidColor1;
        [Tooltip("Define the new color of the liquid.")]
        [SerializeField] private Color liquidColor2;
        
        // Observer subscriptions (Awake is executed when the object is created).
        private void Awake()
        {
            StepManager.Instance.Counter
                .Where(stepTrigger => stepTrigger > stepChangeColor)
                .Subscribe(_ => ChangeColor());
        }
        
        // Sets the initial state of the liquid when the object is activated.
        private void OnEnable() { gameObject.GetComponent<Animator>().Play("Set Burette"); }
        
        // Plays the liquid filling animation.
        public void FillBurette() { gameObject.GetComponent<Animator>().Play("Fill Burette"); }
        
        // Perform a color change of the liquid in the burette.
        private void ChangeColor()
        {
            gameObject.GetComponent<Renderer>().material.color =
                StepManager.Instance.Counter.Value < stepChangeColor ? liquidColor1 : liquidColor2;
        }
        
    }
}
