
// Copyright (c) 2022 MauricioRB06 <https://github.com/MauricioRB06>
// Collaborator: ShiroKame <https://github.com/ShiroKame>
// Supervisory Teacher: herrera78 <https://github.com/herrera78>
//
// The Purpose Of This Script Is:
//
// Establish the behavior of the beaker containing chemicals.
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
using UnityEngine.Serialization;

namespace Chemistry.G3
{
    // Components required for this Script to work.
    [RequireComponent(typeof(Animator))]
    
    public class G3BeakerHno3 : MonoBehaviour
    {
        
        [Space(2)]
        [Header("Step Settings")]
        [Space(5)]
        [Tooltip("Set the step at which the beaker will move to its initial position.")]
        [SerializeField] private int stepStart;
        [FormerlySerializedAs("stepInteract")]
        [Tooltip("Set the step at which the beaker will move to its final position.")]
        [SerializeField] private int stepEnd;
        [SerializeField] private int stepNext;
        
        // Observer subscriptions (Start - Runs on the first frame of the game).
        private void Start()
        {
            StepManager.instance.Counter
                .Where(stepTrigger => stepTrigger == stepStart)
                .Subscribe(_ => { gameObject.GetComponent<Animator>().Play("BeakerHNO3 Start"); });
            
            StepManager.instance.Counter
                .Where(stepTrigger => stepTrigger == stepEnd)
                .Subscribe(_ => { gameObject.GetComponent<Animator>().Play("BeakerHNO3 End");});
            
            StepManager.instance.Counter
                .Where(stepTrigger => stepTrigger == stepNext)
                .Subscribe(_ => { StepManager.instance.ChangeButtonNext(true);});
        }
        
        // This functions are called within the animator as trigger.
        // ReSharper disable once UnusedMember.Local  ( Jetbrains Rider Notation ).
        private void AnimationTrigger() { StepManager.instance.UpdateCounter(); }
        
    }
}
