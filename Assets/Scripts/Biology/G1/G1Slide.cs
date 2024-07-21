﻿
// Copyright (c) 2022 MauricioRB06 <https://github.com/MauricioRB06>
// Supervisory Teacher: herrera78 <https://github.com/herrera78>
//
// The Purpose Of This Script Is:
//
// Sets the behavior of the slide.
//
// Documentation and References:
//
// UniRx: https://github.com/neuecc/UniRx
// C# Interfaces: https://docs.microsoft.com/en-us/dotnet/csharp/
//
// Last Update: 11.12.2022 By MauricioRB06

using System.Collections.Generic;
using Interfaces;
using Managers;
using ModelShark;
using UniRx;
using UnityEngine;

namespace Biology.G1
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(BoxCollider))]
    [RequireComponent(typeof(TooltipTrigger))]
    public class G1Slide : MonoBehaviour, IDraggable
    {
        [Space(2)]
        [Header("Step Settings")] [Space(5)]
        [SerializeField] private GameObject arrowObject;
        [Tooltip("Set the step at which the slide will be enabled.")]
        [SerializeField] private List<int> stepsToEnable = new();
        [Tooltip("Set the step at which the slide will be disable.")]
        [SerializeField] private List<int> stepToDisable;
        [Tooltip("Set the step in which the tooltip component will be disable.")]
        [SerializeField] private int stepToDisableTooltip;
        [Space(15)]
        
        private Rigidbody _rigidbody;
        private BoxCollider _boxCollider;
        private TooltipTrigger _tooltipTrigger;
        
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _boxCollider = GetComponent<BoxCollider>();
            _tooltipTrigger = GetComponent<TooltipTrigger>();
            
            arrowObject.SetActive(false);
            _rigidbody.isKinematic = true;
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
                    G1CameraManager.Instance.SwitchCamera(true);
                    arrowObject.SetActive(true);
                    _rigidbody.isKinematic = false;
                    _boxCollider.enabled = true;
                    BiologyStepManager.Instance.ChangeButtonNext(false);
                });
            
            BiologyStepManager.Instance.Counter
                .Where(stepTrigger => stepToDisable.Contains(stepTrigger))
                .Subscribe(_ =>
                {
                    arrowObject.SetActive(false);
                    _rigidbody.isKinematic = true;
                    _boxCollider.enabled = false;
                });
        }
        
        public void OnStartDrag()
        {
            _rigidbody.useGravity = false;
        }
        
        public void OnEndDrag()
        {
            _rigidbody.useGravity = true;
            _rigidbody.velocity = Vector3.zero;
        }

    }
}
