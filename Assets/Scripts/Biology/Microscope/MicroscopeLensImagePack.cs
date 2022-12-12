
// Copyright (c) 2022 MauricioRB06 <https://github.com/MauricioRB06>
// Supervisory Teacher: herrera78 <https://github.com/herrera78>
//
// The Purpose Of This Script Is:
//
// Establish the behavior of the image packages of the laboratory samples. 
//
// Documentation and References:
//
// C# Interfaces: https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/interfaces/
//
// Last Update: 11.12.2022 By MauricioRB06

using System.Collections;
using Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace Biology.Microscope
{
    public class MicroscopeLensImagePack : MonoBehaviour, IInteractablePart, IInteractableUI
    {
        [Header("Image Pack Mask Settings")][Space(5)]
        [Tooltip("Enter here the mask for the Zoom x10 image to be used.")]
        [SerializeField] private GameObject maskZoomX10;
        [Tooltip("Enter here the mask for the Zoom x20 image to be used.")]
        [SerializeField] private GameObject maskZoomX20;
        [Tooltip("Enter here the mask for the Zoom x40 image to be used.")]
        [SerializeField] private GameObject maskZoomX40;
        [Tooltip("Enter here the mask for the Zoom x100 image to be used.")]
        [SerializeField] private GameObject maskZoomX100;
        [Space(15)]
        
        [Header("Image Pack Image Settings")][Space(5)]
        [Tooltip("Enter here the image for the Zoom x10 image to be used.")]
        [SerializeField] private GameObject imageZoomX10;
        [Tooltip("Enter here the image for the Zoom x20 image to be used.")]
        [SerializeField] private GameObject imageZoomX20;
        [Tooltip("Enter here the image for the Zoom x40 image to be used.")]
        [SerializeField] private GameObject imageZoomX40;
        [Tooltip("Enter here the image for the Zoom x100 image to be used.")]
        [SerializeField] private GameObject imageZoomX100;
        [Space(15)]
        
        [Header("Image Pack Filters Settings")][Space(5)]
        [Tooltip("Enter here the image for the microscope light filter.")]
        [SerializeField] private Image layerLightUI;
        [Tooltip("Enter the image for the microscope's Blur A filter here.")]
        [SerializeField] private Image layerBlurA;
        [Tooltip("Enter the image for the microscope's Blur B filter here.")]
        [SerializeField] private Image layerBlurB;
        
        // They are used to verify the last position of the turning wheels, to know where to move the images.
        private float _lastValueA;
        private float _lastValueB;
        
        // They are used to know the position of the image and to set its movement limits.
        private float _imagePositionX = -195;
        private float _imagePositionY = -195;
        private float _imageSelector;
        
        // They are used to control the behavior of the microscope filters.
        private bool _canChangeLight = true;
        private bool _lightIsReset;
        private bool _canChangeBlur = true;
        private bool _blurIsReset;
        
        // Interactable Interface Implementation.
        public void InteractivePart(float movementRange) { _imageSelector = movementRange; }
        
        // Interactable UI Interface Implementation.
        public void InteractUI(int axis, float value)
        {
            if (axis == 0)
            {
                if(value * 1.08f > _lastValueA)
                {
                    _lastValueA = value * 1.08f;
                    if (_imagePositionX <= 194) { _imagePositionX += value*1.08f/_lastValueA; }
                }
                else
                {
                    _lastValueA = value * 1.08f;
                    if (_imagePositionX >= -194) { _imagePositionX -= value*1.08f/_lastValueA; }
                }
                
                imageZoomX10.transform.localPosition = new Vector3(_imagePositionX, 
                    imageZoomX10.transform.localPosition.y, 0);
                imageZoomX20.transform.localPosition = new Vector3(_imagePositionX, 
                    imageZoomX20.transform.localPosition.y, 0);
                imageZoomX40.transform.localPosition = new Vector3(_imagePositionX, 
                    imageZoomX40.transform.localPosition.y, 0);
                imageZoomX100.transform.localPosition = new Vector3(_imagePositionX, 
                    imageZoomX100.transform.localPosition.y, 0);
            }
            else if (axis == 1)
            {
                if(value * 1.08f > _lastValueA)
                {
                    _lastValueA = value * 1.08f;
                    if (_imagePositionY <= 194) { _imagePositionY += value*1.08f/_lastValueA; }
                }
                else
                {
                    _lastValueA = value * 1.08f;
                    if (_imagePositionY >= -194) { _imagePositionY -= value*1.08f/_lastValueA; }
                }
                
                imageZoomX10.transform.localPosition = new Vector3(imageZoomX10.transform.localPosition.x, 
                    _imagePositionY, 0);
                imageZoomX20.transform.localPosition = new Vector3(imageZoomX20.transform.localPosition.x, 
                    _imagePositionY, 0);
                imageZoomX40.transform.localPosition = new Vector3(imageZoomX40.transform.localPosition.x, 
                    _imagePositionY, 0);
                imageZoomX100.transform.localPosition = new Vector3(imageZoomX100.transform.localPosition.x, 
                    _imagePositionY, 0);
            }
            else if (axis == 2)
            {
                if (_canChangeLight)
                {
                    layerLightUI.color = new Color(layerLightUI.color.r, layerLightUI.color.g, layerLightUI.color.b,
                        Random.Range(0f, 0.95f));
                    _canChangeLight = false;
                }
                else
                {
                    if (_lightIsReset) return;
                    StartCoroutine(ResetLight());
                }
            }
            else if (axis == 3)
            {
                if (_canChangeBlur)
                {
                    layerBlurA.color = new Color(layerBlurA.color.r, layerBlurA.color.g, layerBlurA.color.b,
                        Random.Range(0f, 0.95f));
                    _blurIsReset = false;
                }
                else
                {
                    if (_blurIsReset) return;
                    StartCoroutine(ResetBlur());
                }
            }
            else if (axis == 4)
            {
                if (_canChangeBlur)
                {
                    layerBlurB.color = new Color(layerBlurB.color.r, layerBlurB.color.g, layerBlurB.color.b,
                        Random.Range(0f, 0.95f));
                    _blurIsReset = false;
                }
                else
                {
                    if (_blurIsReset) return;
                    StartCoroutine(ResetBlur());
                }
            }
        }
        
        private IEnumerator ResetLight()
        {
            _lightIsReset = true;
            yield return new WaitForSeconds(0.4f);
            _canChangeLight = true;
            _lightIsReset = false;
        }
        
        private IEnumerator ResetBlur()
        {
            _blurIsReset = true;
            yield return new WaitForSeconds(0.4f);
            _canChangeBlur = true;
            _blurIsReset = false;
        }
        
        private void Update()
        {
            switch (_imageSelector)
            {
                case < 30:
                    maskZoomX10.SetActive(false);
                    maskZoomX20.SetActive(false);
                    maskZoomX40.SetActive(false);
                    maskZoomX100.SetActive(true);
                    break;
                case > 70 and < 130:
                    maskZoomX10.SetActive(false);
                    maskZoomX20.SetActive(false);
                    maskZoomX40.SetActive(true);
                    maskZoomX100.SetActive(false);
                    break;
                case > 160 and < 220:
                    maskZoomX10.SetActive(true);
                    maskZoomX20.SetActive(false);
                    maskZoomX40.SetActive(false);
                    maskZoomX100.SetActive(false);
                    break;
                case > 250:
                    maskZoomX10.SetActive(false);
                    maskZoomX20.SetActive(true);
                    maskZoomX40.SetActive(false);
                    maskZoomX100.SetActive(false);
                    break;
                default:
                    maskZoomX10.SetActive(false);
                    maskZoomX20.SetActive(false);
                    maskZoomX40.SetActive(false);
                    maskZoomX100.SetActive(false);
                    break;
            }
        }
    }
}
