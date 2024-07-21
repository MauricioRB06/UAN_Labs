
// Copyright (c) 2022 MauricioRB06 <https://github.com/MauricioRB06>
// Collaborator: ShiroKame <https://github.com/ShiroKame>
// Supervisory Teacher: herrera78 <https://github.com/herrera78>
//
// The Purpose Of This Script Is:
//
// Sets the behavior of the button that checks the answers.
//
// Documentation and References:
//
// UniRx: https://github.com/neuecc/UniRx
// UniRx: https://programmerclick.com/article/74731581924/ [Spanish]
// C# Interfaces: https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/interface
//
// Last Update: 23.06.2022 By MauricioRB06

using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Chemistry.G2
{
    public class G2CheckQuestions : MonoBehaviour
    {
        [Space(2)]
        [Header("Check Settings")]
        [Space(5)]
        [Tooltip("Place here the object containing the text field of the answer.")]
        [SerializeField] private GameObject inputField;
        [Tooltip("Enter here the error message that will be displayed in case the answer is incorrect.")]
        [SerializeField] private GameObject errorMessage;

        // Variable to check the response.
        private string _text1;
        
        // Background colors for the text field.
        public Color color1;
        public Color color2;
        
        // Function that updates the text variable when the user enters a new input.
        public void TextChanged(string answer)
        {
            _text1 = answer;
        }
        
        // Function that checks if the answer is correct.
        public void CheckQuestions()
        {
            if (_text1 == "1")
            {
                inputField.GetComponent<TMP_InputField>().readOnly = true;
                StepManager.Instance.UpdateCounter();
            }
            else
            {
                errorMessage.SetActive(true);
            }
        }
        
        // Function that updates the color of the text field if the answer is correct or not.
        public void FieldColor(string answer)
        {
            inputField.GetComponent<Image>().color = answer == "1" ? color1 : color2;
        }
        
    }
}
