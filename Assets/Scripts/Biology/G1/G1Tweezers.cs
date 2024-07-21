
// Copyright (c) 2022 MauricioRB06 <https://github.com/MauricioRB06>
// Supervisory Teacher: herrera78 <https://github.com/herrera78>
//
// The Purpose Of This Script Is:
//
// Sets the behavior of the Tweezers.
//
// Documentation and References:
//
// UniRx: https://github.com/neuecc/UniRx
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
    public class G1Tweezers : MonoBehaviour, IDraggable
    {
        
        [Space(2)]
        [Header("Tweezers Settings")][Space(5)]
        [SerializeField] private  Transform objectPosition;
        [Space(2)]
        [Header("Step Settings")]
        [Space(5)]
        [Tooltip("Set the step at which the Tweezers will be enabled.")]
        [SerializeField] private List<int> stepsToEnable = new();
        [Tooltip("Set the step at which the Tweezers will be disable.")]
        [SerializeField] private List<int> stepToDisable;
        [Tooltip("Set the step in which the tooltip component will be disable.")]
        [SerializeField] private int stepToDisableTooltip;
        [Space(15)]
        
        private Rigidbody _rigidbody;
        private BoxCollider _meshCollider;
        private TooltipTrigger _tooltipTrigger;
        
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _meshCollider = GetComponent<BoxCollider>();
            _tooltipTrigger = GetComponent<TooltipTrigger>();
            
            _rigidbody.isKinematic = true;
            _meshCollider.enabled = false;
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
                    _rigidbody.isKinematic = false;
                    _meshCollider.enabled = true;
                });
            
            BiologyStepManager.Instance.Counter
                .Where(stepTrigger => stepToDisable.Contains(stepTrigger))
                .Subscribe(_ =>
                {
                    _rigidbody.isKinematic = true;
                    _meshCollider.enabled = false;
                });
            
            BiologyStepManager.Instance.Counter
                .Where(stepTrigger => stepTrigger == 16)
                .Subscribe(_ => { transform.position = new Vector3(1.166f, 1.6f, -0.23f); });
        }
        
        public void OnStartDrag()
        {
            _rigidbody.useGravity = false;
            transform.rotation = new Quaternion(0.0f, -90.0f, -90.0f, 1.0f);
            _rigidbody.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX;
        }

        public void OnEndDrag()
        {
            _rigidbody.useGravity = true;
            _rigidbody.constraints = RigidbodyConstraints.FreezePositionX;
            _rigidbody.velocity = Vector3.zero;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.GetComponent<BiologyObject>() != null &&
                collision.transform.GetComponent<BiologyObject>().GetObjectType() == BiologyObjectType.Yarn &&
                BiologyStepManager.Instance.Counter.Value == 3)
            {
                BiologyStepManager.Instance.UpdateCounter();
                Transform transformYarn;
                (transformYarn = collision.transform).SetParent(objectPosition);
                transformYarn.position = objectPosition.position;
            }
            
            if (collision.transform.GetComponent<BiologyObject>() != null &&
                collision.transform.GetComponent<BiologyObject>().GetObjectType() == BiologyObjectType.OnionB &&
                BiologyStepManager.Instance.Counter.Value == 16)
            {
                BiologyStepManager.Instance.UpdateCounter();
                Transform transformYarn;
                (transformYarn = collision.transform).SetParent(objectPosition);
                transformYarn.position = objectPosition.position;
            }
        }

    }
}
