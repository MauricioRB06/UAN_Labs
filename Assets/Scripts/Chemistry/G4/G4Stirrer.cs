
// Copyright (c) 2022 MauricioRB06 <https://github.com/MauricioRB06>
// Collaborator: ShiroKame <https://github.com/ShiroKame>
// Supervisory Teacher: herrera78 <https://github.com/herrera78>
//
// The Purpose Of This Script Is:
//
// Define the behavior that the magnetic stirrer will have.
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

namespace Chemistry.G4
{
    // Components required for this Script to work.
    [RequireComponent(typeof(Animator))]
    
    public class G4Stirrer : MonoBehaviour
    {
        
        [Space(2)]
        [Header("Stirrer Settings")]
        [Space(5)]
        [Tooltip("Set the step where the Stirrer will move to its initial position.")]
        [SerializeField] private int stepToIn;
        
        // Observer subscriptions (Awake is executed when the object is created)
        private void Awake()
        {
            StepManager.Instance.Counter
                .Where(stepTrigger => stepTrigger == stepToIn)
                .Subscribe(_ =>
                {
                    gameObject.GetComponent<Animator>().Play("Stirrer Start");
                });
        }
        
        // ReSharper disable once UnusedMember.Local  ( Jetbrains Rider Notation )
        // This function is called within the animator as a trigger.
        private void AnimationFinishTrigger()
        {
            StepManager.Instance.UpdateCounter();
        }

    }
}
