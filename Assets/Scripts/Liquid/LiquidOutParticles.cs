
// Copyright (c) 2022 MauricioRB06 <https://github.com/MauricioRB06>
// Collaborator: ShiroKame <https://github.com/ShiroKame>
// Supervisory Teacher: herrera78 <https://github.com/herrera78>
//
// The Purpose Of This Script Is:
//
// Define the behavior of the particles that will simulate the liquid falling from the burette into the Beaker.
//
// Last Update: 23.06.2022 By MauricioRB06

using System.Collections.Generic;
using Managers;
using UnityEngine;

namespace Liquid
{
    // Components required for this Script to work.
    [RequireComponent(typeof(ParticleSystem))]
    
    public class LiquidOutParticles : MonoBehaviour
    {
        
        [Space(2)]
        [Header("Liquid Duration")]
        [Space(5)]
        [Tooltip("Set here the different durations in seconds that the particles" +
                 " will have, this modifies how far they will go, therefore, how far the liquid will descend.")]
        [SerializeField] private List<float> lifetimeValues = new();
        
        // This variable will be used to store the reference to the particle system just created.
        private ParticleSystem.MainModule _thisParticleSystem;
        
        // Variable initialization (Awake is executed when the object is created).
        private void Awake()
        {
            
            // We assign the particle system of the object that contains this script, to be able to manipulate it.
            _thisParticleSystem = gameObject.GetComponent<ParticleSystem>().main;
            
            // We assign a different life value to the particles, depending on the step where we are.
            _thisParticleSystem.startLifetime = StepManager.Instance.Counter.Value switch
            {
                15 => lifetimeValues[0],
                17 => lifetimeValues[1],
                19 => lifetimeValues[2],
                21 => lifetimeValues[3],
                23 => lifetimeValues[4],
                25 => lifetimeValues[5],
                27 => lifetimeValues[6],
                39 => lifetimeValues[7],
                31 => lifetimeValues[8],
                33 => lifetimeValues[9],
                45 => lifetimeValues[0],
                47 => lifetimeValues[1],
                49 => lifetimeValues[2],
                51 => lifetimeValues[3],
                53 => lifetimeValues[4],
                55 => lifetimeValues[5],
                57 => lifetimeValues[6],
                59 => lifetimeValues[7],
                61 => lifetimeValues[8],
                63 => lifetimeValues[9],
                _ => _thisParticleSystem.startLifetime
            };
        }
        
    }
}
