
// Copyright (c) 2022 MauricioRB06 <https://github.com/MauricioRB06>
// Supervisory Teacher: herrera78 <https://github.com/herrera78>
//
// The Purpose Of This Script Is:
//
// It establishes the behavior of the yarn sample.
//
// Documentation and References:
//
// UniRx: https://github.com/neuecc/UniRx
//
// Last Update: 11.12.2022 By MauricioRB06

using System.Collections.Generic;
using Managers;
using ModelShark;
using UniRx;
using UnityEngine;

namespace Biology.G1
{
    [RequireComponent(typeof(BoxCollider))]
    [RequireComponent(typeof(TooltipTrigger))]
    public class G1Yarn : MonoBehaviour
    {
        
        [Space(2)]
        [Header("Step Settings")] [Space(5)]
        [SerializeField] private GameObject arrowObject;
        [Tooltip("Set the step at which the slide will be enabled.")]
        [SerializeField] private List<int> stepsToEnable = new();
        [Tooltip("Set the step at which the yarn will be disable.")]
        [SerializeField] private List<int> stepToDisable;
        [Tooltip("Set the step at which the yarn will be disable.")]
        [SerializeField] private List<int> stepToDisableArrow;
        [Tooltip("Set the step in which the tooltip component will be disable.")]
        [SerializeField] private int stepToDisableTooltip;
        [Space(15)]
        
        private BoxCollider _boxCollider;
        private TooltipTrigger _tooltipTrigger;
        
        private void Awake()
        {
            _boxCollider = GetComponent<BoxCollider>();
            _tooltipTrigger = GetComponent<TooltipTrigger>();
            
            arrowObject.SetActive(false);
            _boxCollider.enabled = false;
        }
        
        private void Start()
        {
            BiologyStepManager.Instance.Counter
                .Where(stepTrigger => stepTrigger == stepToDisableTooltip)
                .Subscribe(_ => { _tooltipTrigger.enabled = false; });
            
            BiologyStepManager.Instance.Counter
                .Where(stepTrigger => stepsToEnable.Contains(stepTrigger))
                .Subscribe(_ =>
                {
                    arrowObject.SetActive(true);
                    _boxCollider.enabled = true;
                });
            
            BiologyStepManager.Instance.Counter
                .Where(stepTrigger => stepToDisableArrow.Contains(stepTrigger))
                .Subscribe(_ =>
                {
                    arrowObject.SetActive(false);
                });
            
            BiologyStepManager.Instance.Counter
                .Where(stepTrigger => stepToDisable.Contains(stepTrigger))
                .Subscribe(_ =>
                {
                    _boxCollider.enabled = false;
                });
        }
        
    }
}