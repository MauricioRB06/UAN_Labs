
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

using System.Collections.Generic;
using Managers;
using UniRx;
using UnityEngine;

namespace Chemistry.G3
{
    // Components required for this Script to work.
    [RequireComponent(typeof(Animator))]
    
    public class G3BuretteLiquid : MonoBehaviour
    {
        
        [Space(2)]
        [Header("Burette Liquid Settings")]
        [Space(5)]
        [Tooltip("Sets the color of the liquid.")]
        [SerializeField] private Color liquidColor;
        [Tooltip("Sets the step at which the liquid will perform the filling animation.")]
        [SerializeField] private List<int> stepsToScale = new();
        
        // Observer subscriptions (Awake is executed when the object is created).
        private void Awake()
        {
            gameObject.GetComponent<Renderer>().material.color = liquidColor;
            
            StepManager.instance.Counter
                .Where(stepTrigger => stepsToScale.Contains(stepTrigger))
                .Subscribe(_ =>
                {
                    switch (StepManager.instance.Counter.Value)
                    {
                        case 20: gameObject.GetComponent<Animator>().Play("Burette Liquid Step 20");
                            break;
                        case 22: gameObject.GetComponent<Animator>().Play("Burette Liquid Step 22");
                            break;
                        case 24: gameObject.GetComponent<Animator>().Play("Burette Liquid Step 24");
                            break;
                        case 26: gameObject.GetComponent<Animator>().Play("Burette Liquid Step 26");
                            break;
                        case 28: gameObject.GetComponent<Animator>().Play("Burette Liquid Step 28");
                            break;
                        case 30: gameObject.GetComponent<Animator>().Play("Burette Liquid Step 30");
                            break;
                        case 32: gameObject.GetComponent<Animator>().Play("Burette Liquid Step 32");
                            break;
                        case 34: gameObject.GetComponent<Animator>().Play("Burette Liquid Step 34");
                            break;
                        case 36: gameObject.GetComponent<Animator>().Play("Burette Liquid Step 36");
                            break;
                        case 38: gameObject.GetComponent<Animator>().Play("Burette Liquid Step 38");
                            break;
                        case 40: gameObject.GetComponent<Animator>().Play("Burette Liquid Step 40");
                            break;
                        case 42: gameObject.GetComponent<Animator>().Play("Burette Liquid Step 42");
                            break;
                    }
                });
        }
        
        // Sets the initial state of the liquid when the object is activated.
        private void OnEnable() { gameObject.GetComponent<Animator>().Play("Set Burette"); }
        
        // Plays the liquid filling animation.
        // ReSharper disable once UnusedMember.Global
        public void FillBurette() { gameObject.GetComponent<Animator>().Play("Fill Burette"); }
        
    }
}
