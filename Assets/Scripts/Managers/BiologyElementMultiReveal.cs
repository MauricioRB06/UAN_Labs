
// Copyright (c) 2022 MauricioRB06 <https://github.com/MauricioRB06>
// Supervisory Teacher: herrera78 <https://github.com/herrera78>
//
// The Purpose Of This Script Is:
//
// Sets the behavior so that objects can be enabled and disabled.
//
// Documentation and References:
//
// UniRx: https://github.com/neuecc/UniRx
//
// Last Update: 11.12.2022 By MauricioRB06

using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace Managers
{
    public class BiologyElementMultiReveal : MonoBehaviour
    {
        [Space(2)]
        [Header("Multi Reveal Step Settings")]
        [Space(5)]
        [Tooltip("Set the step where the object will be enabled.")]
        [SerializeField] private List<int> stepsToEnable;
        [Tooltip("Set the step where the object will be disabled.")]
        [SerializeField] private List<int> stepsToDisable;
        
        // Observer subscriptions (Awake is executed when the object is created).
        private void Awake()
        {
            BiologyStepManager.Instance.Counter
                .Where(stepTrigger => stepsToEnable.Contains(stepTrigger))
                .Subscribe(_ => { gameObject.SetActive(true); });
            
            BiologyStepManager.Instance.Counter
                .Where(stepTrigger => stepsToDisable.Contains(stepTrigger))
                .Subscribe(_ => { gameObject.SetActive(false); });
        }
        
        private void Start() { gameObject.SetActive(false); }
        
    }
}
