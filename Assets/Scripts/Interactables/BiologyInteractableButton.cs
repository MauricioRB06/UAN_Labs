
// Copyright (c) 2022 MauricioRB06 <https://github.com/MauricioRB06>
// Supervisory Teacher: herrera78 <https://github.com/herrera78>
//
// The Purpose Of This Script Is:
//
// Sets the behavior of the buttons for the Biology guides.
//
// Last Update: 11.12.2022 By MauricioRB06

using Managers;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Interactables
{
    public class BiologyInteractableButton : MonoBehaviour
    {
        [Header("Button")][Space(5)]
        [Tooltip("Add the object containing this script here")]
        [SerializeField] private Button thisButton;
        
        // We verify that the variable containing the button is not empty, since we would not
        // be able to identify which button is going to be subscribed in the Start function.
        private void Awake()
        {
            if (thisButton == null)
            {
                Debug.LogError("The button box is empty, please add the button containing this script.");
            }
        }
        
        // We subscribe the button to the step manager.
        private void Start()
        {
            // When the button detects a click, it will look for the step manager to update the counter.
            thisButton.onClick.AsObservable().Subscribe(_ => BiologyStepManager.Instance.UpdateCounter());
        }
        
        // To avoid problems in the order of execution of the games,
        // functions were added to control the activity of the buttons.
        public void ActivateButton() { thisButton.interactable = true; }
        public void DeactivateButton() { thisButton.interactable = false; }
        public void EnableButton() { gameObject.SetActive(true); }
        public void DisableButton() { gameObject.SetActive(false); }
        
    }
}
