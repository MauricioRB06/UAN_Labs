
// Copyright (c) 2022 MauricioRB06 <https://github.com/MauricioRB06>
// Collaborator: ShiroKame <https://github.com/ShiroKame>
// Supervisory Teacher: herrera78 <https://github.com/herrera78>
//
// The Purpose Of This Script Is:
//
// Set the behavior of the text fields, where the table answers are entered.
//
// Documentation and References:
//
// UniRx: https://github.com/neuecc/UniRx
// UniRx: https://programmerclick.com/article/74731581924/ [Spanish]
//
// Last Update: 23.06.2022 By MauricioRB06

using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Chemistry.G3
{
    public class G3CheckTableQuestions : MonoBehaviour
    {
        
        [Space(2)]
        [Header("Correct Answers")]
        [Space(5)]
        [Tooltip("Enter here the InputField that refers to answer 1.")]
        [SerializeField] private GameObject input1;
        [Tooltip("Enter here the InputField that refers to answer 2.")]
        [SerializeField] private GameObject input2;
        [Tooltip("Enter here the InputField that refers to answer 3.")]
        [SerializeField] private GameObject input3;
        
        // Variables that store the user's answer.
        private string _text1;
        private string _text2;
        private string _text3;
        
        // Colors to indicate whether the answer entered is correct or not.
        public Color color1;
        public Color color2;
        
        // Updates the text based on the user input in question 1.
        public void Text1Changed(string answer) { _text1 = answer; }
        
        // Updates the text based on the user input in question 2.
        public void Text2Changed(string answer) { _text2 = answer; }
        
        // Updates the text based on the user input in question 3.
        public void Text3Changed(string answer) { _text3 = answer; }
        
        // When all 3 answers are correct, no further modifications to the text fields are allowed.
        public void CheckQuestions()
        {
            if (_text1 != "10.0" || _text2 != "12.0" || _text3 != "0.100") return;
            
            StepManager.Instance.UpdateCounter();
            input1.GetComponent<TMP_InputField>().readOnly = true;
            input2.GetComponent<TMP_InputField>().readOnly = true;
            input3.GetComponent<TMP_InputField>().readOnly = true;
            gameObject.SetActive(false);
        }
        
        // When the question text is updated, depending on whether it is correct or not,
        // the color of the corresponding text field is updated.
        
        public void FieldColor1(string answer)
        {
            input1.GetComponent<Image>().color = answer == "10.0" ? color1 : color2;
        }
        
        public void FieldColor2(string answer)
        {
            input2.GetComponent<Image>().color = answer == "12.0" ? color1 : color2;
        }
        
        public void FieldColor3(string answer)
        {
            input3.GetComponent<Image>().color = answer == "0.100" ? color1 : color2;
        }
        
    }
}
