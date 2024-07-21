
// Copyright (c) 2022 MauricioRB06 <https://github.com/MauricioRB06>
// Collaborator: ShiroKame <https://github.com/ShiroKame>
// Supervisory Teacher: herrera78 <https://github.com/herrera78>
//
//  The Purpose Of This Script Is:
//
//  Sets the behavior of the magnet that will simulate the behavior of the magnetic stirrer.
//
//  Documentation and References:
//
// UniRx: https://github.com/neuecc/UniRx
// UniRx: https://programmerclick.com/article/74731581924/ [Spanish]
//
// Last Update: 23.06.2022 By MauricioRB06

using System.Collections.Generic;
using Managers;
using UnityEngine;
using UniRx;

namespace Chemistry.G4
{
    // Components required for this Script to work.
    [RequireComponent(typeof(Animator))]
    
    public class G4Magnet: MonoBehaviour
    {
        
        [Space(2)]
        [Header("Magnet Settings")]
        [Space(5)]
        [Tooltip("Set the steps where the magnet will be turned on.")]
        [SerializeField] private List<int> stepsToOn = new();
        [Tooltip("Set the steps where the magnet will be turned off.")]
        [SerializeField] private List<int> stepsToOff = new();
        [Tooltip("Place here the particles that will be created when the magnetic stirrer is turned on.")]
        [SerializeField] private GameObject stirrerParticles;
        
        // Observer subscriptions (Awake is executed when the object is created)
        private void Awake()
        {
            StepManager.Instance.Counter
                .Where(stepTrigger => stepsToOn.Contains(stepTrigger))
                .Subscribe(_ => MagnetOn());
            
            StepManager.Instance.Counter
                .Where(stepTrigger => stepsToOff.Contains(stepTrigger))
                .Subscribe(_ => MagnetOff());
        }
        
        // Starts the rotation of the magnet and creates the particles that will simulate the magnetic stirrer.
        private void MagnetOn()
        {
            if (stirrerParticles != null)
            {
                gameObject.GetComponent<Animator>().Play("Magnet");
                Instantiate(stirrerParticles,transform.position,stirrerParticles.transform.rotation);
            }
            else
            {
                Debug.LogError("The stirrerParticles is Empty, please add once.");
            }
        }
        
        // End of magnet rotation.
        private void MagnetOff() { gameObject.GetComponent<Animator>().Play("Empty"); }
        
    }
}
