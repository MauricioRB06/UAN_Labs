
// Copyright (c) 2022 MauricioRB06 <https://github.com/MauricioRB06>
// Supervisory Teacher: herrera78 <https://github.com/herrera78>
//
// The Purpose Of This Script Is:
//
// Sets the behavior of the microscope sample positioner.
//
// Documentation and References:
//
// UniRx: https://github.com/neuecc/UniRx
// OnTriggerEnter: https://docs.unity3d.com/ScriptReference/MonoBehaviour.OnTriggerEnter.html
//
// Last Update: 11.12.2022 By MauricioRB06

using System.Collections.Generic;
using Biology.Microscope;
using Interfaces;
using Managers;
using UniRx;
using UnityEngine;

namespace Biology.G1
{
    [RequireComponent(typeof(BoxCollider))]
    public class G1MicroscopeTrigger : MonoBehaviour, IInteractablePart
    {
        
        [Space(2)]
        [Header("Step Settings")] [Space(5)]
        [Tooltip("Place here the part to be blocked to activate the sample.")]
        [SerializeField] private RotatingPart partToLockA;
        [Tooltip("Place here the part to be locked to deactivate its Outline component.")]
        [SerializeField] private MicroscopeOutlinePart partToLockB;
        [Tooltip("Place here the reference position where the sample will be placed.")]
        [SerializeField] private Transform partToParent;
        [Tooltip("Place here the reference to the arrow object.")]
        [SerializeField] private GameObject arrowObject;
        [Tooltip("Set the step at which the Trigger will be enabled.")]
        [SerializeField] private List<int> stepsToEnable = new();
        [Tooltip("Set the step at which the Trigger will be disable.")]
        [SerializeField] private List<int> stepToDisable;
        [Tooltip(".")] 
        [SerializeField] private List<int> stepsToEnableArrow;

        private BoxCollider _boxCollider;
        
        private void Awake()
        {
            _boxCollider = GetComponent<BoxCollider>();
            _boxCollider.isTrigger = true;
            arrowObject.SetActive(false);
        }
        
        private void Start()
        {
            BiologyStepManager.Instance.Counter
                .Where(stepTrigger => stepsToEnable.Contains(stepTrigger))
                .Subscribe(_ =>
                {
                    _boxCollider.enabled = true;
                });
            
            BiologyStepManager.Instance.Counter
                .Where(stepTrigger => stepToDisable.Contains(stepTrigger))
                .Subscribe(_ =>
                {
                    arrowObject.SetActive(false);
                    _boxCollider.enabled = false;
                });
            
            BiologyStepManager.Instance.Counter
                .Where(stepTrigger => stepsToEnableArrow.Contains(stepTrigger))
                .Subscribe(_ => { arrowObject.SetActive(true); });
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.GetComponent<G1Sample>() != null && other.GetComponent<G1Sample>().SampleID == 1
                                                                 && BiologyStepManager.Instance.Counter.Value == 4)
            {
                BiologyStepManager.Instance.UpdateCounter();
                other.transform.position = transform.position;
                other.transform.SetParent(partToParent);
                other.transform.GetComponent<G1Sample>().SampleReady();
            }
            
            if (other.transform.GetComponent<G1Sample>() != null && other.GetComponent<G1Sample>().SampleID == 2
                                                                 && BiologyStepManager.Instance.Counter.Value == 8)
            {
                BiologyStepManager.Instance.UpdateCounter();
                other.transform.position = transform.position;
                other.transform.SetParent(partToParent);
                other.transform.GetComponent<G1Sample>().SampleReady();
            }
            
            if (other.transform.GetComponent<G1Sample>() != null && other.GetComponent<G1Sample>().SampleID == 3
                                                                 && BiologyStepManager.Instance.Counter.Value == 12)
            {
                BiologyStepManager.Instance.UpdateCounter();
                other.transform.position = transform.position;
                other.transform.SetParent(partToParent);
                other.transform.GetComponent<G1Sample>().SampleReady();
            }
        }
        
        public void InteractivePart(float movementRange)
        {
            if (movementRange < -12.5f)
            {
                partToLockA.RotationLock(false);
                partToLockB.LockPart();
                BiologyStepManager.Instance.UpdateCounter();
            }
        }

        public void InteractivePart(float movementRange, bool enableX100Lens) { }

        public void CheckPositionToReset()
        {
        }
    }
}
