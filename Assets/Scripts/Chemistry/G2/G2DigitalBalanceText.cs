
// Copyright (c) 2022 MauricioRB06 <https://github.com/MauricioRB06>
// Collaborator: ShiroKame <https://github.com/ShiroKame>
// Supervisory Teacher: herrera78 <https://github.com/herrera78>
//
// The Purpose Of This Script Is:
//
// Establish the behavior of the digital balance text.
//
// Documentation and References:
//
// UniRx: https://github.com/neuecc/UniRx
// UniRx: https://programmerclick.com/article/74731581924/ [Spanish]
//
// Last Update: 23.06.2022 By MauricioRB06

using System.Collections.Generic;
using Managers;
using TMPro;
using UniRx;
using UnityEngine;

namespace Chemistry.G2
{
    public class G2DigitalBalanceText : MonoBehaviour
    {
        
        [Space(2)]
        [Header("Step Settings")]
        [Space(5)]
        [Tooltip("Set the steps in which the text will be modified.")]
        [SerializeField] private List<int> stepsToSetText;
        
        // Observer subscriptions (Awake is executed when the object is created).
        private void Awake()
        {
            StepManager.Instance.Counter
                .Where(stepTrigger => stepsToSetText.Contains(stepTrigger))
                .Subscribe(_ =>
                {
                    switch (StepManager.Instance.Counter.Value)
                    {
                        case 10: gameObject.GetComponent<TextMeshPro>().SetText("0.00");
                            break;
                        case 13: gameObject.GetComponent<TextMeshPro>().SetText("90.23");
                            break;
                        case 14: gameObject.GetComponent<TextMeshPro>().SetText("0.00");
                            break;
                        case 16: gameObject.GetComponent<TextMeshPro>().SetText("0.23");
                            break;
                        case 19: gameObject.GetComponent<TextMeshPro>().SetText("0.64");
                            break;
                        case 22: gameObject.GetComponent<TextMeshPro>().SetText("1.00");
                            break;
                        case 25: gameObject.GetComponent<TextMeshPro>().SetText("0.00");
                            break;
                    }
                });
        }
        
    }
}
