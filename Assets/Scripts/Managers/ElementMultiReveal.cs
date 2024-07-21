
// Copyright (c) 2022 MauricioRB06 <https://github.com/MauricioRB06>
// Collaborator: ShiroKame <https://github.com/ShiroKame>
// Supervisory Teacher: herrera78 <https://github.com/herrera78>
//
// The Purpose Of This Script Is:
//
// Sets the behavior so that objects can be enabled and disabled.
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
    public class ElementMultiReveal : MonoBehaviour
    {
        
        [Space(2)]
        [Header("Enable 1")]
        [Space(5)]
        [Tooltip("Set the step where the object will be enabled.")]
        [SerializeField] private int enable1 = 1;
        [Tooltip("Set the step where the object will be disabled.")]
        [SerializeField] private int disable1 = 2;
        [Space(15)]
        
        [Header("Enable 2")]
        [Space(5)]
        [Tooltip("Set the step where the object will be enabled.")]
        [SerializeField] private int enable2 = 1;
        [Tooltip("Set the step where the object will be disabled.")]
        [SerializeField] private int disable2 = 2;
        
        // Observer subscriptions (Awake is executed when the object is created).
        private void Awake()
        {
            StepManager.Instance.Counter
                .Where(stepTrigger => stepTrigger == enable1)
                .Subscribe(_ => { gameObject.SetActive(true); }); 
            
            StepManager.Instance.Counter
                .Where(stepTrigger => stepTrigger == disable1)
                .Subscribe(_ => { gameObject.SetActive(false); });
            
            StepManager.Instance.Counter
                .Where(stepTrigger => stepTrigger == enable2)
                .Subscribe(_ => { gameObject.SetActive(true); }); 
            
            StepManager.Instance.Counter
                .Where(stepTrigger => stepTrigger == disable2)
                .Subscribe(_ => { gameObject.SetActive(false); }); 
            
            gameObject.SetActive(false);
        }
        
    }
}
