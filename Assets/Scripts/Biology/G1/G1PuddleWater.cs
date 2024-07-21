
// Copyright (c) 2022 MauricioRB06 <https://github.com/MauricioRB06>
// Supervisory Teacher: herrera78 <https://github.com/herrera78>
//
// The Purpose Of This Script Is:
//
// Establishes the behavior of puddle water.
//
// Documentation and References:
//
// UniRx: https://github.com/neuecc/UniRx
//
// Last Update: 11.12.2022 By MauricioRB06

using Managers;
using ModelShark;
using UniRx;
using UnityEngine;

namespace Biology.G1
{
    [RequireComponent(typeof(BoxCollider))]
    [RequireComponent(typeof(TooltipTrigger))]
    public class G1PuddleWater : MonoBehaviour
    {
        [Space(2)]
        [Header("Arrow Settings")] [Space(5)]
        [SerializeField] private GameObject arrowObject;

        private TooltipTrigger _tooltipTrigger;

        private void Awake()
        {
            _tooltipTrigger = GetComponent<TooltipTrigger>();
        }
        
        private void Start()
        {
            BiologyStepManager.Instance.Counter
                .Where(stepTrigger => stepTrigger == 9)
                .Subscribe(_ =>
                {
                    _tooltipTrigger.enabled = false;
                    arrowObject.SetActive(false);
                });
        }
        
    }
}
