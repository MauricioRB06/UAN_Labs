using System.Collections.Generic;
using Interfaces;
using LiquidVolumeFX;
using Managers;
using ModelShark;
using UniRx;
using UnityEngine;

namespace Biology.G1
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(BoxCollider))]
    [RequireComponent(typeof(TooltipTrigger))]
    public class G1PipettePasteurB : MonoBehaviour, IDraggable
    {
        
        [Space(2)]
        [Header("Step Settings")] [Space(5)]
        [SerializeField] private GameObject arrowObject;
        [SerializeField] private LiquidVolume liquidVolume;
        [Tooltip("Set the step at which the Pipette will be enabled.")]
        [SerializeField] private List<int> stepsToEnable = new();
        [Tooltip("Set the step at which the Pipette will be disable.")]
        [SerializeField] private List<int> stepToDisable;
        [Tooltip("Set the step in which the tooltip component will be disable.")]
        [SerializeField] private int stepToDisableTooltip;
        [Space(15)]
        
        private Rigidbody _rigidbody;
        private BoxCollider _boxCollider;
        private TooltipTrigger _tooltipTrigger;
        private bool _canFill;
        private bool _canEmpty;
        
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _boxCollider = GetComponent<BoxCollider>();
            _tooltipTrigger = GetComponent<TooltipTrigger>();

            arrowObject.SetActive(false);
            _rigidbody.isKinematic = true;
            _boxCollider.enabled = false;
            liquidVolume.level = 0;
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
                    _rigidbody.isKinematic = true;
                    _boxCollider.enabled = false;
                });
            
            BiologyStepManager.instance.Counter
                .Where(stepTrigger => stepTrigger == 10)
                .Subscribe(_ =>
                {
                    arrowObject.SetActive(false);
                    Destroy(transform.GetComponent<BiologyObject>());
                });
            
            BiologyStepManager.instance.Counter
                .Where(stepTrigger => stepTrigger == 15)
                .Subscribe(_ => { arrowObject.SetActive(false);; });
        }
        
        private void Update()
        {
            if (_canFill)
            {
                liquidVolume.level = Mathf.Lerp(liquidVolume.level, 0.5f, 0.02f);
                if (!(liquidVolume.level > 0.5f)) return;
                liquidVolume.level = 0.5f;
                _canFill = false;
            }
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
        
        private void OnCollisionEnter(Collision other)
        {
            if (other.transform.GetComponent<BiologyObject>() != null &&
                other.transform.GetComponent<BiologyObject>().GetObjectType() == BiologyObjectType.Lactofenol &&
                BiologyStepManager.instance.Counter.Value == 13)
            {
                BiologyStepManager.instance.UpdateCounter();
                _canFill = true;
            }
        }
        
    }
}
