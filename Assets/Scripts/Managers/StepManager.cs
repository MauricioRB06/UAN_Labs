
// Copyright (c) 2022 MauricioRB06 <https://github.com/MauricioRB06>
// Collaborator: ShiroKame <https://github.com/ShiroKame>
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
// Last Update: 23.06.2022 By MauricioRB06

using Interactables;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Managers
{
    public class StepManager : MonoBehaviour
    {
        
        // Object where the single instance of the class will be stored.
        public static StepManager Instance;
        
        [Header("Number Of Steps ( 2 - 100 )")]
        [Range(2,100)]
        [SerializeField] private int numberOfSteps = 5;
        
        [Header("Camera Controller")]
        [SerializeField] private GameObject cameraAnimator;
        [Space(15)]
        
        [Header("Core Buttons")]
        [Tooltip("Add the start button here")]
        [SerializeField] private Button startButton;
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
        
        // Here we will store the step we are in.
        public IReactiveProperty<int> Counter { get; }
        
        // Variable that indicates when we have reached the end of the game.
        public IReadOnlyReactiveProperty<bool> IsEnd { get; }
        
        // Here we will store the total number of steps that the game will have.
        private IReactiveProperty<int> StepLimitValue { get; }
        
        // Variable to be used to reference the camera control system in Cinemachine to perform camera changes.
        private Animator _cinemachineCam;
        
        // Variables that will be used to store the status of the cameras and to know
        // which one is active and which one is not.
        private bool _camera1 = true;
        private bool _camera2;
        private bool _camera3;
        private bool _camera4;
        
        // Class Constructor.
        public StepManager()
        {
            // Singleton definition.
            if (Instance == null)
            {
                Instance = this;
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
            IsEnd = Counter.Select(step => step >= StepManager.Instance.StepLimitValue.Value).ToReactiveProperty();
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
            
            _cinemachineCam = cameraAnimator.GetComponent<Animator>();
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
                    nextButton.GetComponent<InteractableButton>().DisableButton();

                    if (SceneManager.GetActiveScene().name is not ("Chemistry_G2" or "Chemistry_G3"))
                    {
                        endButton.GetComponent<InteractableButton>().EnableButton();
                    }
                    
                });
            
            IsEnd
                .Where(isEnd => isEnd)
                .Subscribe(_ =>
                {
                    CursorManager.instance.DisableCursor();
                    instructions.SetActive(false);
                    finish.SetActive(true);
                    endButton.GetComponent<InteractableButton>().DisableButton();
                    finishPanel.SetActive(true);
                });

            Instance.Counter
                .Where(stepTrigger => stepTrigger is 49 or 51 && SceneManager.GetActiveScene().name == "BQ_G1")
                .Subscribe(_ => { ChangeButtonNext(true); });

            Instance.Counter
                .Where(stepTrigger => stepTrigger is 50 && SceneManager.GetActiveScene().name == "BQ_G1")
                .Subscribe(_ => { ChangeButtonNext(false); });

            Instance.Counter
                .Where(stepTrigger => stepTrigger is 35 or 65 && SceneManager.GetActiveScene().name == "Chemistry_G4")
                .Subscribe(_ => { ChangeButtonNext(true); });
            
            Instance.Counter
                .Where(stepTrigger => stepTrigger is 36 or 66 && SceneManager.GetActiveScene().name == "Chemistry_G4")
                .Subscribe(_ => { ChangeButtonNext(false); });
            
            Instance.Counter
                .Where(stepTrigger => stepTrigger is 43 && SceneManager.GetActiveScene().name == "Chemistry_G3")
                .Subscribe(_ => { ChangeButtonNext(true); });
            
            Instance.Counter
                .Where(stepTrigger => stepTrigger is 44 && SceneManager.GetActiveScene().name == "Chemistry_G3")
                .Subscribe(_ => { ChangeButtonNext(false); });

            Instance.Counter
                .Where(stepTrigger => stepTrigger is 2 && SceneManager.GetActiveScene().name == "Chemistry_G2")
                .Subscribe(_ => { ChangeButtonNext(false); });
        }
        
        // Function that allows buttons and interactive objects to update the step counter.
        public void UpdateCounter()
        {
            Counter.Value += 1;
            // Debug.Log("STEP: " + Counter.Value );
        }
        
        // Enables or Disables the next button.
        public void ChangeButtonNext(bool buttonState)
        {
            if (buttonState)
            {
                nextButton.GetComponent<InteractableButton>().ActivateButton();
            }
            else
            {
                nextButton.GetComponent<InteractableButton>().DeactivateButton();
            }
        }
        
        // Enables or Disables the end button.
        public void ChangeButtonEnd(bool buttonState)
        {
            if (buttonState)
            {
                endButton.GetComponent<InteractableButton>().ActivateButton();
            }
            else
            {
                endButton.GetComponent<InteractableButton>().DeactivateButton();
            }
        }
        
        // Sets the new camera configuration, when a camera change is requested.
        public void SwitchCamera()
        {
            if (_camera1)
            {
                _camera1 = false;
                _camera2 = true;
                _camera3 = false;
                _camera4 = false;
                _cinemachineCam.Play("G4 Camera 2");
            }
            else if (_camera2)
            {
                _camera1 = false;
                _camera2 = false;
                _camera3 = true;
                _camera4 = false;
                _cinemachineCam.Play("G4 Camera 3");
            }
            else if (_camera3)
            {
                _camera1 = false;
                _camera2 = false;
                _camera3 = false;            
                _camera4 = true;
                _cinemachineCam.Play("G4 Camera 4");
            }
            else if (_camera4)
            {
                _camera1 = true;
                _camera2 = false;
                _camera3 = false;
                _camera4 = false;
                _cinemachineCam.Play("G4 Camera 1");
            }
        }
        
    }
}
