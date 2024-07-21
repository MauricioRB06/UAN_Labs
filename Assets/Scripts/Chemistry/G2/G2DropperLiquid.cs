
// Copyright (c) 2022 MauricioRB06 <https://github.com/MauricioRB06>
// Collaborator: ShiroKame <https://github.com/ShiroKame>
// Supervisory Teacher: herrera78 <https://github.com/herrera78>
//
// The Purpose Of This Script Is:
//
// Set the behavior of the Dropper Bottle Liquid.
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

namespace Chemistry.G2
{
    public class G2DropperLiquid : MonoBehaviour
    {
        [Space(2)]
        [Header("Step Settings")]
        [Space(5)]
        [Tooltip("Set the step at which the beaker will be fill.")]
        [SerializeField] private int stepToFill;
        [Tooltip("Set the step at which the beaker will be empty.")]
        [SerializeField] private List<int> stepsToEmpty = new ();
        
        // Observer subscriptions (Awake is executed when the object is created).
        private void Awake()
        {
            StepManager.Instance.Counter
                .Where(stepTrigger => stepTrigger == stepToFill)
                .Subscribe(_ => { gameObject.GetComponent<Animator>().Play("Dropper Liquid Fill"); });
            
            StepManager.Instance.Counter
                .Where(stepTrigger => stepsToEmpty.Contains(stepTrigger))
                .Subscribe(_ =>
                {
                    switch (StepManager.Instance.Counter.Value)
                    {
                        case 42: gameObject.GetComponent<Animator>().Play("Dropper Liquid Empty 1");
                            break;
                        case 44: gameObject.GetComponent<Animator>().Play("Dropper Liquid Empty 2");
                            break;
                        case 46: gameObject.GetComponent<Animator>().Play("Dropper Liquid Empty 3");
                            break;
                        case 48: gameObject.GetComponent<Animator>().Play("Dropper Liquid Empty 4");
                            break;
                        case 50: gameObject.GetComponent<Animator>().Play("Dropper Liquid Empty 5");
                            break;
                    }
                });
        }
        
        // ReSharper disable once UnusedMember.Local  ( Jetbrains Rider Notation )
        // This function is called within the animator as a trigger.
        private void AnimationFinishTrigger() { StepManager.Instance.UpdateCounter(); }
        
    }
}
