
using System.Collections.Generic;
using Managers;
using UniRx;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Interactables
{
    public class ReactionButton : MonoBehaviour
    {
        [Header("Microscope Switch Sfx")][Space(5)]
        [Tooltip("Place here the microscope switch sound effect.")]
        [SerializeField] private Button buttonRef;
        [Space(15)]
        
        [FormerlySerializedAs("enable1")]
        [Space(2)]
        [Header("Enable 1")]
        [Space(5)]
        [Tooltip("Set the step where the object will be enabled.")]
        [SerializeField] private List<int> stepsToEnable;
        [FormerlySerializedAs("disable1")]
        [Tooltip("Set the step where the object will be disabled.")]
        [SerializeField] private List<int> stepsToDisable;

        // Observer subscriptions (Awake is executed when the object is created).
        private void Start()
        {
            BiologyStepManager.Instance.Counter
                .Where(stepTrigger => stepsToEnable.Contains(stepTrigger))
                .Subscribe(_ =>
                {
                    buttonRef.interactable = true;
                });
            
            BiologyStepManager.Instance.Counter
                .Where(stepTrigger => stepsToDisable.Contains(stepTrigger))
                .Subscribe(_ =>
                {
                    buttonRef.interactable = false;
                });
            
        }
    }
}
