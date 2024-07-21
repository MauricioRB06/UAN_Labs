
// Copyright (c) 2022 MauricioRB06 <https://github.com/MauricioRB06>
// Collaborator: ShiroKame <https://github.com/ShiroKame>
// Supervisory Teacher: herrera78 <https://github.com/herrera78>
//
// The Purpose Of This Script Is:
//
// Set the behavior of the button that checks for correct answers.
//
// Documentation and References:
//
// UniRx: https://github.com/neuecc/UniRx
// UniRx: https://programmerclick.com/article/74731581924/ [Spanish]
//
// Last Update: 23.06.2022 By MauricioRB06

using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Chemistry.G3
{
    public class G3CheckQuestions : MonoBehaviour
    {
        
        [Space(2)]
        [Header("Correct Answers")]
        [Space(5)]
        [Tooltip("Enter here the Toogle containing the correct answer number 1.")]
        [SerializeField] private GameObject answer1;
        [Tooltip("Enter here the Toogle containing the correct answer number 2.")]
        [SerializeField] private GameObject answer2;
        [Tooltip("Enter here the Toogle containing the correct answer number 3.")]
        [SerializeField] private GameObject answer3;
        [Tooltip("Enter here the Toogle containing the correct answer number 4.")]
        [SerializeField] private GameObject answer4;
        [Space(15)]
        
        [Header("Failed Questionnaire")]
        [Space(5)]
        [Tooltip("Enter here the error message that will be displayed to the user when the questionnaire fails.")]
        [SerializeField] private GameObject errorMessage;
        
        // Function that checks if the answers set as correct in question 1 are selected.
        public void CheckAnswers1()
        {
            if (answer2.GetComponent<Toggle>().isOn)
            {
                errorMessage.SetActive(false);
                StepManager.Instance.UpdateCounter();
            }
            else
            {
                errorMessage.SetActive(true);
            }
        }
        
        // Function that checks if the answers set as correct in question 2 are selected.
        public void CheckAnswers2()
        {
            if (answer4.GetComponent<Toggle>().isOn)
            {
                errorMessage.SetActive(false);
                StepManager.Instance.UpdateCounter();
            }
            else
            {
                errorMessage.SetActive(true);
            }
        }
        
        // Function that checks if the answers set as correct in question 3 are selected.
        public void CheckAnswers3()
        {
            if (answer2.GetComponent<Toggle>().isOn)
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
