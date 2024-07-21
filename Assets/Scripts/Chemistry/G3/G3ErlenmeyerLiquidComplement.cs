
// Copyright (c) 2022 MauricioRB06 <https://github.com/MauricioRB06>
// Collaborator: ShiroKame <https://github.com/ShiroKame>
// Supervisory Teacher: herrera78 <https://github.com/herrera78>
//
// The Purpose Of This Script Is:
//
// Set the filling behavior of the Erlenmeyer.
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
    // Component required for this Script to work.
    [RequireComponent(typeof(Animator))]
    
    public class G3ErlenmeyerLiquidComplement : MonoBehaviour
    {
        
        [Space(2)]
        [Header("Step Settings")]
        [Space(5)]
        [Tooltip("Set the step at which the erlenmeyer will be fill.")]
        [SerializeField] private int stepFill;
        
        // Observer subscriptions (Awake is executed when the object is created).
        private void Awake()
        {
            StepManager.Instance.Counter
                .Where(stepTrigger => stepTrigger == stepFill)
                .Subscribe(_ => { gameObject.GetComponent<Animator>().Play(
                    "Erlenmeyer Liquid Complement Fill"); });
        }
        
    }
}
