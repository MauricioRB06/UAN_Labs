
// Copyright (c) 2022 MauricioRB06 <https://github.com/MauricioRB06>
// Supervisory Teacher: herrera78 <https://github.com/herrera78>
//
// The Purpose Of This Script Is:
//
// Define the behavior of the step manager, who will be in charge of informing the game objects in which step we are,
// as well as managing the behavior of the buttons and defining the end of the game.
//
// Documentation and References:
//
// UniRx: https://github.com/neuecc/UniRx
// UniRx: https://programmerclick.com/article/74731581924/ [Spanish]
// Cinemachine: https://docs.unity3d.com/Packages/com.unity.cinemachine@2.3/api/Cinemachine.html
//
// Last Update: 11.12.2022 By MauricioRB06

using System.Collections;
using Interactables;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Enumerator with the types of objects available for selection.
public enum BiologyObjectType{
    SlideA,
    SlideB,
    PuddleWater,
    Lactofenol,
    OnionB,
    PipettePasteur,
    Yarn
}

namespace Managers
{
    public class BiologyStepManager : MonoBehaviour
    {
        // Object where the single instance of the class will be stored.
        public static BiologyStepManager instance;
        
        [Header("Number Of Steps ( 2 - 100 )")]
        [Range(2,100)]
        [SerializeField] private int numberOfSteps = 5;
        
        [Header("Core Buttons")]
        [Tooltip("Add the next button here")]
        [SerializeField] private Button nextButton;
        [Tooltip("Add the end button here")]
        [SerializeField] private Button endButton;
        [Space(15)]
        
        [Header("BlackBoard Messages")]
        [Tooltip("Add the start button here")]
        [SerializeField] private GameObject welcome;
        [Tooltip("Add the next button here")]
        [SerializeField] private GameObject instructions;
        [Tooltip("Add the end button here")]
        [SerializeField] private GameObject finish;
        [Space(15)]
        
        [Header("Finis Message")]
        [Tooltip("Add the final object here")]
        public GameObject finishPanel;
        
        // It is used to have a global reference of the state of the microscope.
        public bool MicroscopeOn { get; private set; }

        // Here we will store the step we are in.
        public IReactiveProperty<int> Counter { get; }
        
        // Variable that indicates when we have reached the end of the game.
        public IReadOnlyReactiveProperty<bool> IsEnd { get; }
        
        // Here we will store the total number of steps that the game will have.
        private IReactiveProperty<int> StepLimitValue { get; }
        public int Steps => numberOfSteps;
        
        // Class Constructor.
        public BiologyStepManager()
        {
            // Singleton definition.
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            } 
            
            // Declarative Property.
            Counter = new ReactiveProperty<int>(0);
            
            // Declarative Property.
            StepLimitValue = new ReactiveProperty<int>(numberOfSteps);
            
            // Declarative Property
            IsEnd = Counter.Select(step => step >= instance.StepLimitValue.Value).ToReactiveProperty();
        }
        
        // We define the limit of steps of the game.
        private void Awake()
        {
            if (finishPanel == null)
            {
                Debug.LogError("The final panel is empty, please add one.");
            }
            else
            {
                finishPanel.gameObject.SetActive(false);
                StepLimitValue.Value = numberOfSteps;
            }
        }
        
        // We define the behavior that the end of the game will have.
        private void Start()
        {
            Counter
                .Where(stepTrigger => stepTrigger == 2)
                .Subscribe(_ =>
                {
                    welcome.SetActive(false);
                    instructions.SetActive(true);
                });
            
            Counter
                .Where(stepTrigger => stepTrigger == numberOfSteps-1)
                .Subscribe(_ =>
                {
                    nextButton.GetComponent<BiologyInteractableButton>().DisableButton();
                    endButton.GetComponent<BiologyInteractableButton>().EnableButton();
                    
                });
            
            IsEnd
                .Where(isEnd => isEnd)
                .Subscribe(_ =>
                {
                    BiologyCursorManager.Instance.DisableCursor();
                    instructions.SetActive(false);
                    finish.SetActive(true);
                    endButton.GetComponent<BiologyInteractableButton>().DisableButton();
                    finishPanel.SetActive(true);
                });
            
        }
        
        public void LoadMicroscope()
        {
            endButton.GetComponent<BiologyInteractableButton>().DeactivateButton();
            StartCoroutine(LoadMicroscopeMap());
        }
        
        private IEnumerator LoadMicroscopeMap()
        {
            yield return new WaitForSeconds(5f);
            Destroy(gameObject);
            SceneManager.LoadScene("G1 Microscope");
        }
        
        // Function that allows buttons and interactive objects to update the step counter.
        public void UpdateCounter()
        {
            Counter.Value += 1;
            //Debug.Log("STEP: " + Counter.Value );
        }
        
        // 
        public void SwitchMicroscope() { MicroscopeOn = !MicroscopeOn; }

        // Enables or Disables the next button.
        public void ChangeButtonNext(bool buttonState)
        {
            if (buttonState)
            {
                nextButton.GetComponent<BiologyInteractableButton>().ActivateButton();
            }
            else
            {
                nextButton.GetComponent<BiologyInteractableButton>().DeactivateButton();
            }
        }
        
        // Enables or Disables the end button.
        public void ChangeButtonEnd(bool buttonState)
        {
            if (buttonState)
            {
                endButton.GetComponent<BiologyInteractableButton>().ActivateButton();
            }
            else
            {
                endButton.GetComponent<BiologyInteractableButton>().DeactivateButton();
            }
        }
        
    }
}
