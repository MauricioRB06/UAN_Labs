
// Copyright (c) 2022 MauricioRB06 <https://github.com/MauricioRB06>
// Supervisory Teacher: herrera78 <https://github.com/herrera78>
//
// The Purpose Of This Script Is:
//
// Establishes the behavior of the laboratory sample.
//
// Documentation and References:
//
// UniRx: https://github.com/neuecc/UniRx
// C# Interfaces: https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/interfaces/
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
    public class G1Sample : MonoBehaviour, IDraggable
    {
        
        [SerializeField] private int sampleID;
        [SerializeField] private GameObject sampleImagesPack;
        [Space(2)]
        [Header("Step Settings")] [Space(5)]
        [SerializeField] private GameObject arrowObject;
        [Tooltip("Set the step at which the sample will be enabled.")]
        [SerializeField] private List<int> stepsToEnable = new();
        [Tooltip("Set the step at which the sample will be disable.")]
        [SerializeField] private List<int> stepToDisable;
        [Tooltip("Set the step in which the tooltip component will be disable.")]
        [SerializeField] private int stepToDisableTooltip;

                
        private Rigidbody _rigidbody;
        private BoxCollider _boxCollider;
        private TooltipTrigger _tooltipTrigger;
        public int SampleID => sampleID;
        
        private void Awake()
        {
            sampleImagesPack.SetActive(false);
            _rigidbody = GetComponent<Rigidbody>();
            _boxCollider = GetComponent<BoxCollider>();
            _tooltipTrigger = GetComponent<TooltipTrigger>();

            arrowObject.SetActive(false);
            _rigidbody.isKinematic = true;
            _boxCollider.enabled = false;
        }
        
        private void Start()
        {
            BiologyStepManager.instance.Counter
                .Where(stepTrigger => stepTrigger == stepToDisableTooltip)
                .Subscribe(_ => { _tooltipTrigger.enabled = false; });
            
            BiologyStepManager.instance.Counter
                .Where(stepTrigger => stepsToEnable.Contains(stepTrigger))
                .Subscribe(_ =>
                {
                    arrowObject.SetActive(true);
                    _rigidbody.isKinematic = false;
                    _boxCollider.enabled = true;
                });
            
            BiologyStepManager.instance.Counter
                .Where(stepTrigger => stepToDisable.Contains(stepTrigger))
                .Subscribe(_ =>
                {
                    arrowObject.SetActive(false);
                    _rigidbody.isKinematic = true;
                    _boxCollider.enabled = false;
                });
        }
        
        // Interact Interface Implementation.
        public void OnStartDrag() { _rigidbody.useGravity = false; }
        public void OnEndDrag() { _rigidbody.useGravity = true; _rigidbody.velocity = Vector3.zero; }
        
        public void SampleReady() { sampleImagesPack.SetActive(true); }
        public void SampleOff() { sampleImagesPack.SetActive(false); }
        
    }
}
