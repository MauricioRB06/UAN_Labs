
// Copyright (c) 2022 MauricioRB06 <https://github.com/MauricioRB06>
// Collaborator: ShiroKame <https://github.com/ShiroKame>
// Supervisory Teacher: herrera78 <https://github.com/herrera78>
//
// The Purpose Of This Script Is:
//
// Set de behavior of the Digital Balance.
//
// Documentation and References:
//
// UniRx: https://github.com/neuecc/UniRx
// UniRx: https://programmerclick.com/article/74731581924/ [Spanish]
// C# Interfaces: https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/interface
//
// Last Update: 23.06.2022 By MauricioRB06

using Managers;
using UniRx;
using UnityEngine;

namespace Chemistry.G2
{
    // Components required for this Script to work.
    [RequireComponent(typeof(Animator))]
    
    public class G2DigitalBalance : MonoBehaviour
    {
        
        [Space(2)]
        [Header("Digital Balance Settings")]
        [Space(5)]
        [Tooltip("Set the step where the Digital Balance will move to its initial position.")]
        [SerializeField] private int stepToIn;
        [Tooltip("Set the step where the Digital Balance will move to its final position.")]
        [SerializeField] private int stepToOut;
        
        
        // Observer subscriptions (Awake is executed when the object is created)
        private void Awake()
        {
            StepManager.Instance.Counter
                .Where(stepTrigger => stepTrigger == stepToIn)
                .Subscribe(_ =>
                {
                    gameObject.GetComponent<Animator>().Play("Start Digital Balance");
                });
            
            StepManager.Instance.Counter
                .Where(stepTrigger => stepTrigger == stepToOut)
                .Subscribe(_ =>
                {
                    gameObject.GetComponent<Animator>().Play("End Digital Balance");
                });
        }
        
        // ReSharper disable once UnusedMember.Local  ( Jetbrains Rider Notation )
        // This function is called within the animator as a trigger.
        private void AnimationFinishTrigger() { StepManager.Instance.UpdateCounter(); }
        
    }
}
