
// Copyright (c) 2022 MauricioRB06 <https://github.com/MauricioRB06>
// Supervisory Teacher: herrera78 <https://github.com/herrera78>
//
// The Purpose Of This Script Is:
//
// It establishes the behavior of the base where the samples are placed.
//
// Documentation and References:
//
// UniRx: https://github.com/neuecc/UniRx
//
// Last Update: 11.12.2022 By MauricioRB06

using System.Collections.Generic;
using Managers;
using UniRx;
using UnityEngine;

namespace Biology.G1
{
    
    // Component required for this script.
    [RequireComponent(typeof(BoxCollider))]
    public class G1ObjectTrigger : MonoBehaviour
    {
        
        [Space(2)]
        [Header("Step Settings")] [Space(5)]
        [SerializeField] private GameObject arrowObject;
        [Tooltip("Set the step at which the Object Trigger will be enabled.")]
        [SerializeField] private List<int> stepsToEnable = new();
        [Tooltip("Set the step at which the Object Trigger will be disable.")]
        [SerializeField] private List<int> stepToDisable;
        [Tooltip("Set the step in which the tooltip component will be disable.")]
        
        private BoxCollider _boxCollider;
        
        private void Awake()
        {
            _boxCollider = GetComponent<BoxCollider>();
            arrowObject.SetActive(false);
            _boxCollider.enabled = false;
            _boxCollider.isTrigger = true;
        }
        
        private void Start()
        {
            BiologyStepManager.Instance.Counter
                .Where(stepTrigger => stepsToEnable.Contains(stepTrigger))
                .Subscribe(_ =>
                {
                    arrowObject.SetActive(true);
                    _boxCollider.enabled = true;
                });
            
            BiologyStepManager.Instance.Counter
                .Where(stepTrigger => stepToDisable.Contains(stepTrigger))
                .Subscribe(_ =>
                {
                    arrowObject.SetActive(false);
                    _boxCollider.enabled = false;
                });
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.GetComponent<BiologyObject>() != null &&
                other.GetComponent<BiologyObject>().GetObjectType() == BiologyObjectType.SlideA &&
                BiologyStepManager.Instance.Counter.Value == 2 || BiologyStepManager.Instance.Counter.Value == 7
                                                               || BiologyStepManager.Instance.Counter.Value == 12)
            {
                BiologyStepManager.Instance.UpdateCounter();
                other.transform.position = transform.position;
            }
            
            if (other.transform.GetComponent<BiologyObject>() != null &&
                other.GetComponent<BiologyObject>().GetObjectType() == BiologyObjectType.Yarn &&
                BiologyStepManager.Instance.Counter.Value == 4)
            {
                BiologyStepManager.Instance.UpdateCounter();
                Transform transformYarn;
                (transformYarn = other.transform).SetParent(null);
                transformYarn.position = transform.position;
            }
            
            if (other.transform.GetComponent<BiologyObject>() != null &&
                other.GetComponent<BiologyObject>().GetObjectType() == BiologyObjectType.SlideB &&(
                BiologyStepManager.Instance.Counter.Value == 5 || BiologyStepManager.Instance.Counter.Value == 10 
                                                               || BiologyStepManager.Instance.Counter.Value == 18))
            {
                BiologyStepManager.Instance.UpdateCounter();
                other.transform.position = transform.position + new Vector3(0, 0.0025f, 0.0f);
            }
            
            if (other.transform.GetComponent<BiologyObject>() != null &&
                other.GetComponent<BiologyObject>().GetObjectType() == BiologyObjectType.PipettePasteur &&
                (BiologyStepManager.Instance.Counter.Value == 9 || BiologyStepManager.Instance.Counter.Value == 14))
            { BiologyStepManager.Instance.UpdateCounter(); }
            
            if (other.transform.GetComponent<BiologyObject>() != null &&
                other.GetComponent<BiologyObject>().GetObjectType() == BiologyObjectType.OnionB &&
                BiologyStepManager.Instance.Counter.Value == 17)
            {
                BiologyStepManager.Instance.UpdateCounter();
                Transform transformOnion;
                (transformOnion = other.transform).SetParent(null);
                transformOnion.position = transform.position;
            }
        }
        
    }
}
