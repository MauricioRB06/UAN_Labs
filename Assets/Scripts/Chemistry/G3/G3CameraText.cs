
// Copyright (c) 2022 MauricioRB06 <https://github.com/MauricioRB06>
// Collaborator: ShiroKame <https://github.com/ShiroKame>
// Supervisory Teacher: herrera78 <https://github.com/herrera78>
//
// The Purpose Of This Script Is:
//
// Sets the behavior of the text displayed on the camera when zooming.
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

namespace Chemistry.G3
{
    // This components are required for this script to work.
    [RequireComponent(typeof(TextMeshProUGUI))]
    
    public class G3CameraText : MonoBehaviour
    {
        
        [Space(2)]
        [Header("Zoom Camera Text Settings")]
        [Space(5)]
        [Tooltip("Sets the steps in which the text will be updated.")]
        [SerializeField] private List<int> stepsToSetText = new();
        [Tooltip("Sets the text to be displayed in the corresponding step.")]
        [SerializeField] private List<string> textToSet = new();
        
        // Observer subscriptions (Awake is executed when the object is created).
        private void Awake()
        {
            StepManager.Instance.Counter
                .Where(stepTrigger => stepsToSetText.Contains(stepTrigger))
                .Subscribe(_ =>
                {
                    switch (StepManager.Instance.Counter.Value)
                    {
                        case 20: ChangeValue("-.-- mL");
                            break;
                        case 21: ChangeValue(textToSet[0]);
                            break;
                        case 22: ChangeValue("-.-- mL");
                            break;
                        case 23: ChangeValue(textToSet[1]);
                            break;
                        case 24: ChangeValue("-.-- mL");
                            break;
                        case 25: ChangeValue(textToSet[2]);
                            break;
                        case 26: ChangeValue("-.-- mL");
                            break;
                        case 27: ChangeValue(textToSet[3]);
                            break;
                        case 28: ChangeValue("-.-- mL");
                            break;
                        case 29: ChangeValue(textToSet[4]);
                            break;
                        case 30: ChangeValue("-.-- mL");
                            break;
                        case 31: ChangeValue(textToSet[5]);
                            break;
                        case 32: ChangeValue("-.-- mL");
                            break;
                        case 33: ChangeValue(textToSet[6]);
                            break;
                        case 34: ChangeValue("-.-- mL");
                            break;
                        case 35: ChangeValue(textToSet[7]);
                            break;
                        case 36: ChangeValue("-.-- mL");
                            break;
                        case 37: ChangeValue(textToSet[8]);
                            break;
                        case 38: ChangeValue("-.-- mL");
                            break;
                        case 39: ChangeValue(textToSet[9]);
                            break;
                        case 40: ChangeValue("-.-- mL");
                            break;
                        case 41: ChangeValue(textToSet[10]);
                            break;
                        case 42: ChangeValue("-.-- mL");
                            break;
                        case 43: ChangeValue(textToSet[11]);
                            break;
                    }
                });
        }
        
        // Modifies the camera zoom text given a value.
        private void ChangeValue(string value) { gameObject.GetComponent<TextMeshProUGUI>().SetText(value); }
        
    }
}
