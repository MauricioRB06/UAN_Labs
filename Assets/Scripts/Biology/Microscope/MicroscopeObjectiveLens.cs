
using System.Collections;
using System.Collections.Generic;
using Interfaces;
using Managers;
using UniRx;
using UnityEngine;

namespace Biology.Microscope
{
    public class MicroscopeObjectiveLens : RotatingPart
    {

        [Header("Image Pack Filters Settings")][Space(5)]
        [Tooltip("Enter here the image for the microscope light filter.")]
        [SerializeField] private AudioClip lensSwitchSfx;
        
        [Header("Image Pack Filters Settings")][Space(5)]
        [Tooltip("Enter here the image for the microscope light filter.")]
        [SerializeField] private bool canUseObjectiveX100;
        
        [Tooltip("Set the step at which the sample will be enabled.")]
        [SerializeField] private List<int> stepsToEnableX100;
        [Tooltip("Set the step at which the sample will be disable.")]
        [SerializeField] private List<int> stepToDisableX100;
        [Tooltip("Set the step at which the sample will be disable.")]
        [SerializeField] private List<int> stepsToRestoreObjectives;
        [Tooltip("Set the step at which the sample will be disable.")]
        [SerializeField] private List<int> stepsToSendLensReport;
        
        private bool _canRotate = true;
        
        private bool _isInLenX4;
        private bool _isInLenX10;
        private bool _isInLenX40;
        private bool _isInLenX100 = true;
        
        private int _internalMaxDegreesRotationX ;
        private int _internalMinDegreesRotationX;

        private void Awake()
        {
            BiologyStepManager.Instance.Counter
                .Where(stepTrigger => stepsToEnableX100.Contains(stepTrigger))
                .Subscribe(_ => { canUseObjectiveX100 = true; });
            
            BiologyStepManager.Instance.Counter
                .Where(stepTrigger => stepToDisableX100.Contains(stepTrigger))
                .Subscribe(_ => { canUseObjectiveX100 = false; });
            
            BiologyStepManager.Instance.Counter
                .Where(stepTrigger => stepsToRestoreObjectives.Contains(stepTrigger))
                .Subscribe(_ => { StartCoroutine(RestoreLens()); });
            
            BiologyStepManager.Instance.Counter
                .Where(stepTrigger => stepsToSendLensReport.Contains(stepTrigger))
                .Subscribe(_ => { StartCoroutine(ReportLensRotation()); });
        }

        private IEnumerator ReportLensRotation()
        {
            yield return new WaitForSeconds(0.1f);
            UpdateUI(false);
        }
        
        private void OnMouseDrag()
        {
            if (!CanRotate) return;
            if(!BiologyStepManager.Instance.MicroscopeOn) return;
            
            var rotX = Input.GetAxis("Mouse X") * rotationSpeed * Mathf.Deg2Rad;

            if (rotateX && _canRotate)
            {
                if (canUseObjectiveX100)
                {
                    _internalMaxDegreesRotationX = maxDegreesRotationX;
                    _internalMinDegreesRotationX = minDegreesRotationX;
                }
                else
                {
                    _internalMaxDegreesRotationX = maxDegreesRotationX - 10;
                    _internalMinDegreesRotationX = minDegreesRotationX + 95;
                }
                
                AngleX += rotX;
                
                if (AngleX < _internalMinDegreesRotationX)
                {
                    AngleX = _internalMinDegreesRotationX;
                }
                else if (AngleX > _internalMaxDegreesRotationX)
                {
                    AngleX = _internalMaxDegreesRotationX;
                }

                if (AngleX > _internalMinDegreesRotationX && AngleX < _internalMaxDegreesRotationX)
                {
                    transform.Rotate(Vector3.up, -rotX);
                    
                    if (AngleX > 10 & _isInLenX100)
                    {
                        _canRotate = false;
                        StartCoroutine(RotateX100ToX40());
                    }
                    
                    if (AngleX < 80 & _isInLenX40)
                    {
                        _canRotate = false;
                        StartCoroutine(RotateX40ToX100());
                    }

                    if (AngleX > 100 & _isInLenX40)
                    {
                        _canRotate = false;
                        StartCoroutine(RotateX40ToX10());
                    }
                    
                    if (AngleX < 170 & _isInLenX4)
                    {
                        _canRotate = false;
                        StartCoroutine(RotateX4ToX40());
                    }
                    
                    if (AngleX > 190 & _isInLenX4)
                    {
                        _canRotate = false;
                        StartCoroutine(RotateX4ToX10());
                    }
                    
                    if (AngleX > 280 & _isInLenX10)
                    {
                        _canRotate = false;
                        StartCoroutine(RotateX10ToX100());
                    }
                    
                    if (AngleX < 270 & _isInLenX10)
                    {
                        _canRotate = false;
                        StartCoroutine(RotateX10ToX4());
                    }
                    
                    if (AngleX < -9 & _isInLenX100)
                    {
                        _canRotate = false;
                        StartCoroutine(RotateX100ToX10());
                    }
                }
            }
        }

         private IEnumerator RestoreLens()
         {
             if (_isInLenX10)
             {
                 for (var i = AngleX; i > 181; i -= 5)
                 {
                     transform.Rotate(Vector3.up, 5f);
                     AngleX -= 5f;
                     yield return new WaitForSeconds(0.01f);
                 }
                 
                 _isInLenX10 = false;
             }
             else if (_isInLenX40)
             {
                 for (var i = AngleX; i < 180; i += 5)
                 {
                     transform.Rotate(Vector3.up, -5f);
                     AngleX += 5f;
                     yield return new WaitForSeconds(0.01f);
                 }
                 
                 _isInLenX40 = false;
             }
             else if (_isInLenX100)
             {
                 for (var i = AngleX; i < 180; i+=5)
                 {
                     transform.Rotate(Vector3.up, -5f);
                     AngleX += 5f;
                     yield return new WaitForSeconds(0.01f);
                 }
                 
                 _isInLenX100 = false;
             }
             
             _isInLenX4 = true;
             UpdateUI(false);
         }
         
