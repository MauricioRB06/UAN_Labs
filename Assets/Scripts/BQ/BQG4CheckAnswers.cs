
using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace BQ
{
    public class BQG4CheckAnswers : MonoBehaviour
    {
        [Space(2)]
        [Header("Correct Answers")]
        [Space(5)]
        [Tooltip("Place here the object containing the text field of the answer.")]
        [SerializeField] private GameObject inputField;
        [Tooltip("Enter here the Toogle containing the correct answer number 2.")]
        [SerializeField] private GameObject answer2;
        [Space(15)]

        [Header("Failed Questionnaire")]
        [Space(5)]
        [Tooltip("Enter here the error message that will be displayed to the user when the questionnaire fails.")]
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

        // Function that updates the color of the text field if the answer is correct or not.
        public void FieldColor(string answer)
        {
            inputField.GetComponent<Image>().color = answer == "5.97" ? color1 : color2;
        }

        // Function that will check if the answers that were set as correct are selected.
        public void CheckAnswers()
        {
            if (_text1 == "5.97" && answer2.GetComponent<Toggle>().isOn)
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