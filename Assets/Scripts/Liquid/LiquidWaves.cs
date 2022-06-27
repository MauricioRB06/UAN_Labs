
// Copyright (c) 2022 MauricioRB06 <https://github.com/MauricioRB06>
// Collaborator: ShiroKame <https://github.com/ShiroKame>
// Supervisory Teacher: herrera78 <https://github.com/herrera78>
//
// The Purpose Of This Script Is:
//
// Define the behavior of the waves that will simulate the fall of the liquid in the beaker.
//
// Documentation and References:
//
// UniRx: https://github.com/neuecc/UniRx
// UniRx: https://programmerclick.com/article/74731581924/ [Spanish]
//
// Last Update: 23.06.2022 By MauricioRB06

using System.Collections.Generic;
using Managers;
using UnityEngine;
using UniRx;
using UnityEngine.SceneManagement;

namespace Liquid
{
    public class LiquidWaves : MonoBehaviour
    {
        
        [Space(2)]
        [Header("Position Change")]
        [Space(5)]
        [Tooltip("Set the steps in which the position of the waves that simulate the liquid will be changed.")]
        [SerializeField] private List<int> stepsToChange = new();
        [Tooltip("Define the value at which wave position changes will be performed.")]
        [SerializeField] private float offsetValue = 0.005f;
        
        // Observer subscriptions (Start - Runs on the first frame of the game).
        private void Start()
        {
            StepManager.instance.Counter
                .Where(stepTrigger => stepsToChange.Contains(stepTrigger))
                .Subscribe( _ =>
                {
                    var thisObject = gameObject;
                    var position = thisObject.transform.position;
                    
                    position = new Vector3(position.x, 
                        position.y+ offsetValue, position.z);
                    
                    thisObject.transform.position = position;
                });
            
            StepManager.instance.Counter
                .Where(stepTrigger => stepTrigger == 36 && SceneManager.GetActiveScene().name == "Chemistry_G4")
                .Subscribe( _ =>
                {
                    var thisObject = gameObject;
                    var position = thisObject.transform.position;
                    
                    position = new Vector3(position.x, 1.1038f, position.z);
                    
                    thisObject.transform.position = position;
                });
        }
        
    }
}