         private IEnumerator RotateX100ToX40()
         {
             for (var i = AngleX; i < 90; i+=5)
             {
                 transform.Rotate(Vector3.up, -5f);
                 AngleX += 5f;
                 yield return new WaitForSeconds(0.05f);
             }
             
             _canRotate = true;
             _isInLenX4 = false;
             _isInLenX10 = false;
             _isInLenX40 = true;
             _isInLenX100 = false;
             UpdateUI(true);
         }

         private IEnumerator RotateX40ToX10()
         {
             for (var i = AngleX; i < 180; i+=5)
             {
                 transform.Rotate(Vector3.up, -5f);
                 AngleX += 5f;
                 yield return new WaitForSeconds(0.05f);
             }
             
             _canRotate = true;
             _isInLenX4 = true;
             _isInLenX10 = false;
             _isInLenX40 = false;
             _isInLenX100 = false;
             UpdateUI(true);
         }

         private IEnumerator RotateX4ToX10()
         {
             for (var i = AngleX; i < 270; i+=5)
             {
                 transform.Rotate(Vector3.up, -5f);
                 AngleX += 5f;
                 yield return new WaitForSeconds(0.05f);
             }
             
             _canRotate = true;
             _isInLenX4 = false;
             _isInLenX10 = true;
             _isInLenX40 = false;
             _isInLenX100 = false;
             UpdateUI(true);
         }
         
         private IEnumerator RotateX10ToX100()
         {
             for (var i = AngleX; i < 360; i+=5)
             {
                 transform.Rotate(Vector3.up, -5f);
                 AngleX += 5f;
                 yield return new WaitForSeconds(0.05f);
             }

             AngleX = 0;
             transform.rotation = Quaternion.Euler(0, 0, -18.25f);
             _canRotate = true;
             _isInLenX4 = false;
             _isInLenX10 = false;
             _isInLenX40 = false;
             _isInLenX100 = true;
             UpdateUI(true);
         }
         
         private IEnumerator RotateX40ToX100()
         {
             for (var i = AngleX; i > 1; i-=5)
             {
                 transform.Rotate(Vector3.up, 5f);
                 AngleX -= 5f;
                 yield return new WaitForSeconds(0.05f);
             }
             
             AngleX = 0;
             transform.rotation = Quaternion.Euler(0, 0, -18.25f);
             _canRotate = true;
             
             _canRotate = true;
             _isInLenX4 = false;
             _isInLenX10 = false;
             _isInLenX40 = false;
             _isInLenX100 = true;
             UpdateUI(true);
         }
         
         private IEnumerator RotateX4ToX40()
         {
             for (var i = AngleX; i > 91; i-=5)
             {
                 transform.Rotate(Vector3.up, 5f);
                 AngleX -= 5f;
                 yield return new WaitForSeconds(0.05f);
             }
             
             _canRotate = true;
             _isInLenX4 = false;
             _isInLenX10 = false;
             _isInLenX40 = true;
             _isInLenX100 = false;
             UpdateUI(true);
         }
         
         private IEnumerator RotateX10ToX4()
         {
             for (var i = AngleX; i > 181; i-=5)
             {
                 transform.Rotate(Vector3.up, 5f);
                 AngleX -= 5f;
                 yield return new WaitForSeconds(0.05f);
             }
             
             _canRotate = true;
             _isInLenX4 = true;
             _isInLenX10 = false;
             _isInLenX40 = false;
             _isInLenX100 = false;
             UpdateUI(true);
         }
         
         private IEnumerator RotateX100ToX10()
         {
             for (var i = AngleX; i > -90; i-=5)
             {
                 transform.Rotate(Vector3.up, 5f);
                 AngleX -= 5f;
                 yield return new WaitForSeconds(0.05f);
             }
             
             AngleX = 270;
             _canRotate = true;
             _isInLenX4 = false;
             _isInLenX10 = true;
             _isInLenX40 = false;
             _isInLenX100 = false;
             UpdateUI(true);
         }

         private void UpdateUI(bool withSound)
         {
             if (withSound) { AudioManager.Instance.PlaySfx(lensSwitchSfx); }

             if (canUseObjectiveX100)
             {
                 if (objectToInteract != null && objectToInteract.GetComponent<IInteractablePart>() != null)
                 {
                     objectToInteract.GetComponent<IInteractablePart>().InteractivePart(AngleX, true);
                 }
                    
                 if (uiToInteract != null && uiToInteract.GetComponent<IInteractableUI>() != null)
                 {
                     uiToInteract.GetComponent<IInteractableUI>().InteractUI(xAxisValue, AngleX);
                 }
             }
             else
             {
                 if (objectToInteract != null && objectToInteract.GetComponent<IInteractablePart>() != null)
                 {
                     objectToInteract.GetComponent<IInteractablePart>().InteractivePart(AngleX, false);
                 }
                    
                 if (uiToInteract != null && uiToInteract.GetComponent<IInteractableUI>() != null)
                 {
                     uiToInteract.GetComponent<IInteractableUI>().InteractUI(xAxisValue, AngleX);
                 }
             }
         }
    }

}