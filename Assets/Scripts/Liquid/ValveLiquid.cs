
// Copyright (c) 2022 MauricioRB06 <https://github.com/MauricioRB06>
// Collaborator: ShiroKame <https://github.com/ShiroKame>
// Supervisory Teacher: herrera78 <https://github.com/herrera78>
//
// The Purpose Of This Script Is:
//
// Define the behavior of the liquid that simulates exiting the burette valve.
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
    public class ValveLiquid : MonoBehaviour
    {
        
        [Space(2)]
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
            StepManager.Instance.Counter
                .Where(stepTrigger => stepTrigger > stepChangeColor)
                .Subscribe(_ => ChangeColor());
            
            gameObject.SetActive(false);
        }

        public void EnableLiquid() { gameObject.SetActive(true); }

        public void DisableLiquid() { gameObject.SetActive(false); }
        
        // Function that changes the color based on the step in the program.
        private void ChangeColor()
        {
            gameObject.GetComponent<Renderer>().material.color =
                StepManager.Instance.Counter.Value < stepChangeColor ? liquidColor1 : liquidColor2;
        }
        
    }
}
