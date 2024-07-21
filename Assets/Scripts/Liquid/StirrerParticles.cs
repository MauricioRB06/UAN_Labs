
// Copyright (c) 2022 MauricioRB06 <https://github.com/MauricioRB06>
// Collaborator: ShiroKame <https://github.com/ShiroKame>
// Supervisory Teacher: herrera78 <https://github.com/herrera78>
//
// The Purpose Of This Script Is:
//
// To establish the behavior of the particles that will simulate the magnetic stirrer.
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

namespace Liquid
{
    // Components required for this Script to work.
    [RequireComponent(typeof(ParticleSystem))]
    
    public class StirrerParticles : MonoBehaviour
    {
        [Space(2)]
        [Header("Particle Settings")]
        [Space(5)]
        [Tooltip("Sets the initial gravity that will affect the particle system.")]
        [SerializeField] private float startGravityScale;
        [Tooltip("Sets the value at which the severity will be modified in the set steps.")]
        [SerializeField] private float gravityModifierValue = 0.03f;
        [Tooltip("Define here the steps in which gravity will be modified so that the particles extend their range.")]
        [SerializeField] private List<int> stepsToChangeGravity = new();
        [Tooltip("Set the step at which the particle system will be destroyed.")]
        [SerializeField]private List<int> stepToDie = new();
        
        // This variable will be used to store the reference to the particle system just created.
        private ParticleSystem.MainModule _thisParticleSystem;
        
        // Variable initialization (Awake is executed when the object is created).
        private void Awake()
        {
            _thisParticleSystem = gameObject.GetComponent<ParticleSystem>().main;
            startGravityScale = 0;
            _thisParticleSystem.gravityModifier = startGravityScale;
        }
        
        // Observer subscriptions (Start - Runs on the first frame of the game).
        private void Start()
        {
            StepManager.Instance.Counter
                .Where(strepTrigger => stepsToChangeGravity.Contains(strepTrigger))
                .Subscribe( _ =>
                {
                    startGravityScale -= gravityModifierValue;
                    _thisParticleSystem.gravityModifier = startGravityScale;
                });
            
            StepManager.Instance.Counter
                .Where(stepTrigger => stepToDie.Contains(stepTrigger))
                .Subscribe(_ =>
                {
                    Destroy(gameObject, 0.5f);
                });
        }
        
    }
}
