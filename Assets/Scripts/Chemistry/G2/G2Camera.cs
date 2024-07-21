
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
// UniRx: https://programmerclick.com/article/74731581924/ [Spanish]
// C# Interfaces: https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/interface
//
// Last Update: 23.06.2022 By MauricioRB06

using System.Collections.Generic;
using Managers;
using UniRx;
using UnityEngine;

namespace Chemistry.G2
{
    public class G2Camera : MonoBehaviour
    {
        
        [Space(2)]
        [Header("Camera Settings")]
        [Space(5)]
        [Tooltip("Sets the steps of when the camera should move.")]
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
                        case 42: gameObject.GetComponent<Animator>().Play("Camera Detail Step 42");
                            break;
                        case 44: gameObject.GetComponent<Animator>().Play("Camera Detail Step 44");
                            break;
                        case 46: gameObject.GetComponent<Animator>().Play("Camera Detail Step 46");
                            break;
                        case 48: gameObject.GetComponent<Animator>().Play("Camera Detail Step 48");
                            break;
                        case 50: gameObject.GetComponent<Animator>().Play("Camera Detail Step 50");
                            break;
                    }
                });
        }
        
    }
}
