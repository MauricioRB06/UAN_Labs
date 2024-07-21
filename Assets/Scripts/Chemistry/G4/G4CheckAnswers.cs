
// Copyright (c) 2022 MauricioRB06 <https://github.com/MauricioRB06>
// Collaborator: ShiroKame <https://github.com/ShiroKame>
// Supervisory Teacher: herrera78 <https://github.com/herrera78>
//
// The Purpose Of This Script Is:
//
// Establish the behavior of the button that will verify the answers in the final questionnaire.
//
// Last Update: 23.06.2022 By MauricioRB06

using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Chemistry.G4
{
    public class G4CheckAnswers : MonoBehaviour
    {
        
        [Space(2)]
        [Header("Correct Answers")]
        [Space(5)]
        [Tooltip("Enter here the Toogle containing the correct answer number 1.")]
        [SerializeField] private GameObject answer1;
        [Tooltip("Enter here the Toogle containing the correct answer number 2.")]
        [SerializeField] private GameObject answer2;
        [Space(15)]
        
        [Header("Failed Questionnaire")]
        [Space(5)]
        [Tooltip("Enter here the error message that will be displayed to the user when the questionnaire fails.")]
        [SerializeField] private GameObject errorMessage;
        
        // Function that will check if the answers that were set as correct are selected.
        public void CheckAnswers()
        {
            if (answer1.GetComponent<Toggle>().isOn && answer2.GetComponent<Toggle>().isOn)
            {
                errorMessage.SetActive(false);
                StepManager.Instance.UpdateCounter();
            }
            else
            {
                errorMessage.SetActive(true);
            }
        }
        
    }
}
