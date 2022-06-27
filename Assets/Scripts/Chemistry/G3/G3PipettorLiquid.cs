
// Copyright (c) 2022 MauricioRB06 <https://github.com/MauricioRB06>
// Collaborator: ShiroKame <https://github.com/ShiroKame>
// Supervisory Teacher: herrera78 <https://github.com/herrera78>
//
// The Purpose Of This Script Is:
//
// Set the behavior of the Pipettor Liquid.
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
    
    public class G3PipettorLiquid : MonoBehaviour
    {
        
        [Space(2)]
        [Header("Step Settings")]
        [Space(5)]
        [Tooltip("Set the step at which the Pipettor Liquid will move to its end position.")]
        [SerializeField] private int stepEnd;
        
        // Observer subscriptions (Start - Runs on the first frame of the game).
        private void Start()
        {
            StepManager.instance.Counter
                .Where(stepTrigger => stepTrigger == stepEnd)
                .Subscribe(_ => { gameObject.GetComponent<Animator>().Play("Pipettor Liquid End");});
        }
        
        // This functions are called within the animator as trigger.
        // ReSharper disable once UnusedMember.Local  ( Jetbrains Rider Notation ).
        private void AnimationFinishTrigger() { StepManager.instance.UpdateCounter(); }
        
    }
}
