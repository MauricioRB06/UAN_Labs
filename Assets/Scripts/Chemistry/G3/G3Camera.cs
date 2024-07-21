
// Copyright (c) 2022 MauricioRB06 <https://github.com/MauricioRB06>
// Collaborator: ShiroKame <https://github.com/ShiroKame>
// Supervisory Teacher: herrera78 <https://github.com/herrera78>
//
// The Purpose Of This Script Is:
//
// Sets the behavior of the camera.
//
// Documentation and References:
//
// UniRx: https://github.com/neuecc/UniRx
// UniRx: [Spanish] https://programmerclick.com/article/74731581924/
//
// Last Update: 23.06.2022 By MauricioRB06

using System.Collections.Generic;
using Managers;
using UniRx;
using UnityEngine;

namespace Chemistry.G3
{
    public class G3Camera : MonoBehaviour
    {
        [Space(2)]
        [Header("Camera Settings")]
        [Space(5)]
        [Tooltip("Sets the steps in which the camera will move.")]
        [SerializeField] private List<int> stepsToMove = new();
        
        // Observer subscriptions (Awake is executed when the object is created).
        private void Awake()
        {
            StepManager.Instance.Counter
                .Where(stepTrigger => stepsToMove.Contains(stepTrigger))
                .Subscribe(_ =>
                {
                    switch (StepManager.Instance.Counter.Value)
                    {
                        case 20: gameObject.GetComponent<Animator>().Play("Camera Detail Step 20");
                            break;
                        case 22: gameObject.GetComponent<Animator>().Play("Camera Detail Step 22");
                            break;
                        case 24: gameObject.GetComponent<Animator>().Play("Camera Detail Step 24");
                            break;
                        case 26: gameObject.GetComponent<Animator>().Play("Camera Detail Step 26");
                            break;
                        case 28: gameObject.GetComponent<Animator>().Play("Camera Detail Step 28");
                            break;
                        case 30: gameObject.GetComponent<Animator>().Play("Camera Detail Step 30");
                            break;
                        case 32: gameObject.GetComponent<Animator>().Play("Camera Detail Step 32");
                            break;
                        case 34: gameObject.GetComponent<Animator>().Play("Camera Detail Step 34");
                            break;
                        case 36: gameObject.GetComponent<Animator>().Play("Camera Detail Step 36");
                            break;
                        case 38: gameObject.GetComponent<Animator>().Play("Camera Detail Step 38");
                            break;
                        case 40: gameObject.GetComponent<Animator>().Play("Camera Detail Step 40");
                            break;
                        case 42: gameObject.GetComponent<Animator>().Play("Camera Detail Step 42");
                            break;
                    }
                });
        }
        
    }
}
