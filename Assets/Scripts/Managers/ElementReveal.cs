
// Copyright (c) 2022 MauricioRB06 <https://github.com/MauricioRB06>
// Collaborator: ShiroKame <https://github.com/ShiroKame>
// Supervisory Teacher: herrera78 <https://github.com/herrera78>
//
// The Purpose Of This Script Is:
//
// Set the behavior of objects that are enabled or disabled only once.
//
// Documentation and References:
//
// UniRx: https://github.com/neuecc/UniRx
// UniRx: https://programmerclick.com/article/74731581924/ [Spanish] 
//
// Last Update: 23.06.2022 By MauricioRB06

using UniRx;
using UnityEngine;

namespace Managers
{
    public class ElementReveal : MonoBehaviour
    {
        
        [Space(2)]
        [Header("Reveal Settings")]
        [Space(5)]
        [Tooltip("Set whether the object will only be enabled.")]
        [SerializeField] private bool isOnlyEnabled;
        [Tooltip("Set whether the object will be disabled only.")]
        [SerializeField] private bool isOnlyDisable;
        [Space(15)]
        
        [Header("Enable Settings")]
        [Space(5)]
        [Tooltip("Set the step where the object will be enabled.")]
        [SerializeField] private int enableStep = 1;
        [Tooltip("Set the step where the object will be disabled.")]
        [SerializeField] private int disableStep = 2;
        
        // Observer subscriptions (Awake is executed when the object is created).
        private void Awake()
        {
            StepManager.instance.Counter
                .Where(stepTrigger => stepTrigger == enableStep && !isOnlyDisable)
                .Subscribe(_ => { gameObject.SetActive(true); });
            
            StepManager.instance.Counter
                .Where(stepTrigger => stepTrigger == disableStep && !isOnlyEnabled)
                .Subscribe(_ => { gameObject.SetActive(false); });
            
            if(!isOnlyDisable) gameObject.SetActive(false);
        }
        
    }
}
